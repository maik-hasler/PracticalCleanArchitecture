using Domain.Common.Primitives;
using Mediator;

namespace Application.Common.Abstractions.Messaging;

public interface IQueryHandler<TQuery, TResponse>
    : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : ICachableQuery<TResponse>;
