using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.Email;
using Serilog.Sinks.MSSqlServer;
using Serilog.Sinks.MSSqlServer.Sinks.MSSqlServer.Options;

namespace WebApi.Swagger
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //var columnOptions = new ColumnOptions
            //{
            //    AdditionalColumns = new Collection<SqlColumn>
            //    {
            //        new SqlColumn{ColumnName = "EnvironmentUserName", PropertyName = "UserName", DataType = SqlDbType.NVarChar, DataLength = 64},

            //        new SqlColumn{ColumnName = "UserId", DataType = SqlDbType.BigInt, NonClusteredIndex = true},

            //        new SqlColumn {ColumnName = "RequestUri", DataType = SqlDbType.NVarChar, DataLength = -1, AllowNull = false},
            //    }
            //};

            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)//��������־������д,����֮��,Ŀǰ���ֻ��΢���Դ�����־���
            .ReadFrom.Configuration(new ConfigurationBuilder()
            .AddJsonFile("SeriLogConfigs.json")
            .Build())
             .WriteTo.Email(new EmailConnectionInfo()
             {
                 EmailSubject = "ϵͳ����,�����ٲ鿴!",//�ʼ�����
                 FromEmail = "791457931@qq.com",//����������
                 MailServer = "smtp.qq.com",//smtp��������ַ
                 NetworkCredentials = new NetworkCredential(userName: "791457931@qq.com", password: "C#EqualTo2C++LJC"),//�������ֱ��Ƿ�����������ͻ�����Ȩ��
                 Port = 587,//�˿ں�
                 ToEmail = "188***@163.com"//�ռ���

             })
            //.WriteTo.MSSqlServer(@"Server=...", sinkOptions: new SinkOptions { TableName = "Logs" }, columnOptions: columnOptions)
            .CreateLogger();
           
            Log.Information("info");
            Log.Error("err");
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
