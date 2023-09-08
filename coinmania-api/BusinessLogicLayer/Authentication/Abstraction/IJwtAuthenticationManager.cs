namespace BusinessLogicLayer.Authentication.Abstraction;

public interface IJwtAuthenticationManager
{
    string Authenticate(int userId, string userName);
}