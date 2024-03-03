using Sanchime.EventFlows.Events;

namespace Sanchime.EventFlows.Aggregates;

public interface IAggregateRoot
{
    void ApplyEvents(IReadOnlyCollection<IDomainEvent> domainEvents);
}


public interface IAggregateRoot<TKey> : IAggregateRoot
{
    TKey Id { get; }
}
