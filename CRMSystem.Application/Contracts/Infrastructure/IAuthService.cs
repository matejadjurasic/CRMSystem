using CRMSystem.Application.Models.Auth;

namespace CRMSystem.Application.Contracts.Infrastructure
{
    public interface IAuthService
    {
        Task<AuthResponse> AuthenticateUserAsync(LoginModel loginModel);
        Task ResetPasswordAsync(string email, string token, string newPassword);
    }
}
