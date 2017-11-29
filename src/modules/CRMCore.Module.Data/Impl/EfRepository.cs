using CRMCore.Framework.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CRMCore.Module.Data.Impl
{
    public class EfRepository<TEntity> : EfRepository<ApplicationDbContext, TEntity>, IEfRepository<TEntity>
        where TEntity : EntityBase
    {
        public EfRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }

    public class EfRepository<TDbContext, TEntity> : IEfRepository<TDbContext, TEntity>
        where TDbContext : DbContext
        where TEntity : EntityBase
    {
        public TDbContext DbContext { get; private set; }

        public EfRepository(TDbContext dbContext)
        {
            DbContext = dbContext;
            DbContext.ChangeTracker.AutoDetectChangesEnabled = false;
        }

        public virtual async Task<TEntity> GetByIdAsync(Guid id, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var queryable = DbContext.Set<TEntity>().AsNoTracking() as IQueryable<TEntity>;

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    queryable = queryable.Include(includeProperty);
                }
            }

            return await queryable.SingleOrDefaultAsync(e => e.Id.Equals(id));
        }

        public async Task<IReadOnlyList<TEntity>> ListAsync(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var queryable = DbContext.Set<TEntity>().AsNoTracking() as IQueryable<TEntity>;

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    queryable = queryable.Include(includeProperty);
                }
            }

            return await queryable.ToListAsync();
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            var dbEntityEntry = DbContext.Entry(entity);
            await DbContext.Set<TEntity>().AddAsync(entity);
            await DbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> DeleteAsync(TEntity entity)
        {
            DbContext.Entry(entity).State = EntityState.Deleted;
            await DbContext.SaveChangesAsync();
            return await Task.FromResult(entity);
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            DbContext.Entry(entity).State = EntityState.Modified;
            await DbContext.SaveChangesAsync();
            return await Task.FromResult(entity);
        }
    }
}
