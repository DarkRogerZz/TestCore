using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using NLog.Web;

namespace TestCore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddInMemoryCollection(new Dictionary<string, string>
            {
                {"Name","Dennis" },
                {"Age","23" },
                {"Gender","Male" },
                {"Address:Address1","�Ϻ��ֽ�" },
                {"Address:Address2","�Ϻ��ֶ�" }
            });
            Console.WriteLine("=======================�ڴ����õĶ�ȡ======================");
            var configRoot = builder.Build();
            Console.WriteLine($"Name:{configRoot["Name"]}");
            Console.WriteLine($"Age:{configRoot["Age"]}");
            Console.WriteLine($"Gender:{configRoot["Gender"]}");
            //�����ȡ��Address�ڵ�������
            var addressRoot = configRoot.GetSection("Address");
            Console.WriteLine($"Address__Address1:{addressRoot["Address1"]}");
            Console.WriteLine($"Address__Address2:{addressRoot["Address2"]}");
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging((context, ILoggingBuilder) => {
                    ILoggingBuilder.AddFilter("System", LogLevel.Warning);
                    ILoggingBuilder.AddFilter("Microsoft", LogLevel.Warning);
                    ILoggingBuilder.AddLog4Net("log4net.xml");
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>().UseNLog();
                });
    }
}
