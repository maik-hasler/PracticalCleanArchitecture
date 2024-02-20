using Domain.Common.Primitives;

namespace Application.Common.Abstractions.Caching;

public interface ICacheService
{
    Task<T> GetOrCreateAsync<T>(
        string key,
        Func<CancellationToken, ValueTask<T>> factory,
        TimeSpan? expiration = null,
        CancellationToken cancellationToken = default);
}
