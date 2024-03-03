namespace Sanchime.DependencyInjection;

public class TransientInjectionAttribute : InjectionAttribute
{
    public override ServiceLifetime Lifetime => ServiceLifetime.Transient;
}
