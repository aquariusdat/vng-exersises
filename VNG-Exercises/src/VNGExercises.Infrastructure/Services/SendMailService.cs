using Microsoft.Extensions.Logging;
using MimeKit;
using VNGExercises.Infrastructure.Abstractions;
using VNGExercises.Infrastructure.DependencyInjection.Options;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace VNGExercises.Infrastructure.Services
{
    public class SendMailService : ISendMailService
    {
        private readonly ILogger<SendMailService> _logger;
        private readonly MailSettings _mailSettings;
        public SendMailService(ILogger<SendMailService> logger, MailSettings mailSettings)
        {
            _logger = logger;
            _mailSettings = mailSettings;
        }

        public async Task<bool> SendAsync(string ToEmailId, string ToEmailName, string EmailSubject, string EmailBody)
        {
            try
            {
                MimeMessage email_Message = new MimeMessage();
                MailboxAddress email_From = new MailboxAddress(_mailSettings.Name, _mailSettings.EmailId);
                email_Message.From.Add(email_From);
                MailboxAddress email_To = new MailboxAddress(ToEmailName, ToEmailId);
                email_Message.To.Add(email_To);
                email_Message.Subject = EmailSubject;
                BodyBuilder emailBodyBuilder = new BodyBuilder();
                emailBodyBuilder.TextBody = EmailBody;
                email_Message.Body = emailBodyBuilder.ToMessageBody();

                //this is the SmtpClient class from the Mailkit.Net.Smtp namespace, not the System.Net.Mail one
                SmtpClient MailClient = new SmtpClient();
                MailClient.Connect(_mailSettings.Host, _mailSettings.Port, _mailSettings.UseSSL);
                MailClient.Authenticate(_mailSettings.EmailId, _mailSettings.Password);
                MailClient.Send(email_Message);
                MailClient.Disconnect(true);
                MailClient.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error when send mail in SendMailService [Error:::{ex.ToString()}]");
                return false;
            }
        }
    }
}
