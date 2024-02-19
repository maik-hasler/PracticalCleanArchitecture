using Domain.Common.Abstractions.Messaging;

namespace Domain.Common.Primitives;

public abstract class AggregateRoot<TId>
    : Entity<TId>
    where TId : struct
{
    private readonly List<IDomainEvent> _domainEvents = [];

    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void ClearDomainEvents() => _domainEvents.Clear();

    protected void AddDomainEvent(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);
}