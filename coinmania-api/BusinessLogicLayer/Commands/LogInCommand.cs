using DataAccessLayer.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BusinessLogicLayer.Commands;

public class LogInCommand : IRequest<IdentityResult>
{
    public string UserName { get; set; }
    public string Password { get; set; }
}