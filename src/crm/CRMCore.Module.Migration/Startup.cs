using CRMCore.Framework.Entities.Identity;
using CRMCore.Framework.MvcCore;
using CRMCore.Module.Data;
using CRMCore.Module.Identity.Services;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace CRMCore.Module.Migration
{
    public interface IExtendDbContextOptionsBuilder
    {
        DbContextOptionsBuilder Extend(DbContextOptionsBuilder optionsBuilder, string connectionString, string assemblyName);
    }

    public interface IDatabaseConnectionStringFactory
    {
        string Create();
    }

    public class Startup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            var env = services.BuildServiceProvider().GetService<Microsoft.AspNetCore.Hosting.IHostingEnvironment>();
            var config = services.BuildServiceProvider().GetService<IConfiguration>();
            var extendOptionsBuilder = services.BuildServiceProvider().GetService<IExtendDbContextOptionsBuilder>();
            var dbConnectionStringFactory = services.BuildServiceProvider().GetService<IDatabaseConnectionStringFactory>();

            Action<DbContextOptionsBuilder> optionsBuilderAction =
                (optionsBuilder) =>
                {
                    var assemblyName = Assembly.GetEntryAssembly().GetName().Name;
                    var connectionString = dbConnectionStringFactory.Create();
                    extendOptionsBuilder.Extend(optionsBuilder, connectionString, assemblyName);
                };

            // Adds DbContexts
            services.AddDbContext<ApplicationDbContext>(options => optionsBuilderAction(options));
            services.AddIdentity<ApplicationUser, ApplicationRole>()
              .AddEntityFrameworkStores<ApplicationDbContext>()
              .AddDefaultTokenProviders();
            
            // Adds IdentityServer
            services.AddIdentityServer(x => x.IssuerUri = "null")
                .AddAspNetIdentity<ApplicationUser>()
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = builder => optionsBuilderAction(builder);
                })
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = builder => optionsBuilderAction(builder);
                })
                .Services.AddTransient<IProfileService, IdentityWithAdditionalClaimsProfileService>();

            base.ConfigureServices(services);
        }
    }
}
