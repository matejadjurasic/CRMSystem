using CRMSystem.Domain.Models;

namespace CRMSystem.Application.Contracts.Infrastructure
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
        Task SendWelcomeEmailAsync(User user, string password);
    }
}
