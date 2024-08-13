using CRMSystemAPI.Models.DatabaseModels;

namespace CRMSystemAPI.Services.TokenServices
{
    public interface ITokenService
    {
        Task<string> GenerateToken(User user);
    }
}
