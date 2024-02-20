using Application.Common.Abstractions.Persistence;
using Mediator;

namespace Application.Common.Behaviors;

internal sealed class TransactionBehavior<TRequest, TResponse>(
    IUnitOfWork unitOfWork)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class, IMessage, Abstractions.Messaging.ICommand
    where TResponse : class
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async ValueTask<TResponse> Handle(
        TRequest message,
        CancellationToken cancellationToken,
        MessageHandlerDelegate<TRequest, TResponse> next)
    {
        await using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);

        try
        {
            var response = await next(message, cancellationToken);

            await transaction.CommitAsync(cancellationToken);

            return response;
        }
        catch (Exception)
        {
            await transaction.RollbackAsync(cancellationToken);

            throw;
        }
    }
}
