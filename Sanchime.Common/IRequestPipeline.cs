namespace Sanchime.Common;

public interface IRequestPipeline<TContext, TReturn>
{
    IRequestPipeline<TContext, TReturn> Use(Func<Func<TContext, TReturn>, Func<TContext, TReturn>> middleware);

    TReturn Run();
}
