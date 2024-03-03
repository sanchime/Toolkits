using Sanchime.EventFlows.Aggregates;

namespace Sanchime.EventFlows.Seedworks;

public interface IRepository<T> : IAggregateRoot
{
    IUnitOfWork UnitOfWork { get; }
}
