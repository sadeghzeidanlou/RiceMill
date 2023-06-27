using Microsoft.Extensions.Caching.Memory;
using RiceMill.Application.Common.Interfaces;

namespace RiceMill.Infrastructure.Caching
{
    public class MemoryCacheService : ICacheService
    {
        private readonly IMemoryCache _cache;

        public MemoryCacheService(IMemoryCache cache) => _cache = cache;

        public T? Get<T>(string key) => _cache.Get<T>(key);

        public void Set<T>(string key, T value)
        {
            if (_cache.TryGetValue(key, out _))
            {
                Remove(key);
                _cache.Set(key, value);
                return;
            }
            _cache.Set(key, value);
        }

        public void Remove(string key) => _cache.Remove(key);
    }
}
