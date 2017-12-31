using Microsoft.EntityFrameworkCore;

namespace CRMCore.Module.Data.SqlServer
{
    public sealed class SqlServerDbContextOptionsBuilderFactory : IExtendDbContextOptionsBuilder
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
}
