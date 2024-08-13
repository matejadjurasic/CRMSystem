using CRMSystemAPI.Models.DataTransferModels.AuthTransferModels;

namespace CRMSystemAPI.Services.AuthServices
{
    public interface IAuthService
    {
        Task<AuthResponse?> AuthenticateUserAsync(LoginModel loginModel);
    }
}
