using AutoMapper;
using BusinessLogicLayer.Commands;
using DataAccessLayer.Models;
using DataAccessLayer.Services.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BusinessLogicLayer.CommandHandlers;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, IdentityResult>
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepo;

    public RegisterUserCommandHandler(IUserRepository userRepo, IMapper mapper)
    {
        _mapper = mapper;
        _userRepo = userRepo;
    }

    public async Task<IdentityResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<User>(request);
        return await _userRepo.RegistrationAsync(user, request.Password);
    }
}