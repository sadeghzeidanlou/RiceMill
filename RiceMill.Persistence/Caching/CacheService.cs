using Microsoft.Extensions.Caching.Memory;
using RiceMill.Application.Common.Interfaces;

namespace RiceMill.Persistence.Caching
{
    public class CacheService : ICacheService
    {
        private readonly IMemoryCache _cache;

        public CacheService(IMemoryCache cache) => _cache = cache;

        public T Get<T>(string key) => _cache.Get<T>(key);

        public void Set<T>(string key, T value) => _cache.Set(key, value, TimeSpan.MinValue);

        public void Remove(string key) => _cache.Remove(key);
    }
}