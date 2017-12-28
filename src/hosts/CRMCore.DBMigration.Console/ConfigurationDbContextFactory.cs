using CRMCore.Module.Data;
using CRMCore.Module.Data.SqlServer;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Reflection;

namespace CRMCore.DBMigration.Console
{
    public class ConfigurationDbContextFactory : IDesignTimeDbContextFactory<ConfigurationDbContext>
    {
        private readonly IExtendDbContextOptionsBuilder _extendOptionsBuilder;
        private readonly IDatabaseConnectionStringFactory _dbConnectionStringFactory;
        private readonly IConfiguration _config;

        public ConfigurationDbContextFactory()
        {
            _config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddUserSecrets<Startup>()
                .Build();

            _extendOptionsBuilder = new SqlServerDbContextOptionsBuilderFactory();
            _dbConnectionStringFactory = new SqlServerDatabaseConnectionStringFactory(_config);
        }

        public ConfigurationDbContext CreateDbContext(string[] args)
        {
            var migrationAssembly = typeof(ConfigurationDbContextFactory).GetTypeInfo().Assembly;

            var dbContextOptionBuilder = _extendOptionsBuilder.Extend(
                new DbContextOptionsBuilder<ConfigurationDbContext>(),
                _dbConnectionStringFactory.Create(),
                migrationAssembly.GetName().Name);

            return (ConfigurationDbContext)Activator.CreateInstance(
                typeof(ConfigurationDbContext),
                dbContextOptionBuilder.Options,
                new ConfigurationStoreOptions());
        }
    }
}
