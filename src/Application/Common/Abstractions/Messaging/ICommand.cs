using Domain.Common.Primitives;
using Mediator;

namespace Application.Common.Abstractions.Messaging;

public interface ICommand<TResponse>
    : IRequest<Result<TResponse>>,
    ICommand;

public interface ICommand;
