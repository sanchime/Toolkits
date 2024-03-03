namespace Sanchime.DependencyInjection;


public class ScopedInjectionAttribute : InjectionAttribute
{
    public override ServiceLifetime Lifetime => ServiceLifetime.Scoped;
}
