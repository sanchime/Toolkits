namespace Sanchime.EventFlows;

public interface IEvent;

public interface IEventHander<TEvent>
    where TEvent : IEvent
{
    Task Handle(TEvent @event, CancellationToken cancellation = default);
}

/// <summary>
/// 事件分发器
/// </summary>
public interface IEventPublisher
{
    Task Publish<TEvent>(TEvent @event, CancellationToken cancellation = default)
        where TEvent : IEvent;
}