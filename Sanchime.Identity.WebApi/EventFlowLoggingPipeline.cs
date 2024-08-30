namespace Sanchime.Identity.WebApi;

public class EventFlowLoggingPipeline<TRequest, TResult>(ILogger<EventFlowLoggingPipeline<TRequest, TResult>> logger) : IEventFlowPipeline<TRequest, TResult>
{
    public async Task<TResult> Handle(TRequest request, Func<TRequest, CancellationToken, Task<TResult>> next, CancellationToken cancellation = default)
    {
        logger.LogTrace("请求执行: {@Request}", request);

        var result = await next(request, cancellation);

        logger.LogTrace("请求执行完成: {@ResultType}", result?.GetType());

        return result;
    }
}
