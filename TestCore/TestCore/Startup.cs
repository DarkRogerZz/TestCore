using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.OpenApi.Models;
using TestCore.Services;
using TestCore.Services.IServices;

namespace TestCore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(u =>
            {
                u.ReturnHttpNotAcceptable = false;
                
            }).AddXmlDataContractSerializerFormatters();

            services.Configure<DemoOptions>(Configuration.GetSection("Demo"));
            services.AddSingleton<IDemoService, DemoService>();

            services.AddSwaggerGen(u =>
            {
                u.SwaggerDoc("v1",new OpenApiInfo{Title = "Test API",Version = "v1"});
                //获取xml文件名
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                //获取xml文件路径
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                //添加控制器层注释，true表示显示器控制器注释
                u.IncludeXmlComments(xmlPath,true);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(u => { u.SwaggerEndpoint("/swagger/v1/swagger.json","Test API v1");});

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }

    }
}
