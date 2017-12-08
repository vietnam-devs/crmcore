using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CRMCore.Framework.Entities
{
    public interface IQueryFluent<TEntity, TResponse> where TEntity : EntityBase
    {
        IQueryFluent<TEntity, TResponse> OrderBy(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy);
        IQueryFluent<TEntity, TResponse> Include(Expression<Func<TEntity, object>> expression);
        IQueryFluent<TEntity, TResponse> Projection(Expression<Func<TEntity, TResponse>> selector);
        IQueryFluent<TEntity, TResponse> Criterion(Criterion criterion);
        Task<PaginatedItem<TResponse>> ComplexQueryAsync();
        Task<PaginatedItem<TResponse>> ComplexFindAllAsync();
        Task<TEntity> ComplexFindOneAsync();
    }
}
