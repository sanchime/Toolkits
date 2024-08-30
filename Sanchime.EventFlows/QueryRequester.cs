using Microsoft.Extensions.DependencyInjection;

namespace Sanchime.EventFlows;

/// <summary>
/// 查询分发器
/// </summary>
internal sealed class QueryRequester(IServiceProvider provider, IEventFlowPipelineDispatcher pipelineDispatcher) : IQueryRequester
{
    public Task<TQueriedResult> Request<TQuery, TQueriedResult>(TQuery query, CancellationToken cancellation = default)
        where TQuery : IQuery<TQueriedResult>
    {
        var handler = provider.GetRequiredService<IQueryHandler<TQuery, TQueriedResult>>();
        return pipelineDispatcher.Handle(query, handler.Handle, cancellation);
    }
}