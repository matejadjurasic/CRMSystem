using CRMSystem.Application.Contracts.Infrastructure;
using CRMSystem.Application.Models.Auth;
using Microsoft.AspNetCore.Mvc;

namespace CRMSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            var authResponse = await _authService.AuthenticateUserAsync(loginModel);
            return Ok(authResponse);
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromQuery] string email, [FromQuery] string token, [FromBody] ResetPasswordModel resetPasswordModel)
        {
            await _authService.ResetPasswordAsync(email,token,resetPasswordModel.NewPassword);
            return Ok("Password has been reset successfully.");
        }
    }
}
