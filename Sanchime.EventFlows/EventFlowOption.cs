using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Sanchime.EventFlows;

public sealed record class EventFlowOption
{
    internal Assembly[] Assemblies { get; private set; } = [ Assembly.GetEntryAssembly() ?? Assembly.GetCallingAssembly(), typeof(IEventFlowMediator).Assembly ];

    public IServiceCollection Services { get; }

    public EventFlowOption(IServiceCollection services)
    {
        Services = services;
    }

    public void RegisterAssemblies(params Assembly[] assemblies)
    {
        Assemblies = [ ..assemblies, Assembly.GetCallingAssembly(), typeof(IEventFlowMediator).Assembly];
    }

    public void RegisterCommandDispatcher<TCommandDispatcher>()
        where TCommandDispatcher : ICommandExecuter
    {
        Services.Decorate<ICommandExecuter, TCommandDispatcher>();
    }

    public void RegisterQueryDispatcher<TQueryDispatcher>()
        where TQueryDispatcher : IQueryRequester
    {
        Services.Decorate<IQueryRequester, TQueryDispatcher>();
    }

    public void RegisterEventDispatcher<TEventDispatcher>()
        where TEventDispatcher : IEventPublisher
    {
        Services.Decorate<IEventPublisher, TEventDispatcher>();
    }
}