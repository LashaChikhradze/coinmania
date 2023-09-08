using BusinessLogicLayer.Commands;
using DataAccessLayer.Services.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BusinessLogicLayer.CommandHandlers;

public class SignUpConfirmationCommandHandler : IRequestHandler<SignUpConfirmationCommand, IdentityResult>
{
    private readonly IUserRepository _userRepo = default;

    public SignUpConfirmationCommandHandler(IUserRepository userRepo)
    {
        _userRepo = userRepo;
    }

    public async Task<IdentityResult> Handle(SignUpConfirmationCommand request, CancellationToken cancellationToken)
    {
        return await _userRepo.ConfirmPhoneSignUp(request.PhoneNumber, request.Token);
    }
}