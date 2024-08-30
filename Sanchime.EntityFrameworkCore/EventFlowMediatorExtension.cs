using Microsoft.EntityFrameworkCore;
using Sanchime.EventFlows;
using Sanchime.EventFlows.Entities;

namespace Sanchime.EntityFrameworkCore;

public static class EventFlowMediatorExtension
{
    public static async Task DispatchDomainEventsAsync(this IEventPublisher publisher, DbContext context, CancellationToken cancellationToken)
    {
        var domainEntities = context.ChangeTracker
            .Entries<Entity>()
            .Where(x => x.Entity.DomainEvents?.Count > 0);

        foreach (var entry in domainEntities)
        {
            var events = entry.Entity.DomainEvents.ToArray();
            // 避免递归执行事件，先清除
            entry.Entity.ClearDomainEvents();

            foreach (var e in events)
            {
                await publisher.Publish(e, cancellationToken).ConfigureAwait(false);
            }
        }
    }
}
