using Microsoft.Extensions.DependencyInjection;
using System.Collections.Concurrent;

namespace Sanchime.EventFlows;

internal sealed class EventFlowPipelineDispatcher(IServiceProvider provider) : IEventFlowPipelineDispatcher
{
    private readonly ConcurrentDictionary<Type, Delegate> _pipelines = new();

    public async Task<TResult> Handle<TRequest, TResult>(TRequest request,
        Func<TRequest, CancellationToken, Task<TResult>> handle,
        CancellationToken cancellation = default)
        where TRequest : notnull
    {

        var chain = (Func<TRequest, CancellationToken, Task<TResult>>)_pipelines.GetOrAdd(typeof(TRequest), type =>
        {
            return GetExecuteChain();
        });

        return await chain(request, cancellation);

        Func<TRequest, CancellationToken, Task<TResult>> GetExecuteChain()
        {
            var pipelines = provider.GetServices<IEventFlowPipeline<TRequest, TResult>>();

            var chain = handle;

            foreach (var pipeline in pipelines.Reverse())
            {
                var next = chain;
                chain = (cmd, ct) => pipeline.Handle(cmd, next, ct);
            }

            return chain;
        }
    }
}
