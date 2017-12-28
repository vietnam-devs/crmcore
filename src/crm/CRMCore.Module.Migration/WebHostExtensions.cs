using CRMCore.Module.Migration.Seeder;
using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace CRMCore.Module.Migration
{
    public static class WebHostExtensions
    {
        public static IWebHost DbContextRegistration(this IWebHost webHost)
        {
            return webHost
                .MigrateDbContext<PersistedGrantDbContext>((_, __) => { })
                .MigrateDbContext<ConfigurationDbContext>((context, services) =>
                {
                    var env = services.GetService<IHostingEnvironment>();
                    var logger = services.GetService<ILogger<ApplicationDbContextSeed>>();
                    var configuration = services.GetService<IConfiguration>();
                    new ConfigurationDbContextSeed()
                        .SeedAsync(context, configuration)
                        .Wait();
                })
                .MigrateDbContext<Data.ApplicationDbContext>((context, services) =>
                {
                    var env = services.GetService<IHostingEnvironment>();
                    var logger = services.GetService<ILogger<ApplicationDbContextSeed>>();

                    new ApplicationDbContextSeed()
                        .SeedAsync(context, env, logger)
                        .Wait();
                });
        }

        internal static IWebHost MigrateDbContext<TContext>(this IWebHost webHost, Action<TContext, IServiceProvider> seeder)
            where TContext : DbContext
        {
            using (var scope = webHost.Services.CreateScope())
            {
                scope.ServiceProvider.MigrateDbContext(seeder);
            }

            return webHost;
        }

        /// <summary>
        /// This function will open up the door to make the Setup page
        /// Because we can call to this function for provision new database
        /// </summary>
        /// <typeparam name="TContext"></typeparam>
        /// <param name="serviceProvider"></param>
        /// <param name="seeder"></param>
        /// <returns></returns>
        public static IServiceProvider MigrateDbContext<TContext>(
            this IServiceProvider serviceProvider, Action<TContext, 
                IServiceProvider> seeder)
            where TContext : DbContext
        {
            var logger = serviceProvider.GetRequiredService<ILogger<TContext>>();
            var context = serviceProvider.GetService<TContext>();

            try
            {
                logger.LogInformation($"Migrating database associated with context {typeof(TContext).Name}");

                context.Database.Migrate();

                seeder(context, serviceProvider);

                logger.LogInformation($"Migrated database associated with context {typeof(TContext).Name}");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"An error occurred while migrating the database used on context {typeof(TContext).Name}");
            }

            return serviceProvider;
        }
    }
}
