using AutoMapper;
using BusinessLogicLayer.Commands;
using DataAccessLayer.Models;
using DataAccessLayer.Services.Abstractions;
using MediatR;

namespace BusinessLogicLayer.CommandHandlers;

public class FindUserCommandHandler : IRequestHandler<FindUserCommand, bool>
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepo;

    public FindUserCommandHandler(IUserRepository userRepo, IMapper mapper)
    {
        _mapper = mapper;
        _userRepo = userRepo;
    }

    public async Task<bool> Handle(FindUserCommand request, CancellationToken cancellationToken)
    {
        var result = await _userRepo.FindUserAsync(request.Username);
        return _mapper.Map<bool>(result);
    }
}