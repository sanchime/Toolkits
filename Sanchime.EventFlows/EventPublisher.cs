using Microsoft.Extensions.DependencyInjection;

namespace Sanchime.EventFlows;

/// <summary>
/// 事件分发器
/// </summary>
internal sealed class EventPublisher(IServiceProvider provider, IEventFlowPipelineDispatcher pipelineDispatcher) : IEventPublisher
{
    public async Task Publish<TEvent>(TEvent @event, CancellationToken cancellation = default) where TEvent : IEvent
    {
        var handlers = provider.GetServices<IEventHander<TEvent>>();
        if ((handlers?.Any()) is not true)
        {
            return;
        }
        foreach (var handler in handlers)
        {
            await pipelineDispatcher.Handle(@event, async (e, c) =>
            {
                await handler.Handle(e, c).ConfigureAwait(false);

                return Unit.Value;
            }, cancellation).ConfigureAwait(false);
        }
    }
}