using BusinessLogicLayer.Commands;
using DataAccessLayer.Services.Abstractions;
using MediatR;

namespace BusinessLogicLayer.CommandHandlers;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand,bool>
{
    private readonly IUserRepository _userRepo = default;

    public DeleteUserCommandHandler(IUserRepository userRepo)
    {
        _userRepo = userRepo;
    }

    public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        return await _userRepo.DeleteUserAsync(request.UserId);
    }
}