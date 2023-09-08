using DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;

namespace DataAccessLayer.Services.Abstractions;

public interface IUserRepository
{
    Task<bool> DeleteUserAsync(int id);
    Task<IdentityResult> RegistrationAsync(User item,string password);
    Task<IdentityResult> LogInAsync(LogInUser item);
    Task<IList<UserInformation>> GetUsersAsync(string phone, string firstName, string lastName);
    Task<bool> FindUserAsync(string username); 
    Task<UserInformation> ConfirmPhoneSignIn(string phone, string token);
    Task<IdentityResult> ConfirmPhoneSignUp(string phone, string token);

}