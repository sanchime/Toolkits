
namespace Sanchime.Common;

public class RequestPipeline<TContext, TReturn>(TContext initial = default!) : IRequestPipeline<TContext, TReturn>
{
    private readonly Stack<Func<Func<TContext, TReturn>, Func<TContext, TReturn>>> _components = new();

    private Func<TContext, TReturn>? _seeder;

    public TReturn Run()
    {
        var seeder = _seeder ??= _ => default!;

        return _components.Aggregate(seeder, (current, next) => next(current))(initial);

        /*foreach (var component in _components)
        {
            seeder = component(seeder);
        }

        return seeder(initial);*/
    }

    public IRequestPipeline<TContext, TReturn> Use(Func<Func<TContext, TReturn>, Func<TContext, TReturn>> middleware)
    {
        _components.Push(middleware);
        return this;
    }
}
