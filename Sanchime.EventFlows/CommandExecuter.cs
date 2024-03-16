using Microsoft.Extensions.DependencyInjection;

namespace Sanchime.EventFlows;

/// <summary>
/// 命令分发器
/// </summary>
internal sealed class CommandExecuter(IServiceProvider serviceProvider, IEventFlowPipelineDispatcher pipelineDispatcher) : ICommandExecuter
{
    public async Task<TExecutedResult> Execute<TCommand, TExecutedResult>(TCommand command, CancellationToken cancellation = default)
        where TCommand : ICommand<TExecutedResult>
    {
        await using var scope = serviceProvider.CreateAsyncScope();
        var handler = scope.ServiceProvider.GetRequiredService<ICommandHandler<TCommand, TExecutedResult>>();

        return await pipelineDispatcher.Handle(command, handler.Handle, cancellation);
    }

    public async Task Execute<TCommand>(TCommand command, CancellationToken cancellation = default)
        where TCommand : ICommand
    {
        await using var scope = serviceProvider.CreateAsyncScope();
        var handler = scope.ServiceProvider.GetRequiredService<ICommandHandler<TCommand>>();

        await pipelineDispatcher.Handle(command, async (cmd, ct) =>
        {
            await handler.Handle(cmd, ct);
            return Unit.Value;
        }, cancellation);
    }
}