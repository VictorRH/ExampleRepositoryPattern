using ExampleRepositoryPattern.BusinessLogic.Data;
using ExampleRepositoryPattern.BusinessLogic.Logic;
using ExampleRepositoryPattern.Core.Interfaz;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExampleRepositoryPattern
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

            //services.AddTransient(typeof(IGenericRepository<>), (typeof(GenericRepository<>)));
            services.AddScoped(typeof(IGenericRepository<>), (typeof(GenericRepository<>)));



            services.AddDbContext<RepositoryPatternDbContext>(options =>
            {
                options.EnableSensitiveDataLogging().UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddControllers();

            services.AddSwaggerGen(x=> {
                x.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { 
                    Title = "Example Repository Pattern",
                    Version = "v1"
                });
                x.CustomSchemaIds(x=>x.FullName);
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
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
            app.UseSwaggerUI(x=> {
                x.SwaggerEndpoint("v1/swagger.json", "Example Repository Patter");
            });

        }
    }
}
