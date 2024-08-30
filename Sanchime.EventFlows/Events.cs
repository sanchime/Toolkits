namespace Sanchime.EventFlows;

public interface IEvent;

/// <summary>
/// 事件处理
/// </summary>
/// <typeparam name="TEvent"></typeparam>
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