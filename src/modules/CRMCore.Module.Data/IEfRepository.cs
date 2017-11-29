using CRMCore.Framework.Entities;
using Microsoft.EntityFrameworkCore;

namespace CRMCore.Module.Data
{
    public interface IEfRepository<TEntity> : IEfRepository<ApplicationDbContext, TEntity>
        where TEntity : EntityBase
    {
    }

    public interface IEfRepository<TDbContext, TEntity> : IRepository<TEntity>
        where TDbContext : DbContext
        where TEntity : EntityBase
    {
        TDbContext DbContext { get; }
    }
}
