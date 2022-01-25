using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PM.CloudPlatform.ForkliftManager.Apis.Data;
using System;
using System.Threading.Tasks;

namespace PM.CloudPlatform.ForkliftManager.Apis
{
    /// <summary>
    /// </summary>
    public class Program
    {
        /// <summary>
        /// </summary>
        /// <param name="args"> </param>
        /// <returns> </returns>
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            //using (var scope = host.Services.CreateScope())
            //{
            //    try
            //    {
            //        var dbContext = scope.ServiceProvider.GetService<ForkliftManagerDbContext>();

            //        if (dbContext?.Database != null)
            //        {
            //            await dbContext.Database.EnsureDeletedAsync();
            //            await dbContext.Database.MigrateAsync();
            //        }
            //    }
            //    catch (Exception e)
            //    {
            //        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
            //        logger.LogError(e, "Database Migration Error!");
            //    }
            //}

            await host.RunAsync();
        }

        /// <summary>
        /// </summary>
        /// <param name="args"> </param>
        /// <returns> </returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseUrls("http://localhost:10808/");
                });
    }
}