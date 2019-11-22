using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Web.Api.Infrastructure.Identity;
using Web.Api.Infrastructure.Data;

namespace Web.Api
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();
            //host.MigrateDbContext<AppIdentityDbContext>((_, __) => { });
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();
                try
                {
                    var userManager = services.GetRequiredService<UserManager<AppUser>>();
                    await AppIdentityDbContextSeed.SeedAsync(userManager);

                    var appDbContext = services.GetRequiredService<AppDbContext>();
                    await AppDbContextSeed.SeedAsync(appDbContext, loggerFactory, userManager);

                } catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger<Program>();
                    logger.LogError(ex, "An error occurred seeding the DB.");
                }
            }
            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseUrls("http://0.0.0.0:5100");
    }
}
