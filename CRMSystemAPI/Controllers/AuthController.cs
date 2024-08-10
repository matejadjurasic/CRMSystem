using CRMSystemAPI.Auth.AuthModels;
using CRMSystemAPI.Auth.Tokens;
using CRMSystemAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CRMSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly UserManager<User> _userManager;

        public AuthController(ITokenService tokenService, UserManager<User> userManager)
        {
            _tokenService = tokenService;
            _userManager = userManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            var user = await _userManager.FindByEmailAsync(loginModel.Email);
            Console.WriteLine(user);
            if (user != null && await _userManager.CheckPasswordAsync(user, loginModel.Password))
            {
                var token = _tokenService.GenerateToken(user);
                AuthResponse authResponse = new AuthResponse
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    Token = token.Result
                };
                return Ok(authResponse);
            }

            return Unauthorized("Invalid email or password");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("test")]
        public async Task<IActionResult> Info()
        {
            var user = await _userManager.FindByIdAsync("1");
            return Ok(user);
        }
    }
}
