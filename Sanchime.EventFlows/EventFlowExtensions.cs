using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Sanchime.EventFlows;

public static class EventFlowExtensions
{
    public static IServiceCollection AddEventFlow(this IServiceCollection services, Action<EventFlowOption>? configer = null)
    {
        RegisterDispatchers(services);

        services.TryAddSingleton<IEventFlowMediator, EventFlowMediator>();

        var options = new EventFlowOption(services);

        configer?.Invoke(options);

        RegisterHandlers(services, options);

        return services;
    }

    private static void RegisterDispatchers(IServiceCollection services)
    {
        services.TryAddSingleton<ICommandExecuter, CommandExecuter>();
        services.TryAddSingleton<IQueryRequester, QueryRequester>();
        services.TryAddSingleton<IEventPublisher, EventPublisher>();
    }

    private static void RegisterHandlers(IServiceCollection services, EventFlowOption options)
    {
        services.Scan(selector =>
        {
            selector.FromAssemblies(options.Assemblies)
                .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime();

            selector.FromAssemblies(options.Assemblies)
               .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<,>)))
               .AsImplementedInterfaces()
               .WithScopedLifetime();

            selector.FromAssemblies(options.Assemblies)
               .AddClasses(classes => classes.AssignableTo(typeof(IQueryHandler<,>)))
               .AsImplementedInterfaces()
               .WithScopedLifetime();

            selector.FromAssemblies(options.Assemblies)
               .AddClasses(classes => classes.AssignableTo(typeof(IEventHander<>)))
               .AsImplementedInterfaces()
               .WithScopedLifetime();
        });
    }
}