using Sanchime.EventFlows;
using Sanchime.EventFlows.Entities;

namespace Sanchime.EntityFrameworkCore;

public static class EventFlowMediatorExtension
{
    public static async Task DispatchDomainEventsAsync(this IEventFlowMediator mediator, SanchimeDbContext context)
    {
        var domainEntities = context.ChangeTracker
            .Entries<Entity>()
            .Where(x => x.Entity.DomainEvents?.Count > 0);

        var domainEvents = domainEntities
            .SelectMany(x => x.Entity.DomainEvents);

        domainEntities.ToList()
            .ForEach(entity => entity.Entity.ClearDomainEvents());

        foreach (var domainEvent in domainEvents)
            await mediator.Publish(domainEvent);
    }
}
