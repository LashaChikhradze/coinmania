using AutoMapper;
using BusinessLogicLayer.Authentication.Abstraction;
using BusinessLogicLayer.Commands;
using DataAccessLayer.Models;
using DataAccessLayer.Services.Abstractions;
using MediatR;

namespace BusinessLogicLayer.CommandHandlers;

public class SignInConfirmationCommandHandler : IRequestHandler<SignInConfirmationCommand, Tuple<UserInformation, string>>
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepo;
    private readonly IJwtAuthenticationManager _authenticationManager;

    public SignInConfirmationCommandHandler(IUserRepository userRepo, IMapper mapper, IJwtAuthenticationManager authenticationManager)
    {
        _mapper = mapper;
        _userRepo = userRepo;
        _authenticationManager = authenticationManager;
    }

    public async Task<Tuple<UserInformation, string>> Handle(SignInConfirmationCommand request, CancellationToken cancellationToken)
    {
        var result =  await _userRepo.ConfirmPhoneSignIn(request.PhoneNumber, request.Token);
        if (result.Id > 0)
        {
            var tk = _authenticationManager.Authenticate(result.Id, request.PhoneNumber);
            return Tuple.Create(result, tk);
        }

        return Tuple.Create(new UserInformation(), string.Empty);
    }
}