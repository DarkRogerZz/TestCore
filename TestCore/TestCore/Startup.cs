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
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Microsoft.OpenApi.Models;
using TestCore.Extension;
using TestCore.Services;
using TestCore.Services.IServices;

namespace TestCore
{
    public class Startup
    {
        private void HandleBranchAndRejoin(IApplicationBuilder app, ILogger<Startup> logger)
        {
            app.Use(async (context, next) =>
            {
                var branchVer = context.Request.Query["branch"];
                logger.LogInformation("Branch used = {branchVer}", branchVer);

            // Do work that doesn't write to the Response.
            await next();
            // Do other work that doesn't write to the Response.
        });
        }
        private static void HandleMapTest1(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                await context.Response.WriteAsync("Map Test 1");
            });
        }

        private static void HandleMapTest2(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                await context.Response.WriteAsync("Map Test 2");
            });
        }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ChangeToken.OnChange(() => Configuration.GetReloadToken(), () =>
              {
                  Console.WriteLine("重新加载配置。");
              });
            services.AddDemoService(Configuration.GetSection("Demo"));
            services.AddOptions<DemoOptions>().Bind(Configuration.GetSection(DemoOptions.MyConfig)).ValidateDataAnnotations();
            services.AddControllers(u =>
            {
                u.ReturnHttpNotAcceptable = false;

            }).AddXmlDataContractSerializerFormatters();

            services.AddSwaggerGen(u =>
            {
                u.SwaggerDoc("v1", new OpenApiInfo { Title = "Test API", Version = "v1" });
                //获取xml文件名
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                //获取xml文件路径
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                //添加控制器层注释，true表示显示器控制器注释
                u.IncludeXmlComments(xmlPath, true);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            app.Map("/map1", HandleMapTest1);

            app.Map("/map2", HandleMapTest2);

            app.UseWhen(context => context.Request.Query.ContainsKey("branch"),
                appBuilder => HandleBranchAndRejoin(appBuilder, logger));

            app.Run(async context =>
            {
                Console.WriteLine("Main:Hello from main pipeline.");
                await context.Response.WriteAsync("Hello from main pipeline.");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();

            app.UseSwaggerUI(u => { u.SwaggerEndpoint("/swagger/v1/swagger.json", "Test API v1"); });
        }

    }
}
