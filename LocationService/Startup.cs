using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using LocationService.Repositorys;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using log4net;
using Microsoft.Extensions.Configuration;

namespace LocationService
{
    public class Startup
    {
        public static IConfigurationRoot Configuration { get; set; }

        public Startup()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true);

            Configuration = builder.Build();
        }

      

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            var transient = true;
            if (Configuration.GetSection("transient") != null)
            {
                transient = Boolean.Parse(Configuration.GetSection("transient").Value);
            }
            if (transient)
            {
                Logger.Info("Using transient location record repository.");
                services.AddScoped<ILocationRecordRepository, InMemoryLacationRecordRepository>();
            }
            else
            {
                var connectionString = Configuration.GetSection("ConnectionStrings:SqlServerConnection").Value;

                services.AddDbContext<LocationDbContext>();

                Logger.Info($"Using '{connectionString}' for DB connection string." );
               
                services.AddScoped<ILocationRecordRepository, LocationRecordRepository>();
            }

            var locationUrl = Configuration.GetSection("location:url").Value;
            Logger.Info($"Using{locationUrl} for location service URL");





        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory log)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");

                app.UseHsts();
            }
            log.AddLog4Net();
            
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
