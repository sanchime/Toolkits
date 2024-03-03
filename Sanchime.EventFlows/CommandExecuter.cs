using Microsoft.Extensions.DependencyInjection;

namespace Sanchime.EventFlows;

/// <summary>
/// 命令分发器
/// </summary>
internal sealed class CommandExecuter(IServiceProvider serviceProvider) : ICommandExecuter
{
    public async Task<TExecutedResult> Execute<TCommand, TExecutedResult>(TCommand command, CancellationToken cancellation = default)
        where TCommand : ICommand<TExecutedResult>
    {
        await using var scope = serviceProvider.CreateAsyncScope();
        var handler = scope.ServiceProvider.GetRequiredService<ICommandHandler<TCommand, TExecutedResult>>();

        return await handler.Handle(command, cancellation);
    }

    public async Task Execute<TCommand>(TCommand command, CancellationToken cancellation = default)
        where TCommand : ICommand
    {
        await using var scope = serviceProvider.CreateAsyncScope();
        var handler = scope.ServiceProvider.GetRequiredService<ICommandHandler<TCommand>>();

        await handler.Handle(command, cancellation);
    }
}