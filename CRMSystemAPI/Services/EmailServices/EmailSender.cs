using System.Net.Mail;
using System.Net;
using CRMSystemAPI.Models.DatabaseModels;
using Microsoft.AspNetCore.Identity;

namespace CRMSystemAPI.Services.EmailServices
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;

        public EmailSender(IConfiguration configuration, UserManager<User> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            var smtpClient = new SmtpClient(_configuration["Email:SmtpServer"])
            {
                Port = int.Parse(_configuration["Email:SmtpPort"]),
                Credentials = new NetworkCredential(_configuration["Email:Username"], _configuration["Email:Password"]),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_configuration["Email:FromAddress"]),
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
            var baseUrl = _configuration["App:BaseUrl"];
            var resetLink = $"{baseUrl}/auth/reset-password?token={encodedToken}&email={user.Email}";

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

            await SendEmailAsync(user.Email, subject, body);
        }
    }
}
