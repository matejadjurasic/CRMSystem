using CRMSystem.Domain.Models;

namespace CRMSystem.Application.Contracts.Infrastructure
{
    public interface ITokenService
    {
        Task<string> GenerateToken(User user);
    }
}
