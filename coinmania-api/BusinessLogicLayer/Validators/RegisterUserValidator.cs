using BusinessLogicLayer.Commands;
using FluentValidation;

namespace BusinessLogicLayer.Validators;

public class RegisterUserValidator : AbstractValidator<RegisterUserCommand>
{
    [Obsolete]
    public RegisterUserValidator()
    {
        RuleFor(o => o.FirstName)
            .NotEmpty();
        RuleFor(o => o.LastName)
            .NotEmpty();
        RuleFor(o => o.UserName)
            .NotEmpty()
            .Length(9)
            .Matches(@"^\d+$");

        RuleFor(o => o.Password)
            .NotEmpty()
            .MinimumLength(6)
            .MaximumLength(15)
            .Matches(@"^([a-zA-Z0-9]+)$");

    }
}