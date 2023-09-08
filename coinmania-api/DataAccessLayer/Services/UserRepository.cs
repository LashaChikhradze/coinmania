using System.Collections;
using DataAccessLayer.DB;
using DataAccessLayer.Models;
using DataAccessLayer.Services.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SMSOfficeSharp;

namespace DataAccessLayer.Services;

public class UserRepository : IUserRepository
{
    private readonly UserManager<User> _userManager;
    private readonly ApplicationDbContext _dbContext;
    private readonly SignInManager<User> _signInManager;

    public UserRepository(UserManager<User> userManager, SignInManager<User> signInManager, ApplicationDbContext db)
    {
        _dbContext = db;
        _userManager = userManager;
        _signInManager = signInManager;
    }
    
    public async Task<bool> DeleteUserAsync(int id)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
        var result = await _userManager.DeleteAsync(user);
        return result.Succeeded;
    }

    public async Task<IdentityResult> LogInAsync(LogInUser item)
    {
        var user = await _userManager.FindByNameAsync(item.UserName);
        if (user != null && await _userManager.CheckPasswordAsync(user, item.Password))
        {
            await _signInManager.SignOutAsync();
            await _signInManager.PasswordSignInAsync(user, item.Password, false, false);
            var token = await _userManager.GenerateTwoFactorTokenAsync(user,
                _userManager.Options.Tokens.ChangePhoneNumberTokenProvider);
            var s = new Sender { ApiKey = "c6dc66605ae848fcb5863720d2e61288", MessageTitle = "message" };
            try
            {
                s.Send("Confirmation token: " + token, "995" + user.PhoneNumber);
            }
            catch (Exception e)
            {
                return IdentityResult.Failed();
            }

            return IdentityResult.Success;
        }
        else
        {
            return IdentityResult.Failed();
        }
    }
    
    public async Task<UserInformation> ConfirmPhoneSignIn(string phone, string token)
    {
        var user = await _userManager.FindByNameAsync(phone);
        if (user != null)
        {
            var result = _signInManager.TwoFactorSignInAsync(_userManager.Options.Tokens.ChangePhoneNumberTokenProvider, token, true, false);
            if (result.Result.Succeeded)
            {
                var userinfo = await _dbContext.UserInformations.FirstAsync(o => o.Phone == phone);
                return userinfo;
            }
            else
            {
                return new UserInformation();
            }
        }

        return new UserInformation();
    }

    public async Task<IdentityResult> RegistrationAsync(User item, string password)
    {
        var result = await _userManager.CreateAsync(item, password);

        if (result.Succeeded)
        {
            var user = await _userManager.FindByNameAsync(item.UserName);
            var token = await _userManager.GenerateChangePhoneNumberTokenAsync(user, user.PhoneNumber);
            var s = new Sender { ApiKey = "c6dc66605ae848fcb5863720d2e61288", MessageTitle = "message" };
            try
            {
                s.Send("Confirmation token: " + token, "995" + user.PhoneNumber);
            }
            catch (Exception e)
            {
                return IdentityResult.Failed();
            }
        }

        return IdentityResult.Success;
    }

    public async Task<IList<UserInformation>> GetUsersAsync(string phone = "", string firstName = "", string lastName = "")
    {
        IList<UserInformation> users = await _dbContext.UserInformations
            .Where(o => o.Phone.Contains(phone) && o.FirstName.Contains(firstName) && o.LastName.Contains(lastName)).ToListAsync();
        return users;
    }

    public async Task<bool> FindUserAsync(string username)
    {
        var user = await _userManager.FindByNameAsync(username);
        if (user != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public async Task<IdentityResult> ConfirmPhoneSignUp(string phone, string token)
    {
        var user = await _userManager.FindByNameAsync(phone);
        if (user != null)
        {
            var result = await _userManager.ChangePhoneNumberAsync(user, phone, token);
                if (result.Succeeded)
                {
                    user.PhoneNumberConfirmed = true;
                    var updateResult = await _userManager.SetTwoFactorEnabledAsync(user, true);
                    return updateResult;
                }
        }
        return IdentityResult.Failed();
    }
}