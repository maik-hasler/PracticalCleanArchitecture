using Application.Common.Abstractions.Caching;
using Application.Common.Abstractions.Messaging;
using Mediator;

namespace Application.Common.Behaviors;

internal sealed class CachingBehavior<TRequest, TResponse>(
    ICacheService cachingService)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class, IMessage, ICachableQuery
    where TResponse : class
{
    private readonly ICacheService _cachingService = cachingService;

    public async ValueTask<TResponse> Handle(
        TRequest message,
        CancellationToken cancellationToken,
        MessageHandlerDelegate<TRequest, TResponse> next)
    {
        return await _cachingService.GetOrCreateAsync(
            message.Key,
            _ => next(message, cancellationToken),
            message.Expiration,
            cancellationToken);
    }
}
