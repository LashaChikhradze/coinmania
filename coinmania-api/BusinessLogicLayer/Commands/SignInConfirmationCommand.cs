using DataAccessLayer.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BusinessLogicLayer.Commands;

public class SignInConfirmationCommand : IRequest<Tuple<UserInformation, string>>
{
    public string PhoneNumber { get; set; }
    public string Token { get; set; }
}