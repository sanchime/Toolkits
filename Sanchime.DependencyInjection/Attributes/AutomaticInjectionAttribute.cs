namespace Sanchime.DependencyInjection;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public class InjectionAttribute : Attribute, IInjectable
{
    public virtual ServiceLifetime Lifetime { get; } = ServiceLifetime.Scoped;

    public Type? Interface { get; set; }

    public string? ServiceKey { get; set; }
}