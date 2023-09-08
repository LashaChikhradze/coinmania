using Microsoft.AspNetCore.Identity;

namespace DataAccessLayer.Models;

public class User:IdentityUser<int>
{
    public UserInformation GeneralInformation { get; set; }
}