using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;

namespace Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Log.Logger = new LoggerConfiguration()
            //    .MinimumLevel.Information()//��С�ļ�¼�ȼ�
            //    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)//��������־������д,����֮��,Ŀǰ���ֻ��΢���Դ�����־���
            //    .WriteTo.Console()//���������̨
            //    .CreateLogger();

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)//��������־������д,����֮��,Ŀǰ���ֻ��΢���Դ�����־���
                .ReadFrom.Configuration(new ConfigurationBuilder()
                .AddJsonFile("SeriLogConfigs.json")
                .Build())
                .CreateLogger();

            Log.Information("info");
            Log.Error("err");
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .UseSerilog()
            .ConfigureWebHostDefaults(webBuilder =>
             {
                 webBuilder.UseStartup<Startup>();
             });
    }
}
