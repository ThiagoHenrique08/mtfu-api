using SendGrid.Helpers.Mail;
using SendGrid;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace MoreThanFollowUp.API.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly ISendGridClient _sendGridClient;

        public EmailSender(ISendGridClient sendGridClient)
        {
            _sendGridClient = sendGridClient;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var message = new SendGridMessage()
            {
                From = new EmailAddress("your-email@example.com", "Your Name"),
                Subject = subject,
                HtmlContent = htmlMessage
            };
            message.AddTo(new EmailAddress(email));

            await _sendGridClient.SendEmailAsync(message);
        }
    }

}
