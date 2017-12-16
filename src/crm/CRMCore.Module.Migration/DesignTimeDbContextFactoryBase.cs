using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Reflection;

namespace CRMCore.Module.Migration
{
    public abstract class DesignTimeDbContextFactoryBase<TDbConext> : IDesignTimeDbContextFactory<TDbConext>
        where TDbConext : DbContext
    {
        protected readonly IConfiguration Config;
        protected IExtendDbContextOptionsBuilder ExtendOptionsBuilder;
        protected IDatabaseConnectionStringFactory DbConnectionStringFactory;

        protected DesignTimeDbContextFactoryBase()
        {
            Config = BuildConfig();
            ExtendOptionsBuilder = BuildExtendOptionsBuilder();
            DbConnectionStringFactory = BuildDbConnectionStringFactory();
        }

        public abstract IConfiguration BuildConfig();
        public abstract IExtendDbContextOptionsBuilder BuildExtendOptionsBuilder();
        public abstract IDatabaseConnectionStringFactory BuildDbConnectionStringFactory();

        public abstract dynamic SetOptions();

        public TDbConext CreateDbContext(string[] args)
        {
            var migrationAssembly = Assembly.GetEntryAssembly();

            var dbContextOptionBuilder = ExtendOptionsBuilder.Extend(
                new DbContextOptionsBuilder<TDbConext>(),
                DbConnectionStringFactory.Create(),
                migrationAssembly.GetName().Name);

            if (SetOptions() == null)
            {
                return (TDbConext)Activator.CreateInstance(
                    typeof(TDbConext),
                    dbContextOptionBuilder.Options);
            }

            return (TDbConext)Activator.CreateInstance(
                typeof(TDbConext),
                dbContextOptionBuilder.Options,
                SetOptions());
        }
    }
}
