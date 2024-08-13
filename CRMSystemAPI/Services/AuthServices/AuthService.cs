using CRMSystemAPI.Models.DatabaseModels;
using CRMSystemAPI.Models.DataTransferModels.AuthTransferModels;
using CRMSystemAPI.Services.TokenServices;
using Microsoft.AspNetCore.Identity;

namespace CRMSystemAPI.Services.AuthServices
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;

        public AuthService(UserManager<User> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        public async Task<AuthResponse?> AuthenticateUserAsync(LoginModel loginModel)
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
            return null;
        }
    }
}
