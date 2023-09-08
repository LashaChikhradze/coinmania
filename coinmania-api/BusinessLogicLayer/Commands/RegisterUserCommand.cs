using MediatR;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace BusinessLogicLayer.Commands;

public class RegisterUserCommand : IRequest<IdentityResult>
{
    public string UserName { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}