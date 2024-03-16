using FluentValidation;

namespace Sanchime.Identity.WebApi;

public class EventFlowValidationPipeline<TRequest, TResult>(IServiceProvider provider) : IEventFlowPipeline<TRequest, TResult>
{
    public Task<TResult> Handle(TRequest request, Func<TRequest, CancellationToken, Task<TResult>> next, CancellationToken cancellation = default)
    {
        Validate(request);

        return next(request, cancellation);
    }

    private void Validate(TRequest request)
    {
        var validator = provider.GetService<IValidator<TRequest>>();
        if (validator is not null)
        {
            var result = validator.Validate(request);

            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors.ToJoinString(error => error.ErrorMessage, ','));
            }
        }
    }
}
