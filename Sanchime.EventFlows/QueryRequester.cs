using Microsoft.Extensions.DependencyInjection;

namespace Sanchime.EventFlows;

/// <summary>
/// 查询分发器
/// </summary>
internal sealed class QueryRequester(IServiceProvider serviceProvider, IEventFlowPipelineDispatcher pipelineDispatcher) : IQueryRequester
{
    public async Task<TQueriedResult> Request<TQuery, TQueriedResult>(TQuery query, CancellationToken cancellation = default)
        where TQuery : IQuery<TQueriedResult>
    {
        await using var scope = serviceProvider.CreateAsyncScope();

        var handler = scope.ServiceProvider.GetRequiredService<IQueryHandler<TQuery, TQueriedResult>>();
        return await pipelineDispatcher.Handle(query, handler.Handle, cancellation);
    }
}