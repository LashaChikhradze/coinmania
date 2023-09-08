using BusinessLogicLayer.Commands;
using FluentValidation;

namespace BusinessLogicLayer.Validators;

public class LogInUserValidator : AbstractValidator<LogInCommand>
{
    [Obsolete]
    public LogInUserValidator()
    {
        RuleFor(o => o.UserName)
            .NotEmpty()
            .Length(9)
            .Matches(@"^\d+$");
        RuleFor(o => o.Password)
            .NotEmpty()
            .MinimumLength(6)
            .MaximumLength(15);
    }
}