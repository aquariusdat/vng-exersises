using Microsoft.Extensions.Logging;
using MimeKit;
using MimeKit.Text;
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

        public async Task<bool> SendAsync(string EmailTo, string EmailSubject, string EmailBody)
        {
            try
            {
                MimeMessage email_Message = new MimeMessage();
                email_Message.From.Add(MailboxAddress.Parse(_mailSettings.Email));
                email_Message.To.Add(MailboxAddress.Parse(EmailTo));

                email_Message.Subject = EmailSubject;
                BodyBuilder emailBodyBuilder = new BodyBuilder();
                emailBodyBuilder.TextBody = EmailBody;
                //email_Message.Body = emailBodyBuilder.ToMessageBody();
                email_Message.Body = new TextPart(TextFormat.Html) { Text = EmailBody };

                //this is the SmtpClient class from the Mailkit.Net.Smtp namespace, not the System.Net.Mail one
                SmtpClient MailClient = new SmtpClient();
                MailClient.Connect(_mailSettings.Host, _mailSettings.Port, _mailSettings.UseSSL);
                MailClient.Authenticate(_mailSettings.Email, _mailSettings.Password);
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
