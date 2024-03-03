using Microsoft.Extensions.DependencyInjection;

namespace Sanchime.EventFlows;

/// <summary>
/// 事件分发器
/// </summary>
internal sealed class EventPublisher(IServiceProvider serviceProvider) : IEventPublisher
{
    public async Task Publish<TEvent>(TEvent @event, CancellationToken cancellation = default) where TEvent : IEvent
    {
        await using var scope = serviceProvider.CreateAsyncScope();
        var handlers = scope.ServiceProvider.GetServices<IEventHander<TEvent>>();
        if ((handlers?.Any()) is not true)
        {
            return;
        }
        foreach (var handler in handlers)
        {
            await handler.Handle(@event, cancellation).ConfigureAwait(false);
        }
    }
}