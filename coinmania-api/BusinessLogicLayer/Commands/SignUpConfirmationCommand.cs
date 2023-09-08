using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BusinessLogicLayer.Commands;

public class SignUpConfirmationCommand : IRequest<IdentityResult>
{
    public string PhoneNumber { get; set; }
    public string Token { get; set; }
}