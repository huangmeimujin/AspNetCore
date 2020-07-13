using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore;

namespace TeamService
{
    class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();

        //static void Main(string[] args)
        //{
        //    var config=new ConfigurationBuilder()
        //    .AddCommandLine(args)
        //    .Build();

        //    var host=new WebHostBuilder()
        //    .UseKestrel()
        //    .UseStartup<Startup>()
        //    .UseConfiguration(config)
        //    .Build();

        //    host.Run();
        //}

    }
}
