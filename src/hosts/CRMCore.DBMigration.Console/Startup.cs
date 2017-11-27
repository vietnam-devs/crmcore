using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using CRMCore.Module.Data;
using Microsoft.Extensions.Logging;

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

            // Adds IdentityServer             services.AddIdentityServer(x => x.IssuerUri = "null")                 .AddConfigurationStore(options =>                 {                     options.ConfigureDbContext = builder => builder.UseMySql(Configuration.GetConnectionString("Default"), mySqlOptionsAction: sqlOptions =>                         {                             sqlOptions.MigrationsAssembly("CRMCore.DBMigration.Console");                         });                 })                 .AddOperationalStore(options =>                 {                     options.ConfigureDbContext = builder => builder.UseMySql(Configuration.GetConnectionString("Default"), mySqlOptionsAction: sqlOptions =>                     {                         sqlOptions.MigrationsAssembly("CRMCore.DBMigration.Console");                     });                 });
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
        }
    }
}
