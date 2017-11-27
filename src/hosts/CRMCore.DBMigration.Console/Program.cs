using System.IO;
using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace CRMCore.DBMigration.Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args)
                .MigrateDbContext<PersistedGrantDbContext>((_, __) => { })
                .MigrateDbContext<ConfigurationDbContext>((_, __) => { })
                .MigrateDbContext<Module.Data.ApplicationDbContext>((_, __) => { });
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>()
                .ConfigureAppConfiguration((hostContext, options) =>
                {
                    options.Sources.Clear();
                    options.AddJsonFile("appsettings.json", true, true);
                    options.AddJsonFile($"appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json", true);
                    if (hostContext.HostingEnvironment.IsDevelopment())
                    {
                        options.AddUserSecrets<Startup>();
                    }
                    options.AddEnvironmentVariables();
                })
                .Build();
    }
}
