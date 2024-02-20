using Application.Common.Abstractions.Caching;
using Microsoft.Extensions.Caching.Memory;

namespace Infrastructure.Caching;

internal sealed class CacheService(
    IMemoryCache memoryCache)
    : ICacheService
{
    private static readonly TimeSpan DefaultExpiration = TimeSpan.FromMinutes(5);

    private readonly IMemoryCache _memoryCache = memoryCache;

    public async Task<T> GetOrCreateAsync<T>(
        string key,
        Func<CancellationToken, ValueTask<T>> factory,
        TimeSpan? expiration = null,
        CancellationToken cancellationToken = default)
    {
        if (_memoryCache.TryGetValue(key, out T? result))
        {
            return result!;
        }

        var valueTaskResult = await factory(cancellationToken);

        _memoryCache.Set(
            key,
            valueTaskResult, 
            expiration ?? DefaultExpiration);

        return valueTaskResult;
    }
}
