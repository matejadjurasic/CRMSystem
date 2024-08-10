using CRMSystemAPI.Models;

namespace CRMSystemAPI.Auth.Tokens
{
    public interface ITokenService
    {
        Task<string> GenerateToken(User user);
    }
}
