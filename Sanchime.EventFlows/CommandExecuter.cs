using Microsoft.Extensions.DependencyInjection;

namespace Sanchime.EventFlows;

/// <summary>
/// 命令分发器
/// </summary>
internal sealed class CommandExecuter(IServiceProvider provider, IEventFlowPipelineDispatcher pipelineDispatcher) : ICommandExecuter
{
    public async Task<TExecutedResult> Execute<TCommand, TExecutedResult>(TCommand command, CancellationToken cancellation = default)
        where TCommand : ICommand<TExecutedResult>
    {
        var handler = provider.GetRequiredService<ICommandHandler<TCommand, TExecutedResult>>();

        return await pipelineDispatcher.Handle(command, handler.Handle, cancellation);
    }

    public async Task Execute<TCommand>(TCommand command, CancellationToken cancellation = default)
        where TCommand : ICommand
    {
        var handler = provider.GetRequiredService<ICommandHandler<TCommand>>();

        await pipelineDispatcher.Handle(command, async (cmd, ct) =>
        {
            await handler.Handle(cmd, ct);
            return Unit.Value;
        }, cancellation);
    }
}