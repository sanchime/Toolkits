using FluentValidation;

namespace Sanchime.Identity.WebApi;

public class CommandValidationDispatcher(IServiceProvider provider, ICommandExecuter executer) : ICommandExecuter
{
    public Task<TExecutedResult> Execute<TCommand, TExecutedResult>(TCommand command, CancellationToken cancellation = default) where TCommand : ICommand<TExecutedResult>
    {
        Validate(command);

        return executer.Execute<TCommand, TExecutedResult>(command, cancellation);
    }

    public Task Execute<TCommand>(TCommand command, CancellationToken cancellation = default) where TCommand : ICommand
    {
        Validate(command);
        return executer.Execute<TCommand>(command, cancellation);
    }

    private void Validate<TCommand>(TCommand command)
    {
        var validator = provider.GetService<IValidator<TCommand>>();
        if (validator is not null)
        {
            var result = validator.Validate(command);

            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors.ToJoinString(error => error.ErrorMessage, ','));
            }
        }
    }
}
