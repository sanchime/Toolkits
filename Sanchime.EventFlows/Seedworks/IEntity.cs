namespace Sanchime.EventFlows.Seedworks;

public interface IEntity
{
    bool IsTransient();
}

public interface IEntity<TKey> : IEntity
{
}