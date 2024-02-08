using DiscussionForumAPI.Contracts;
using DiscussionForumAPI.Helper;
using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Security;
using DiscussionForumAPI.Controllers;
using System.Net;
using System.Net.Mail;

namespace DiscussionForumAPI.Service
{
    /// <summary Author = Kirti Garg>
    /// This is service class that contains all the implementation of methods related to mail.
    /// </summary>
    public class MailService : IMail
    {
        private readonly EmailSettings _mailSettings;
        private readonly ILogger<AuthenticateController> _logger;
        public MailService(IOptions<EmailSettings> mailSettingsOptions, ILogger<AuthenticateController> logger)
        {
            _mailSettings = mailSettingsOptions.Value;
            _logger = logger;
        }

        public  bool SendMailAsync(MailData mailData)
        {
            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(_mailSettings.SenderEmail, _mailSettings.SenderName);
                    mail.To.Add(mailData.EmailToId);
                    mail.Subject = mailData.EmailSubject;
                    mail.Body = mailData.EmailBody;
                    mail.IsBodyHtml = true; 
                    using (SmtpClient smtp = new SmtpClient(_mailSettings.Server, _mailSettings.Port))
                    {
                        smtp.Credentials = new NetworkCredential(_mailSettings.SenderEmail, _mailSettings.Password);
                        smtp.EnableSsl = true;
                        smtp.Send(mail);
                    }


                }
                return true;
            }
            catch (Exception ex)
            {
                // Exception Details
                _logger.LogError(ex.Message, ex);
                return false;
            }
        }
    }
}

