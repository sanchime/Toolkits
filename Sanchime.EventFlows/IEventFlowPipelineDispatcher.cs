namespace Sanchime.EventFlows;


public interface IEventFlowPipelineDispatcher
{
    Task<TResult> Handle<TRequest, TResult>(TRequest request, Func<TRequest, CancellationToken, Task<TResult>> handle, CancellationToken cancellation = default)
        where TRequest : notnull;
}