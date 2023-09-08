using DataAccessLayer.Models;
using MediatR;

namespace BusinessLogicLayer.Commands;

public class GetUsersCommand : IRequest<IList<UserInformation>>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
}