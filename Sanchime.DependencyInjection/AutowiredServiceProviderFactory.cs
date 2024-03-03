namespace Sanchime.DependencyInjection;

/// <summary>
/// 属性注入提供器工厂
/// </summary>
public sealed class AutowiredServiceProviderFactory : IServiceProviderFactory<IServiceCollection>
{
    public IServiceCollection CreateBuilder(IServiceCollection services) => services ?? new ServiceCollection();

    public IServiceProvider CreateServiceProvider(IServiceCollection containerBuilder)
    {
        var serviceProvider = containerBuilder.BuildServiceProvider();
        return new AutowiredServiceProvider(serviceProvider);
    }
}
