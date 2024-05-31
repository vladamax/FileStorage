using Microsoft.AspNetCore.Mvc;
using FileStorage.Auth;
using FileStorage.Auth.DTO;
using FileStorage.Services.Auth;

namespace FileStorage.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(IUserAccountService userAccountService) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserDTO userDTO)
        {
            var response = await userAccountService.CreateAccount(userDTO);
            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            var response = await userAccountService.LoginAccount(loginDTO);
            return Ok(response);
        }
    }
}
