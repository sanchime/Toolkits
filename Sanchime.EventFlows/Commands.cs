namespace Sanchime.EventFlows;

public interface ICommand;

public interface ICommand<out TExecutedResult> : ICommand;

public interface ICommandHandler<in TCommand, TExecutedResult>
    where TCommand : ICommand<TExecutedResult>
{
    Task<TExecutedResult> Handle(TCommand command, CancellationToken cancellation = default);
}

public interface ICommandHandler<in TCommand>
    where TCommand : ICommand
{
    Task Handle(TCommand command, CancellationToken cancellation = default);
}

public interface ICommandExecuter
{
    Task<TExecutedResult> Execute<TCommand, TExecutedResult>(TCommand command, CancellationToken cancellation = default)
        where TCommand : ICommand<TExecutedResult>;

    Task Execute<TCommand>(TCommand command, CancellationToken cancellation = default)
        where TCommand : ICommand;
}