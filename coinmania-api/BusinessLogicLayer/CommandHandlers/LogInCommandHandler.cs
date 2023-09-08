using AutoMapper;
using BusinessLogicLayer.Authentication.Abstraction;
using BusinessLogicLayer.Commands;
using DataAccessLayer.Models;
using DataAccessLayer.Services.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BusinessLogicLayer.CommandHandlers;

public class LogInCommandHandler : IRequestHandler<LogInCommand, IdentityResult>
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepo;

    public LogInCommandHandler(IUserRepository userRepo, IMapper mapper)
    {
        _mapper = mapper;
        _userRepo = userRepo;
    }

    public async Task<IdentityResult> Handle(LogInCommand request, CancellationToken cancellationToken)
    {
        var logInInfo = _mapper.Map<LogInUser>(request);
        return await _userRepo.LogInAsync(logInInfo);
    }
}