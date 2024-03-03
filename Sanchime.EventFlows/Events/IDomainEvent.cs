using Sanchime.EventFlows.Aggregates;

namespace Sanchime.EventFlows.Events;

public interface IDomainEvent : IEvent
{
    Type AggregateType { get; }

    Type EventType { get; }

    int AggregateSequenceNumber { get; }

    DateTimeOffset Timestamp { get; }

    IAggregateEvent GetAggregateEvent();
}

public interface IDomainEvent<TAggregate, out TKey> : IDomainEvent
        where TAggregate : IAggregateRoot<TKey>
{
    TKey AggregateIdentity { get; }
}

public interface IDomainEvent<TAggregate, out TKey, out TAggregateEvent> : IDomainEvent<TAggregate, TKey>
        where TAggregate : IAggregateRoot<TKey>
        where TAggregateEvent : IAggregateEvent<TAggregate, TKey>
{
    TAggregateEvent AggregateEvent { get; }
}