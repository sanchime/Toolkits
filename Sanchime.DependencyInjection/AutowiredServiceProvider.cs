using System.Linq.Expressions;
using System.Reflection;
using System.Collections.Concurrent;

namespace Sanchime.DependencyInjection;

internal sealed class AutowiredServiceProvider(IServiceProvider provider) : IServiceProvider, ISupportRequiredService, IDisposable
{
    private static readonly ConcurrentDictionary<Type, Action<object, IServiceProvider>> _autowiredActions = [];
    private bool _disposed;

    public object GetRequiredService(Type serviceType)
    {
        var instance = provider.GetRequiredService(serviceType);
        InternalAutowried(instance);
        return instance;
    }

    public object? GetService(Type serviceType)
    {
        var instance = provider.GetService(serviceType);
        InternalAutowried(instance);
        return instance;
    }

    internal void InternalAutowried(object? instance)
    {
        if (provider is null || instance is null)
        {
            return;
        }
        var type = instance as Type ?? instance.GetType();

        var flags = BindingFlags.Public | BindingFlags.NonPublic;

        if (instance is Type)
        {
            instance = default!;
            flags |= BindingFlags.Static;
        }
        else
        {
            flags |= BindingFlags.Instance;
        }

        UseExpression(instance, type, flags);
    }

    private void UseExpression(object instance, Type type, BindingFlags flags)
    {
        if (_autowiredActions.TryGetValue(type, out var action))
        {
            action(instance, provider);
        }
        else
        {
            //参数
            var objParam = Expression.Parameter(typeof(object), nameof(instance));
            var spParam = Expression.Parameter(typeof(IServiceProvider), nameof(provider));

            var obj = Expression.Convert(objParam, type);
            var getService = typeof(IServiceProvider).GetMethod(nameof(IServiceProvider.GetService))!;
            var setList = new List<Expression>();

            //字段赋值
            foreach (var field in type.GetFields(flags))
            {
                var autowiredAttr = field.GetCustomAttribute<AutowiredAttribute>();
                if (autowiredAttr is null)
                {
                    continue;
                }
                var fieldExp = Expression.Field(obj, field);
                var createService = Expression.Call(spParam, getService, Expression.Constant(field.FieldType));
                var setExp = Expression.Assign(fieldExp, Expression.Convert(createService, field.FieldType));
                setList.Add(setExp);
            }
            //属性赋值
            foreach (var property in type.GetProperties(flags))
            {
                var autowiredAttr = property.GetCustomAttribute<AutowiredAttribute>();
                if (autowiredAttr is null)
                {
                    continue;
                }
                var propExp = Expression.Property(obj, property);
                var createService = Expression.Call(spParam, getService, Expression.Constant(property.PropertyType));
                var setExp = Expression.Assign(propExp, Expression.Convert(createService, property.PropertyType));
                setList.Add(setExp);
            }
            var bodyExp = Expression.Block(setList);
            action = Expression.Lambda<Action<object, IServiceProvider>>(bodyExp, objParam, spParam).Compile();
            _autowiredActions.TryAdd(type, action);
            action(instance, provider);
        }
    }

    private void UseReflection(object instance, Type type, BindingFlags flags)
    {
        // 字段
        foreach (var field in type.GetFields(flags))
        {
            var autowriedAttr = field.GetCustomAttribute<AutowiredAttribute>();
            if (autowriedAttr is null)
            {
                continue;
            }
            var dependency = GetService(field.FieldType);
            if (dependency is not null)
                field.SetValue(instance, dependency);
        }

        // 属性
        foreach (var property in type.GetProperties(flags))
        {
            var autowriedAttr = property.GetCustomAttribute<AutowiredAttribute>();
            if (autowriedAttr is null)
            {
                continue;
            }
            var dependency = GetService(property.PropertyType);
            if (dependency is not null)
                property.SetValue(instance, dependency);
        }
    }

    private void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                
                // TODO: 释放托管状态(托管对象)
            }

            // TODO: 释放未托管的资源(未托管的对象)并重写终结器
            // TODO: 将大型字段设置为 null
            _disposed = true;
        }
    }

    // // TODO: 仅当“Dispose(bool disposing)”拥有用于释放未托管资源的代码时才替代终结器
    // ~AutowiredServiceProvider()
    // {
    //     // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
    //     Dispose(disposing: false);
    // }

    public void Dispose()
    {
        // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
