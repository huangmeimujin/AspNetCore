using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LocationReports.Events;
using LocationReports.Models;
using LocationReports.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LocationReports
{
    public class Startup
    {
        public static IConfigurationRoot Configuration { get; set; }
        public Startup(IWebHostEnvironment env)
        {
            var buider = new ConfigurationBuilder()
                //.SetBasePath(env.ContentRootPath)
                //.AddIniFile("appsettings.json", true)
                //.AddEnvironmentVariables();
                //用上面的方式会报System.FormatException,原因等有空在查

                .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true);

            Configuration = buider.Build();

        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddOptions();
            services.Configure<AMQPOptions>(Configuration.GetSection("amqp"));
            services.Configure<TeamServiceOptions>(Configuration.GetSection("teamservice"));
            services.AddSingleton(typeof(IEventEmitter),typeof(AMQPEventEmitter));
            services.AddSingleton(typeof(ICommandEventConverter),typeof(CommandEventConverter));
            services.AddSingleton(typeof(ITeamServiceClient),typeof(HttpTeamServiceClient));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
