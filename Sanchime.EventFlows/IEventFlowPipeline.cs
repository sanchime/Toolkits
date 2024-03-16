namespace Sanchime.EventFlows;

public interface IEventFlowPipeline<TRequest, TResult>
{
    Task<TResult> Handle(TRequest request, Func<TRequest, CancellationToken, Task<TResult>> next, CancellationToken cancellation = default);
}

public interface IEventFlowPipeline<TRequest> : IEventFlowPipeline<TRequest, Unit>
{

}

