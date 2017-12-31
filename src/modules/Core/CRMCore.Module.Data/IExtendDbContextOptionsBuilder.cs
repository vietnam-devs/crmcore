using Microsoft.EntityFrameworkCore;

namespace CRMCore.Module.Data
{
    public interface IExtendDbContextOptionsBuilder
    {
        DbContextOptionsBuilder Extend(DbContextOptionsBuilder optionsBuilder, string connectionString, string assemblyName);
    }
}
