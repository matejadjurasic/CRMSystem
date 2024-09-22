using CRMSystem.Application.Contracts.Infrastructure;
using CRMSystem.Application.Exceptions;
using CRMSystem.Application.Models.Auth;
using CRMSystem.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Net;

namespace CRMSystem.Infrastructure.Services
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
            _logger.LogInformation("Authenticating user with email: {Email}", loginModel.Email);
            var user = await _userManager.FindByEmailAsync(loginModel.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, loginModel.Password))
            {
                _logger.LogInformation("User {UserId} authenticated successfully", user.Id);
                var roles = await _userManager.GetRolesAsync(user);
                var token = await _tokenService.GenerateToken(user);

                return new AuthResponse
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email!,
                    Token = token,
                    Roles = roles.ToList()
                };
            }
            _logger.LogWarning("Failed authentication attempt for email: {Email}", loginModel.Email);
            throw new AuthenticationException("Invalid Credentials");
        }

        public async Task ResetPasswordAsync(string email, string token, string newPassword)
        {
            _logger.LogInformation("Attempting password reset for email: {Email}", email);
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                _logger.LogWarning("Password reset failed. User with email {Email} not found", email);
                throw new NotFoundException(nameof(User), email);
            }

            _logger.LogDebug("User {UserId} found, resetting password", user.Id);
            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);

            if (!result.Succeeded)
            {
                var errorDescription = result.Errors.FirstOrDefault()?.Description ?? "Unknown error";
                _logger.LogError("Password reset failed for user {UserId}. Error: {Error}", user.Id, errorDescription);
                throw new AuthenticationException(errorDescription);
            }
            _logger.LogInformation("Password reset successfully for user {UserId}", user.Id);
        }
    }
}
