using Mediator;
using Microsoft.Extensions.Logging;

namespace Application.Common.Behaviors;

internal sealed class LoggingBehavior<TRequest, TResponse>(
    ILogger<TRequest> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class, IMessage
    where TResponse : class
{
    private readonly ILogger<TRequest> _logger = logger;

    public async ValueTask<TResponse> Handle(
        TRequest message,
        CancellationToken cancellationToken,
        MessageHandlerDelegate<TRequest, TResponse> next)
    {
        var requestName = typeof(TRequest).Name;

        _logger.LogInformation(
            "Request started: {Name} {@Request}",
            requestName,
            message);

        var response = await next(message, cancellationToken);

        _logger.LogInformation(
            "Request ended: {Name} {@Request}",
            requestName,
            message);

        return response;
    }
}
