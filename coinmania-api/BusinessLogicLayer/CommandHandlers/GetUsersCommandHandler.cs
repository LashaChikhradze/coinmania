using AutoMapper;
using BusinessLogicLayer.Commands;
using DataAccessLayer.Models;
using DataAccessLayer.Services.Abstractions;
using MediatR;

namespace BusinessLogicLayer.CommandHandlers;

public class GetUsersCommandHandler : IRequestHandler<GetUsersCommand, IList<UserInformation>>
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepo;

    public GetUsersCommandHandler(IUserRepository userRepo, IMapper mapper)
    {
        _mapper = mapper;
        _userRepo = userRepo;
    }

    public async Task<IList<UserInformation>> Handle(GetUsersCommand request, CancellationToken cancellationToken)
    {
        var users = await _userRepo.GetUsersAsync(request.PhoneNumber, request.FirstName, request.LastName);
        return _mapper.Map<IList<UserInformation>>(users);
    }
}