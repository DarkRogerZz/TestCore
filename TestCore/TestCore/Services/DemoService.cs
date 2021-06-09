using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using TestCore.Services.IServices;

namespace TestCore.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class DemoService : IDemoService
    {
        private readonly IOptions<DemoOptions> _options;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public DemoService(IOptions<DemoOptions> options)
        {
            _options = options;
        }

        public void GetDemoConfig()
        {
            Console.WriteLine($"serviceName:{_options.Value.Name}");
        }
    }
}
