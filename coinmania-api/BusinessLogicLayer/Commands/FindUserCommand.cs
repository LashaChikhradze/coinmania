using MediatR;

namespace BusinessLogicLayer.Commands;

public class FindUserCommand : IRequest<bool>
{
    public string Username { get; set; }
}