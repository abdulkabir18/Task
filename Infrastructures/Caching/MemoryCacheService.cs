namespace Infrastructures.Caching
{
    using Application.Abstractions;
    using Microsoft.Extensions.Caching.Memory;

    using System;
    using System.Threading.Tasks;

    public class MemoryCacheService : IMemoryCacheService
    {
        private readonly IMemoryCache _cache;

        public MemoryCacheService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public Task<T?> GetAsync<T>(string key)
        {
            _cache.TryGetValue<T>(key, out var value);
            return Task.FromResult(value);
        }

        public Task SetAsync<T>(string key, T value, TimeSpan duration)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = duration
            };

            _cache.Set(key, value, cacheEntryOptions);
            return Task.CompletedTask;
        }

        public Task RemoveAsync(string key)
        {
            _cache.Remove(key);
            return Task.CompletedTask;
        }
    }


}
