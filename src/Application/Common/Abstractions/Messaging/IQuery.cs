using Domain.Common.Primitives;
using Mediator;

namespace Application.Common.Abstractions.Messaging;

public interface IQuery<TResponse>
    : IRequest<Result<TResponse>>;
