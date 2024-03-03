using System.Diagnostics;
using System.Reflection;
using System.Runtime.Loader;

namespace Sanchime.DependencyInjection;

public static class AutomaticInjectionExtensions
{
    private static bool _needLazy;

    private readonly static MethodInfo _lazyRegister = typeof(AutomaticInjectionExtensions).GetMethod(nameof(WithLazy), BindingFlags.Static | BindingFlags.Public)!;

    /// <summary>
    /// 启用自动注入
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"></param>
    /// <param name="needLazy">是否需要延迟注入，如果为True，则会同时注册<see cref="Lazy{T}"/>类型</param>
    /// <param name="assemblies"></param>
    /// <returns><see cref="IServiceCollection"></returns>
    public static IServiceCollection EnableAutomationInjection(
        this IServiceCollection services,
        bool needLazy = false,
        params Assembly[]? assemblies)
    {
        _needLazy = needLazy;

        if (assemblies is not { Length: > 0 })
        {
            assemblies = AssemblyLoadContext.All.SelectMany(x => x.Assemblies).ToArray();
        }

        var injectings = from assembly in assemblies
                         from type in assembly.GetTypes()
                         where CanInject(type)
                         select type;

        foreach (var injecting in injectings)
        {
            Register(services, injecting);
            Console.WriteLine($"Registered service type: {injecting.FullName}");
        }

        return services;

        static bool CanInject(Type type)
        {
            return !type.IsAbstract
                //&& !type.IsInterface
                && !type.IsGenericType
                && !type.IsNested
                && type.GetCustomAttribute<InjectionAttribute>() is not null;
        }
    }


    private static void Register(IServiceCollection services, Type type)
    {
        var inject = type.GetCustomAttribute<InjectionAttribute>()!;
        var @interface = inject.Interface;
        var serviceType = type;

        if (@interface is not null)
        {
            serviceType = @interface;
            services.Add(new ServiceDescriptor(serviceType, inject.ServiceKey, type, inject.Lifetime));
        }
        else
        {
            services.Add(new ServiceDescriptor(serviceType, inject.ServiceKey, serviceType, inject.Lifetime));
        }

        if (_needLazy)
        {
            var currentLazyRegister = _lazyRegister.MakeGenericMethod(serviceType);

            currentLazyRegister.Invoke(null, new object[] { services, inject.Lifetime });
        }
    }


    public static void WithLazy<TServiceType>(this IServiceCollection services, ServiceLifetime lifetime = ServiceLifetime.Scoped)
        where TServiceType : notnull
    {
        var currentLazyType = typeof(Lazy<TServiceType>);
        services.Add(new ServiceDescriptor(
            currentLazyType,
            provider => new Lazy<TServiceType>(() =>
            {
                using var autowirer = new AutowiredServiceProvider(provider);
                return autowirer.GetRequiredService<TServiceType>();
            }),
            lifetime));
    }
}
