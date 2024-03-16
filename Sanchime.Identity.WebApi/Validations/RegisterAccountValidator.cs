using FluentValidation;

namespace Sanchime.Identity.WebApi.Validations;

public sealed class RegisterAccountValidator : AbstractValidator<RegisterAccountCommand>
{
    public RegisterAccountValidator()
    {
        RuleFor(x => x.Account)
            .NotNull()
            .NotEmpty()
            .WithMessage("账号不能为空");
        RuleFor(x => x.Password)
            .NotNull()
            .NotEmpty().WithMessage("密码不能为空");
    }
}
