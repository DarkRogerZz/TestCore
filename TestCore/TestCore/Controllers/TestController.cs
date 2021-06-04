using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestCore.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {

        /// <summary>
        /// 问张三多大
        /// </summary>
        /// <returns></returns>
        [HttpGet,Route("lisi")]
        public string GetZhangsan()
        {
            var sb = new Lisi();
            var age = sb.Say();
            return age;
        }

        /// <summary>
        /// 获取张三多大
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("liqi")]
        public string GetZhangsanByLiqi()
        {
            var liqi = new Liqi();
            var age = liqi.Say();
            return age;
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
