using System.Net.Mail;
using System.Net;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using CRMSystem.Domain.Models;
using CRMSystem.Application.Configuration;
using CRMSystem.Application.Contracts.Infrastructure;

namespace CRMSystem.Infrastructure.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;
        private readonly EmailSettings _emailSettings;
        private readonly AppSettings _appSettings;

        public EmailSender(IConfiguration configuration, UserManager<User> userManager, IOptions<EmailSettings> emailOptions, IOptions<AppSettings> appOptions)
        {
            _configuration = configuration;
            _emailSettings = emailOptions.Value;
            _appSettings = appOptions.Value;
            _userManager = userManager;
        }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            var smtpClient = new SmtpClient(_emailSettings.SmtpServer)
            {
                Port = _emailSettings.SmtpPort,
                Credentials = new NetworkCredential(_emailSettings.Username, _emailSettings.Password),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_emailSettings.FromAddress),
                Subject = subject,
                Body = message,
                IsBodyHtml = true,
            };
            mailMessage.To.Add(email);

            return smtpClient.SendMailAsync(mailMessage);
        }

        public async Task SendWelcomeEmailAsync(User user, string password)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var encodedToken = Uri.EscapeDataString(token);
            var resetLink = $"{_appSettings.BaseUrl}reset-password?token={encodedToken}&email={user.Email}";

            var subject = "Welcome to Our Service";
            var body = $@"
            Hi {user.Name},
            <br /><br />
            Welcome to our service! Here are your account details:
            <br /><br />
            Email: {user.Email}<br />
            Temporary Password: {password}<br /><br />
            Please reset your password using the following link:
            <br />
            <a href='{resetLink}'>Reset Password</a>
            <br /><br />
            Best regards,<br />
            Your Company Team
            ";

            await SendEmailAsync(user.Email!, subject, body);
        }
    }
}
