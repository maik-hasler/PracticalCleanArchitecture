namespace Application.Common.Abstractions.Messaging;

public interface ICachableQuery<TResponse>
    : IQuery<TResponse>,
    ICachableQuery;

public interface ICachableQuery
{
    string Key { get; }

    TimeSpan? Expiration { get; }
}
