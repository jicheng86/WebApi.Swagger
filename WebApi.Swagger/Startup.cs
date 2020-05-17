using System;
using System.IO;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using WebApi.Swagger.Controllers;
using WebApi.Swagger.DomainModel;
using WebApi.Swagger.Service;

namespace WebApi.Swagger
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // Register the Swagger services  --NSwag
            services.AddSwaggerDocument(options =>
            {
                options.PostProcess = document =>
                {
                    document.Info.Version = "v1";
                    document.Info.Title = "APISwagger";
                    document.Info.Description = "ASP.NET Core web API";
                    document.Info.TermsOfService = "None";
                    document.Info.Contact = new NSwag.OpenApiContact
                    {
                        Name = "JiChengLee",
                        Email = "791457931@qq.com",
                        Url = "https://www.cnblogs.com/jicheng/"
                    };
                    document.Info.License = new NSwag.OpenApiLicense
                    {
                        Name = "JiChengLee",
                        Url = "https://www.cnblogs.com/jicheng/"
                    };
                };
            });

            services.Configure<JwtManagement>(Configuration.GetSection("jwtSettingConfig"));


            JwtManagement jwtManagement = Configuration.GetSection("jwtSettingConfig").Get<JwtManagement>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                //Token Validation Parameters
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    //��ȡ������Ҫʹ�õ�Microsoft.IdentityModel.Tokens.SecurityKey����ǩ����֤��
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtManagement.Secret)),
                    //��ȡ������һ��System.String������ʾ��ʹ�õ���Ч�����߼����ҵķ����ߡ�
                    ValidIssuer = jwtManagement.Issuer,
                    //��ȡ������һ���ַ��������ַ�����ʾ�����ڼ�����Ч���ڷ������ƵĹ��ڡ�
                    ValidAudience = jwtManagement.Audience,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                };

                services.AddSingleton<ILogger, ILogger<JwtAuthenticationController>>();
                services.AddSingleton<ILogger, ILogger<DemoController>>();
            });


            services.AddScoped<IJwtAuthenticateService, JwtAuthenticationService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseOpenApi();
            app.UseSwaggerUi3();

            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
