using System.Diagnostics;
using Mediator;
using Microsoft.Extensions.Logging;

namespace Application.Common.Behaviors;

internal sealed class PerformanceBehavior<TRequest, TResponse>(
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
        var startTime = Stopwatch.GetTimestamp();

        var response = await next(message, cancellationToken);

        var elapsedTime = Stopwatch.GetElapsedTime(startTime);

        if (elapsedTime.TotalMilliseconds > 500)
        {
            var requestName = typeof(TRequest).Name;

            _logger.LogWarning(
                "Slow Request: {Name} ({ElapsedMilliseconds} milliseconds) {@Request}",
                requestName,
                elapsedTime.TotalMilliseconds,
                message);
        }

        return response;
    }
}
