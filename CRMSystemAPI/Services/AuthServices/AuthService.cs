using CRMSystemAPI.Exceptions;
using CRMSystemAPI.Models.DatabaseModels;
using CRMSystemAPI.Models.DataTransferModels.AuthTransferModels;
using CRMSystemAPI.Services.EmailServices;
using CRMSystemAPI.Services.TokenServices;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics;
using System.Net;

namespace CRMSystemAPI.Services.AuthServices
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthService> _logger;

        public AuthService(UserManager<User> userManager, ITokenService tokenService,IConfiguration configuration, ILogger<AuthService> logger)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<AuthResponse> AuthenticateUserAsync(LoginModel loginModel)
        {
            var user = await _userManager.FindByEmailAsync(loginModel.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, loginModel.Password))
            {
                var roles = await _userManager.GetRolesAsync(user);
                var token = await _tokenService.GenerateToken(user);

                return new AuthResponse
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    Token = token,
                    Roles = roles.ToList()
                };
            }
            throw new AuthenticationException("Invalid Credentials");
        }

        public async Task ResetPasswordAsync(string email, string token, string newPassword)
        {
            var user = await _userManager.FindByEmailAsync(email) ?? throw new NotFoundException("User not found");
            _logger.LogInformation($"Received token: {token}");

            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);
            if (!result.Succeeded)
                throw new AuthenticationException(result.Errors.FirstOrDefault().ToString());
        }
    }
}
