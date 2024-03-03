namespace Sanchime.DependencyInjection;

public class SingletonInjectionAttribute : InjectionAttribute
{
    public override ServiceLifetime Lifetime => ServiceLifetime.Singleton;
}
