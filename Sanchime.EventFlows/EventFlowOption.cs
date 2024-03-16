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
}