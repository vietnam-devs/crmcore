using CRMCore.Framework.Entities;
using Microsoft.EntityFrameworkCore;

namespace CRMCore.Module.Data
{
    public interface IEfRepositoryAsync<TEntity> : IEfRepositoryAsync<ApplicationDbContext, TEntity>
        where TEntity : EntityBase
    {
    }

    public interface IEfRepositoryAsync<TDbContext, TEntity> : IRepositoryAsync<TEntity>
        where TDbContext : DbContext
        where TEntity : EntityBase
    {
        TDbContext DbContext { get; }
    }
}
