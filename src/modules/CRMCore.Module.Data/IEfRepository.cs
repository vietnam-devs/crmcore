using CRMCore.Framework.Entities;
using Microsoft.EntityFrameworkCore;

namespace CRMCore.Module.Data
{
    public interface IEfRepositoryAsync<TEntity> : IEfRepositoryAsync<ApplicationDbContext, TEntity>
        where TEntity : EntityBase
    {
    }

    public interface IEfQueryRepository<TEntity> : IEfQueryRepository<ApplicationDbContext, TEntity>
        where TEntity : EntityBase
    {
    }

    public interface IEfRepositoryAsync<TDbContext, TEntity> : IRepositoryAsync<TEntity>
        where TDbContext : DbContext
        where TEntity : EntityBase
    {
    }

    public interface IEfQueryRepository<TDbContext, TEntity> : IQueryRepository<TEntity>
        where TDbContext : DbContext
        where TEntity : EntityBase
    {
    }
}
