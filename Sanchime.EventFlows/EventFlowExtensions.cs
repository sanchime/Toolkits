using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Sanchime.EventFlows;

public static class EventFlowExtensions
{
    public static IServiceCollection AddEventFlow(this IServiceCollection services, Action<EventFlowOption>? configer = null)
    {
        var options = new EventFlowOption(services);

        configer?.Invoke(options);

        RegisterDispatchers(services, options);

        services.Add(new ServiceDescriptor(typeof(IEventFlowMediator), typeof(EventFlowMediator), options.Lifetime));
        services.Add(new ServiceDescriptor(typeof(IEventFlowPipelineDispatcher), typeof(EventFlowPipelineDispatcher), options.Lifetime));
        RegisterHandlers(services, options);

        return services;
    }

    private static void RegisterDispatchers(IServiceCollection services, EventFlowOption options)
    {
        services.Add(new ServiceDescriptor(typeof(ICommandExecuter), typeof(CommandExecuter), options.Lifetime));
        services.Add(new ServiceDescriptor(typeof(IQueryRequester), typeof(QueryRequester), options.Lifetime));
        services.Add(new ServiceDescriptor(typeof(IEventPublisher), typeof(EventPublisher), options.Lifetime));
    }

    private static void RegisterHandlers(IServiceCollection services, EventFlowOption options)
    {
        services.Scan(selector =>
        {
            selector.FromAssemblies(options.Assemblies)
                .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<>)))
                .AsImplementedInterfaces()
                .WithLifetime(options.Lifetime);

            selector.FromAssemblies(options.Assemblies)
               .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<,>)))
               .AsImplementedInterfaces()
               .WithLifetime(options.Lifetime);

            selector.FromAssemblies(options.Assemblies)
               .AddClasses(classes => classes.AssignableTo(typeof(IQueryHandler<,>)))
               .AsImplementedInterfaces()
               .WithLifetime(options.Lifetime);

            selector.FromAssemblies(options.Assemblies)
               .AddClasses(classes => classes.AssignableTo(typeof(IEventHander<>)))
               .AsImplementedInterfaces()
               .WithLifetime(options.Lifetime);
        });
    }
}