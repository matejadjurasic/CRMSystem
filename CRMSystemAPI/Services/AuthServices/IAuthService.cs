﻿using CRMSystemAPI.Models.DatabaseModels;
using CRMSystemAPI.Models.DataTransferModels.AuthTransferModels;
using Microsoft.AspNetCore.Identity;

namespace CRMSystemAPI.Services.AuthServices
{
    public interface IAuthService
    {
        Task<AuthResponse> AuthenticateUserAsync(LoginModel loginModel);
        Task SendWelcomeEmailAsync(User user, string password);
        Task ResetPasswordAsync(string email, string token, string newPassword);
    }
}
