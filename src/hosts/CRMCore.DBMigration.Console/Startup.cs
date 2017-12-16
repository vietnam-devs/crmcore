using CRMCore.Framework.MvcCore.Extensions;
using CRMCore.Module.Migration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
            services.AddScoped<IExtendDbContextOptionsBuilder, SqlServerDbContextOptionsBuilderFactory>();
            services.AddScoped<IDatabaseConnectionStringFactory, SqlServerDatabaseConnectionStringFactory>();
            services.AddMvcModules();
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
        }
    }

    internal sealed class SqlServerDbContextOptionsBuilderFactory : IExtendDbContextOptionsBuilder
    {
        public DbContextOptionsBuilder Extend(DbContextOptionsBuilder optionsBuilder, string connectionString, string assemblyName)
        {
            return optionsBuilder.UseSqlServer(
                connectionString,
                sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(assemblyName);
                });
        }
    }

    internal sealed class SqlServerDatabaseConnectionStringFactory : IDatabaseConnectionStringFactory
    {
        private readonly IConfiguration _config;

        public SqlServerDatabaseConnectionStringFactory(IConfiguration config)
        {
            _config = config;
        }

        public string Create()
        {
            return _config.GetConnectionString("SqlServerDefault");
        }
    }
}
