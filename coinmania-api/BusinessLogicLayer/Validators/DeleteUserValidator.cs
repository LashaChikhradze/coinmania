using BusinessLogicLayer.Commands;
using FluentValidation;

namespace BusinessLogicLayer.Validators;

public class DeleteUserValidator : AbstractValidator<DeleteUserCommand>
{
    public DeleteUserValidator()
    {
        RuleFor(o => o.UserId)
            .NotEmpty()
            .NotEqual(0);
    }
}