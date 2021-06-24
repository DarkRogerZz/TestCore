using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration.Memory;
using NLog.Web;
using TestCore.Options;

namespace TestCore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var index = Array.IndexOf(args,"/env");
            var environment = index > -1 ? args[index + 1] : "Development";

            var source = new Dictionary<string, string>
            {
                ["format:dateTime:longDatePattern"] = "dddd, MMMM d, yyyy",
                ["format:dateTime:longTimePattern"] = "h:mm:ss tt",
                ["format:dateTime:shortDatePattern"] = "M/d/yyyy",
                ["format:dateTime:shortTimePattern"] = "h:mm tt",

                ["format:currencyDecimal:digits"] = "2",
                ["format:currencyDecimal:symbol"] = "$",
            };


            //var configuration = new ConfigurationBuilder()
            //    .Add(new MemoryConfigurationSource { InitialData = source })
            //    .Build();

            //var options = new FormatOptions(configuration.GetSection("Format"));
            //var dateTime = options.DateTime;
            //var currencyDecimal = options.CurrencyDecimal;

            var options = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false)
                .AddJsonFile($"appsettings.{environment}.json", true)
                .Build()
                .GetSection("format")
                .Get<FormatOptions>();
            var dateTime = options.DateTime;
            var currencyDecimal = options.CurrencyDecimal;

            Console.WriteLine("DateTime:");
            //Console.WriteLine($"\tLongDatePattern: {dateTime.LongDatePattern}");
            //Console.WriteLine($"\tLongTimePattern: {dateTime.LongTimePattern}");
            //Console.WriteLine($"\tShortDatePattern: {dateTime.ShortDatePattern}");
            //Console.WriteLine($"\tShortTimePattern: {dateTime.ShortTimePattern}");

            Console.WriteLine("CurrencyDecimal:");
            Console.WriteLine($"\tDigits:{currencyDecimal.Digits}");
            Console.WriteLine($"\tSymbol:{currencyDecimal.Symbol}");

            Console.WriteLine("=============================================");

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
            var options1 = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true)
                .Build()
                .GetSection("Demo")
                .Get<DemoOptions>();
            Console.WriteLine("=======================文件配置的读取======================");
            Console.WriteLine($"Name:{options1.Name}");
            Console.WriteLine($"Tag:{options1.Tag}");
            Console.WriteLine($"Max:{options1.Max}");

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
