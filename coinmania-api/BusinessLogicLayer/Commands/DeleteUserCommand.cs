using MediatR;

namespace BusinessLogicLayer.Commands;

public class DeleteUserCommand : IRequest<bool>
{
    public int UserId { get; set; }
}