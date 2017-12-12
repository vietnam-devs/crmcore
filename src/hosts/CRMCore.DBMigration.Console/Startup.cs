using CRMCore.Framework.Entities.Identity;
using CRMCore.Module.Data;
using CRMCore.Module.Identity.Services;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace CRMCore.DBMigration.Console
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        private IHostingEnvironment Environment { get; }

        public Startup(IHostingEnvironment env, IConfiguration config)
        {
            Environment = env;
            Configuration = config;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            Action<DbContextOptionsBuilder> optionsBuilderAction = (optionsBuilder) =>
            {
                if (!Environment.IsDevelopment())
                {
                    optionsBuilder.UseMySql(
                        Configuration.GetConnectionString("Default"), 
                        mySqlOptionsAction: sqlOptions =>
                        {
                            sqlOptions.MigrationsAssembly("CRMCore.DBMigration.Console");
                        });
                } else
                {
                    optionsBuilder.UseSqlServer(
                        Configuration.GetConnectionString("SqlServerDefault"),
                        sqlOptions =>
                        {
                            sqlOptions.MigrationsAssembly("CRMCore.DBMigration.Console");
                        });
                }
            };

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
                .Services.AddTransient<IProfileService, IdentityWithAdditionalClaimsProfileService>(); ;
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
        }
    }
}
