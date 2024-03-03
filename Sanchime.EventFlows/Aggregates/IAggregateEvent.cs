namespace Sanchime.EventFlows.Aggregates;

public interface IAggregateEvent : IEvent
{
}


public interface IAggregateEvent<TAggregate, TKey> : IAggregateEvent
       where TAggregate : IAggregateRoot<TKey>
{
}