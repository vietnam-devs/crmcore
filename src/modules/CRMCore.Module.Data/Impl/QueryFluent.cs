using CRMCore.Framework.Entities;
using CRMCore.Module.Data.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CRMCore.Module.Data.Impl
{
    public class QueryFluent<TEntity, TResponse> : IQueryFluent<TEntity, TResponse>
        where TEntity : EntityBase
    {
        public readonly Expression<Func<TEntity, bool>> _filter;
        public readonly List<Expression<Func<TEntity, object>>> _includes;
        public Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> _orderBy;
        public Expression<Func<TEntity, TResponse>> _selector;
        public Criterion _criterion;
        private readonly IEfQueryRepository<TEntity> _repository;

        public QueryFluent(IEfQueryRepository<TEntity> repository)
        {
            _repository = repository;
            _includes = new List<Expression<Func<TEntity, object>>>();
        }

        public QueryFluent(IEfQueryRepository<TEntity> repository, IQueryObject<TEntity> queryObject) 
            : this(repository) { _filter = queryObject.Query(); }

        public QueryFluent(IEfQueryRepository<TEntity> repository, Expression<Func<TEntity, bool>> filter) 
            : this(repository) { _filter = filter; }

        public IQueryFluent<TEntity, TResponse> Include(Expression<Func<TEntity, object>> expression)
        {
            _includes.Add(expression);
            return this;
        }

        public IQueryFluent<TEntity, TResponse> Criterion(Criterion criterion)
        {
            _criterion = criterion;
            return this;
        }

        public IQueryFluent<TEntity, TResponse> OrderBy(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy)
        {
            _orderBy = orderBy;
            return this;
        }

        public IQueryFluent<TEntity, TResponse> Projection(Expression<Func<TEntity, TResponse>> selector)
        {
            _selector = selector;
            return this;
        }

        public async Task<PaginatedItem<TResponse>> ComplexQueryAsync()
        {
            return await _repository.QueryAsync(_criterion, _selector, _includes.ToArray());
        }

        public async Task<PaginatedItem<TResponse>> ComplexFindAllAsync()
        {
            if (_filter == null)
            {
                throw new InvalidOperationException("Need to set expression for the query.");
            }

            return await _repository.FindAllAsync(_criterion, _selector, _filter, _includes.ToArray());
        }

        public async Task<TEntity> ComplexFindOneAsync()
        {
            if (_filter == null)
            {
                throw new InvalidOperationException("Need to set expression for the query.");
            }

            return await _repository.FindOneAsync(_filter, _includes.ToArray());
        }
    }
}
