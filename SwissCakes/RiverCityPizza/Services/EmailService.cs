using Microsoft.Extensions.Options;
using SwissConfectionery.Models;
using System.Net;
using System.Net.Mail;

namespace SwissConfectionery.Services
{
    public class EmailService : IEmailService
    {
        private readonly SMTPOptions _smtpOptions;
        private readonly EmailOptions _emailOptions;
        public EmailService(IOptions<SMTPOptions> smtpOptions, IOptions<EmailOptions> exhibitorRequestInfoOptions)
        {
            _smtpOptions = smtpOptions.Value;
            _emailOptions = exhibitorRequestInfoOptions.Value;
        }

        public bool SendEmail(string fromName, string subject, string body,
            string sender, string recipients,
            bool isBodyHtml = true,
            bool isCustomerEmail = false,
            IEnumerable<string> emailAttachments = null)
        {
            bool sent = false;

            var message = new MailMessage
            {
                IsBodyHtml = isBodyHtml
                ,
                From = new MailAddress(sender, fromName)
            };

            var arrRecipients = recipients.Split(',');
            if (arrRecipients != null)
            {
                foreach (var recipient in arrRecipients)
                {
                    message.To.Add(new MailAddress(recipient));
                }
            }

            var ccRecipients = _emailOptions.Cc?.Split(';');
            if (ccRecipients != null)
            {
                foreach (var ccRecipient in ccRecipients)
                {
                    message.CC.Add(new MailAddress(ccRecipient));
                }
            }
            var bccRecipients = _emailOptions.Bcc.Split(';');
            if (bccRecipients != null && !isCustomerEmail)
            {
                foreach (var bccRecipient in bccRecipients)
                {
                    message.Bcc.Add(new MailAddress(bccRecipient));
                }
            }

            message.Subject = subject;
            message.Body = body;

            if (emailAttachments != null)
            {
                foreach (var emailAttachment in emailAttachments)
                {
                    var attachment = new Attachment(emailAttachment);
                    message.Attachments.Add(attachment);
                }

            }

            // Comment or delete the next line if you are not using a configuration set
            //message.Headers.Add("X-SES-CONFIGURATION-SET", CONFIGSET);

            using var client = new SmtpClient(_smtpOptions.Host, _smtpOptions.Port)
            {
                Credentials = new NetworkCredential(_smtpOptions.Username, _smtpOptions.Password),
                EnableSsl = _smtpOptions.EnableSSL
            };

            try
            {
                client.Send(message);
                sent = true;
            }

            catch (Exception)
            {
            }

            return sent;

        }
       
    }
}
