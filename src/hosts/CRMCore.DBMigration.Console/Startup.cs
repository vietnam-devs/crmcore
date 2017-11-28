using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using CRMCore.Module.Data;
using Microsoft.Extensions.Logging;
using IdentityServer4.Services;
using CRMCore.Module.Identity.Services;
using CRMCore.Framework.Entities.Identity;
using Microsoft.AspNetCore.Identity;

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
            services.AddDbContext<ApplicationDbContext>(options => options.UseMySql(Configuration.GetConnectionString("Default"), mySqlOptionsAction: sqlOptions=>{
                sqlOptions.MigrationsAssembly("CRMCore.DBMigration.Console");
            }));

            services.AddIdentity<ApplicationUser, ApplicationRole>()
              .AddEntityFrameworkStores<ApplicationDbContext>()
              .AddDefaultTokenProviders();

            // Adds IdentityServer             services.AddIdentityServer(x => x.IssuerUri = "null")
                .AddAspNetIdentity<ApplicationUser>()                 .AddConfigurationStore(options =>                 {                     options.ConfigureDbContext = builder => builder.UseMySql(Configuration.GetConnectionString("Default"), mySqlOptionsAction: sqlOptions =>                         {                             sqlOptions.MigrationsAssembly("CRMCore.DBMigration.Console");                         });                 })                 .AddOperationalStore(options =>                 {                     options.ConfigureDbContext = builder => builder.UseMySql(Configuration.GetConnectionString("Default"), mySqlOptionsAction: sqlOptions =>                     {                         sqlOptions.MigrationsAssembly("CRMCore.DBMigration.Console");                     });                 })
                    .Services.AddTransient<IProfileService, IdentityWithAdditionalClaimsProfileService>();;
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
        }
    }
}
