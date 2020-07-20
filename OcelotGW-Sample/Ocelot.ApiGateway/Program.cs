using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace Ocelot.ApiGateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            new WebHostBuilder()
                //.UseUrls("http://localhost:2222;https://*:3333")
                
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config
                        .SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
                        .AddJsonFile("appsettings.json", true, true)
                        //.AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true)
                        .AddJsonFile("ocelot.json",false, true)
                        .AddEnvironmentVariables();

                    
                })
                
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .ConfigureServices(s =>
                {
                    s.AddOcelot();
                })
                .ConfigureLogging((hostingContext, config) =>
                {

                })
                .UseIISIntegration()
                .Configure(app =>
                {
                    app.UseOcelot().Wait();
                })
                .Build().Run();
        }

    }
}
    
