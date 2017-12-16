using CRMCore.Module.Migration;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace CRMCore.DBMigration.Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).DbContextRegistration();
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
