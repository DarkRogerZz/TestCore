using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
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

        public string GetDate()
        {
            var time = string.Empty;
            time += "===》》" + DateTime.Now.ToString(CultureInfo.DefaultThreadCurrentUICulture);
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
