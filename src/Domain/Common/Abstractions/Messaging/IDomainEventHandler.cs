using Mediator;

namespace Domain.Common.Abstractions.Messaging;

public interface IDomainEventHandler<in TDomainEvent>
    : INotificationHandler<TDomainEvent>
    where TDomainEvent : IDomainEvent;
