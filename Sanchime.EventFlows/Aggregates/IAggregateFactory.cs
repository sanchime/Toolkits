namespace Sanchime.EventFlows.Aggregates;

public interface IAggregateFactory
{
    Task<TAggregate> CreateNewAggregateAsync<TAggregate, TKey>(TKey id)
        where TAggregate : IAggregateRoot<TKey>;
}
