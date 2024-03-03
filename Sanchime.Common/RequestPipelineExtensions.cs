namespace Sanchime.Common;

public static class RequestPipelineExtensions
{
    public static IRequestPipeline<TContext, TReturn> Use<TContext, TReturn>(this IRequestPipeline<TContext, TReturn> pipeline, Func<TContext, Func<TContext, TReturn>, TReturn> middleware)
    {
        return pipeline.Use(next => context => middleware(context, next));
    }
}
