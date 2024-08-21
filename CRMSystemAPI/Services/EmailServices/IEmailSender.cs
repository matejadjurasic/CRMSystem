using CRMSystemAPI.Models.DatabaseModels;

namespace CRMSystemAPI.Services.EmailServices
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
        Task SendWelcomeEmailAsync(User user, string password);
    }
}
