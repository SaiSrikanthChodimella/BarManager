using BarManagerAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BarManagerAPI.AuthenticationService
{
    public class AuthService(IConfiguration configuration) : IAuthService
    {
        public string GeneratePasswordHash(User user, string password) => new PasswordHasher<User>().HashPassword(user, user.PasswordHash);

        public string GenerateToken(User user)
        {
            var claims = new List<Claim>()
            {
                new(ClaimTypes.Name,user.Name)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("AppSettings:Token")!));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new JwtSecurityToken(
                issuer: configuration.GetValue<string>("AppSettings:Issuer"),
                audience: configuration.GetValue<string>("AppSettings:Audience"),
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }

        public bool VerifyUser(User user, string password)
        {
            var hashedPassword = new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, password);
            return hashedPassword == PasswordVerificationResult.Success;
        }
    }
}
