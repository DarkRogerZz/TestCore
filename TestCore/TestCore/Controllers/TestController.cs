using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NLog;
using TestCore.Services.IServices;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestCore.Controllers
{
    /// <summary>
    /// 测试
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ILogger<TestController> _logger;

        public TestController(ILogger<TestController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 获取选项配置
        /// </summary>
        [HttpGet]
        public void GetConfig([FromServices] IDemoService service)
        {
            service.GetDemoConfig();
        }

        /// <summary>
        /// 获取选项配置
        /// </summary>
        [HttpGet,Route("snapshot")]
        public void GetConfigBySnapshot([FromServices] IDemoService service)
        {
            service.GetDemoConfigBySnapshot();
        }

        /// <summary>
        /// 获取选项配置
        /// </summary>
        [HttpGet,Route("monitor")]
        public void GetConfigByMonitor([FromServices] IDemoService service)
        {
            service.GetConfigByMonitor();
        }

    


        /// <summary>
        /// 写日志
        /// </summary>
        [HttpPost]
        public void WriteLog()
        {
            _logger.LogCritical("是这Critical");
            _logger.LogInformation("这是Information");
            _logger.LogDebug("这是Debug");
            _logger.LogWarning("Warning");
            _logger.LogError("Error");
            _logger.LogTrace("Trace");

            LogFactory logger = new LogFactory();
        }

        // POST api/<TestController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<TestController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TestController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        
    }
}
