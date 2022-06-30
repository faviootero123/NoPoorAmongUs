using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using MimeKit;

namespace SaucyCapstone.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailConfiguration _emailConfig;
        public EmailSender(IOptions<EmailConfiguration> emailConfig)
        {
            _emailConfig = emailConfig.Value;
        }
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var emailToSend = new MimeMessage();
            emailToSend.From.Add(new MailboxAddress("saucycoders", _emailConfig.From));
            emailToSend.To.Add(MailboxAddress.Parse(email));
            emailToSend.Subject = subject;
            emailToSend.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = htmlMessage };

            //send email with G-mail
            using var emailClient = new SmtpClient();
            await emailClient.ConnectAsync(_emailConfig.SmtpServer, _emailConfig.Port, false);
            await emailClient.AuthenticateAsync(_emailConfig.UserName, _emailConfig.Password);
            _ = await emailClient.SendAsync(emailToSend);
        }
    }
}
