using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.NETcore2.Data;
using API.NETcore2.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace API.NETcore2._0
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {   
            services.AddMvc();
            services.AddResponseCompression(); //adicionar comprensão 
            services.AddScoped<StoreDataContext, StoreDataContext>(); //AddScoped só vai enviar uma vez (Singleton)
            services.AddTransient<ProductRepository, ProductRepository>();//AddTransient vai enviar novos quantos pedirem 
            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Abner API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            app.UseResponseCompression();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Minha API V1");
            });

            /**app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });**/
        }
    }
}
