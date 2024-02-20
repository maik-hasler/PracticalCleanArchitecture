using Domain.Common.Primitives;
using Mediator;

namespace Application.Common.Abstractions.Messaging;

public interface ICommandHandler<TCommand, TResponse>
    : IRequestHandler<TCommand, Result<TResponse>>
    where TCommand : ICommand<TResponse>;