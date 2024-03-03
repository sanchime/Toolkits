namespace Sanchime.EventFlows;

public sealed class EventFlowMediator(IQueryRequester requester, ICommandExecuter executer, IEventPublisher publisher) : IEventFlowMediator
{
    public Task<TExecutedResult> Execute<TCommand, TExecutedResult>(TCommand command, CancellationToken cancellation = default) where TCommand : ICommand<TExecutedResult>
        => executer.Execute<TCommand, TExecutedResult>(command, cancellation);

    public Task Execute<TCommand>(TCommand command, CancellationToken cancellation = default) where TCommand : ICommand
        => executer.Execute<TCommand>(command, cancellation);

    public Task Publish<TEvent>(TEvent @event, CancellationToken cancellation = default) where TEvent : IEvent
    => publisher.Publish<TEvent>(@event, cancellation);

    public Task<TQueriedResult> Request<TQuery, TQueriedResult>(TQuery query, CancellationToken cancellation = default) where TQuery : IQuery<TQueriedResult>
        => requester.Request<TQuery, TQueriedResult>(query, cancellation);
}
