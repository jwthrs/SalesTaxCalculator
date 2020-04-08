using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SalesTaxCalculator.Services;
using SalesTaxCalculator.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore.Design;

namespace SalesTaxCalculator
{
    /// <summary>
    /// Startup configures the server before it runs.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Sets configuration.
        /// </summary>
        /// <param name="configuration">Configuration object</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        /// <summary>
        /// Injects the needed controllers and services required to run the API endpoints.
        /// This method gets called at the runtime. 
        /// 
        /// </summary>
        /// <param name="services">Services to be configured.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddControllers();
            services.AddScoped<ISalesTaxMediator, SalesTaxMediator>();
            services.AddDbContext<ISalesTaxContext, SalesTaxContext>(options => 
                
                // ConnectionStrings:SalesTaxContext is a field in the appsettings.json which holds the database connection string.
                options.UseSqlServer(Configuration["ConnectionStrings:SalesTaxContext"])
            );
        }

        /// <summary>
        /// Configure HTTP request pipeline.
        /// </summary>
        /// <param name="app">Application Builder</param>
        /// <param name="env">WebHostEnvironment</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
