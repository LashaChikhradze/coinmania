using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BusinessLogicLayer.Authentication.Abstraction;
using Microsoft.IdentityModel.Tokens;

namespace BusinessLogicLayer.Authentication;

public class JwtAuthenticationManager : IJwtAuthenticationManager
{
    private readonly string _key;
    public JwtAuthenticationManager(string key)
    {
        _key = key;
    }


    public string Authenticate(int userId, string userName)
    {
        if (userId > 0)
        {
            var tokenKey = Encoding.ASCII.GetBytes(_key);
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new List<Claim>()
                {
                    new Claim(ClaimTypes.Name,userName),
                    new Claim(ClaimTypes.NameIdentifier,userId.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(12),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        return string.Empty;
    }

}