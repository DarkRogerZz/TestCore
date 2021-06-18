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
                {"Address:Address1","上海浦江" },
                {"Address:Address2","上海浦东" }
            });
            Console.WriteLine("=======================内存配置的读取======================");
            var configRoot = builder.Build();
            Console.WriteLine($"Name:{configRoot["Name"]}");
            Console.WriteLine($"Age:{configRoot["Age"]}");
            Console.WriteLine($"Gender:{configRoot["Gender"]}");
            //这里读取出Address节点下内容
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
