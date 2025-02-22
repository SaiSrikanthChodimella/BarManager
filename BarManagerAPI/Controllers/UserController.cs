using BarManagerAPI.AuthenticationService;
using BarManagerAPI.DTO;
using BarManagerAPI.Models;
using BarManagerAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BarManagerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController(IUnitOfWork unitOfWork, IAuthService authenticationService) : ControllerBase
    {

        [HttpPost("register")]
        public async Task<ActionResult> RegisterUser(User user)
        {
            var userExists = await unitOfWork.UserRepository.Find(x => x.Name == user.Name);

            if (userExists.Any())
            {
                return BadRequest("User Already Exists");
            }

            var userDTO = user.mapToUserDTO();

            user.PasswordHash = authenticationService.GeneratePasswordHash(user, user.PasswordHash);

            await unitOfWork.UserRepository.AddAsync(user);
            await unitOfWork.SaveAsync();

            return Ok(userDTO);
        }

        [HttpPost("login")]
        public async Task<ActionResult> LoginUser(User user)
        {
            var foundUser = await unitOfWork.UserRepository.Find(x => x.Name == user.Name);

            if (foundUser.FirstOrDefault() is User member)
            {
                var isVerified = authenticationService.VerifyUser(member, user.PasswordHash);
                if (isVerified)
                {
                    var token = authenticationService.GenerateToken(member);
                    return Ok(token);
                }
            }
            return BadRequest("User not Found or Password is wrong");
        }
    }
}
