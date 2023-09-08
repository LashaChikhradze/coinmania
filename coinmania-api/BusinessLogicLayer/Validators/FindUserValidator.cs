using BusinessLogicLayer.Commands;
using FluentValidation;

namespace BusinessLogicLayer.Validators;

public class FindUserValidator: AbstractValidator<FindUserCommand>
{
    public FindUserValidator()
    {
        RuleFor(o => o.Username)
            .NotEmpty()
            .Length(9)
            .Matches(@"^\d+$");
    }
}