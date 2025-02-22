using BarManagerAPI.Models;

namespace BarManagerAPI.AuthenticationService
{
    public interface IAuthService
    {
        string GeneratePasswordHash(User user, string password);

        bool VerifyUser(User user, string password);

        string GenerateToken(User user);
    }
}
