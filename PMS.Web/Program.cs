using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using System;
using System.IO;

namespace PMS.Web
{
    public class Program
    {
        [Obsolete]
        public static void Main(string[] args)
        {
            string dateTimeNowString = DateTime.Now.ToString("yyyyMMdd");
            Log.Logger = new LoggerConfiguration()
              .MinimumLevel.Debug()
              .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
              .WriteTo.Logger(
                  x => x.Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Error)
            .WriteTo.File($"SeriLogHandlers/Error/{dateTimeNowString}-Error.log", rollingInterval: RollingInterval.Day)
             )

           .WriteTo.Logger(
                 x => x.Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Debug)
           .WriteTo.File($"SeriLogHandlers/Debug/{dateTimeNowString}-Debug.log", rollingInterval: RollingInterval.Day)
            )
           .WriteTo.Console()
           .CreateLogger();
            CreateHostBuilder(args).Build().Run();
            var host=new WebHostBuilder().UseKestrel()
            .UseContentRoot(Directory.GetCurrentDirectory())
            .UseIISIntegration()            
            .Build();

            host.Run();
        }

        [Obsolete]
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.ConfigureAppConfiguration((hostingContext, config) =>
                    {
                        config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                        config.AddJsonFile("customSettings.json", optional: false, reloadOnChange: false);
                    })
                    .UseSerilog();
                });
    }
}
