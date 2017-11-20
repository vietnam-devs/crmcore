using System;
using System.Threading.Tasks;
using CRMCore.Framework.CqrsLite.Domain;
using Microsoft.Extensions.Caching.Memory;

namespace CRMCore.Framework.CqrsLite.Caching
{
    /// <summary>
    /// A cache implementation that has cache in memory and 15 minutes sliding expiration.
    /// </summary>
    public class MemoryCache : ICache
    {
        private readonly MemoryCacheEntryOptions _cacheOptions;
        private readonly IMemoryCache _cache;

        public MemoryCache()
        {

            _cacheOptions = new MemoryCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromMinutes(15)
            };
            _cache = new Microsoft.Extensions.Caching.Memory.MemoryCache(new MemoryCacheOptions());

        }

        public Task<bool> IsTracked(Guid id)
        {
            return Task.FromResult(_cache.TryGetValue(id, out var o) && o != null);
        }

        public Task Set(Guid id, AggregateRoot aggregate)
        {
            _cache.Set(id, aggregate, _cacheOptions);
            return Task.FromResult(0);
        }

        public Task<AggregateRoot> Get(Guid id)
        {
            return Task.FromResult((AggregateRoot)_cache.Get(id));
        }

        public Task Remove(Guid id)
        {
            _cache.Remove(id);
            return Task.FromResult(0);
        }

        public void RegisterEvictionCallback(Action<Guid> action)
        {
            _cacheOptions.RegisterPostEvictionCallback((key, value, reason, state) =>
            {
                action.Invoke((Guid)key);
            });
        }
    }
}
