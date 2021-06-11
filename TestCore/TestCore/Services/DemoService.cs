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

        private readonly IOptionsMonitor<DemoOptions> _optionsMonitor;

        private readonly IOptionsSnapshot<DemoOptions> _optionsSnapshot;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public DemoService(IOptions<DemoOptions> options,
            IOptionsSnapshot<DemoOptions> optionsSnapshot,
            IOptionsMonitor<DemoOptions> optionsMonitor)
        {
            _options = options;
            _optionsSnapshot = optionsSnapshot;
            _optionsMonitor = optionsMonitor;
            //监听配置，配置改变时触发
            _optionsMonitor.OnChange(options =>
            {
                Console.WriteLine($"配置发生了变更，新值为{_optionsMonitor.CurrentValue.Max}");
            });

        }

        public void GetDemoConfig()
        {
            Console.WriteLine($"Max options:{_options.Value.Max}");
        }

        public void GetDemoConfigBySnapshot()
        {
            Console.WriteLine($"Max Snapshot:{_optionsSnapshot.Value.Max}");
        }

        public void GetConfigByMonitor()
        {
            try
            {

                Console.WriteLine($"Max:{_optionsMonitor.CurrentValue.Max}");
            }
            catch (OptionsValidationException optValEx)
            {
                Console.WriteLine(optValEx.Message);
            }
        }
    }
}
