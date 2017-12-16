using CRMCore.Module.Migration;
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
    public class PersistedGrantDbContextFactory : IDesignTimeDbContextFactory<PersistedGrantDbContext>
    {
        private readonly IExtendDbContextOptionsBuilder _extendOptionsBuilder;
        private readonly IDatabaseConnectionStringFactory _dbConnectionStringFactory;
        private readonly IConfiguration _config;

        public PersistedGrantDbContextFactory()
        {
            _config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddUserSecrets<Startup>()
                .Build();

            _extendOptionsBuilder = new SqlServerDbContextOptionsBuilderFactory();
            _dbConnectionStringFactory = new SqlServerDatabaseConnectionStringFactory(_config);
        }

        public PersistedGrantDbContext CreateDbContext(string[] args)
        {
            var migrationAssembly = typeof(PersistedGrantDbContextFactory).GetTypeInfo().Assembly;

            var dbContextOptionBuilder = _extendOptionsBuilder.Extend(
                new DbContextOptionsBuilder<PersistedGrantDbContext>(),
                _dbConnectionStringFactory.Create(),
                migrationAssembly.GetName().Name);

            return (PersistedGrantDbContext)Activator.CreateInstance(
                typeof(PersistedGrantDbContext),
                dbContextOptionBuilder.Options,
                new OperationalStoreOptions());
        }
    }
}
