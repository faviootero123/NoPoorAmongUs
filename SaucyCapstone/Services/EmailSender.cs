using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity.UI.Services;
using MimeKit;

namespace SaucyCapstone.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailConfiguration _emailConfig;
        public EmailSender(EmailConfiguration emailConfig)
        {
            _emailConfig = emailConfig;
        }
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var emailToSend = new MimeMessage();
            emailToSend.From.Add(new MailboxAddress("saucycoders", _emailConfig.From));
            emailToSend.To.Add(MailboxAddress.Parse(email));
            emailToSend.Subject = subject;
            emailToSend.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = htmlMessage };

            Task result;
            //send email with G-mail
            using (var emailClient = new SmtpClient())
            {
                try
                {
                    emailClient.Connect(_emailConfig.SmtpServer, _emailConfig.Port, false);
                    emailClient.Authenticate(_emailConfig.UserName, _emailConfig.Password);
                    emailClient.Send(emailToSend);

                    result = Task.CompletedTask;
                }
                catch (Exception ex)
                {
                    result = Task.FromException(ex);
                }
                finally
                {
                    emailClient.Disconnect(true);
                    emailClient.Dispose();
                }
            }

            return result;
        }
    }
}
