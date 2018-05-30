namespace Applications.Core.Services
{
    using System;
    using System.IO;
    using System.Linq;
    using Microsoft.Extensions.Options;
    using MimeKit;
    using Models;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Notification service
    /// </summary>
    public class NotificationService : INotificationService
    {
        readonly EmailSetting emailSetting;
        readonly ILogger<NotificationService> logger;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="emailSettings"></param>
        /// <param name="logger"></param>
        public NotificationService(
                IOptions<EmailSetting> emailSettings,
                ILogger<NotificationService> logger)
        {
            this.emailSetting = emailSettings.Value;
            this.logger = logger;
        }

        /// <summary>
        /// Send notification.
        /// If test email is specified, then the emails will be sent to it.
        /// </summary>
        /// <param name="notificationInfo">Information regarding the notification</param>
        /// <returns>True/False indicating success of email</returns>
        public bool SendNotification(NotificationInfo notificationInfo)
        {
            if (notificationInfo == null)
            {
                return false;
            }

            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(!string.IsNullOrWhiteSpace(notificationInfo.SenderEmail) ? notificationInfo.SenderEmail : emailSetting.EmailFromAddress));

                if (notificationInfo.Recipients != null)
                {
                    if (!string.IsNullOrWhiteSpace(emailSetting.TestEmail))
                    {
                        message.To.AddRange(emailSetting.TestEmail.Split(';').Select(address => new MailboxAddress(address)));
                        notificationInfo.Body = (notificationInfo.Body ?? string.Empty) + $"<br />Actual Recipients: {string.Join(";", notificationInfo.Recipients)}";
                    }
                    else
                    {
                        message.To.AddRange(notificationInfo.Recipients.Select(address => new MailboxAddress(address)));
                    }
                }

                if (notificationInfo.BlindCopyRecipients != null)
                {
                    if (!string.IsNullOrWhiteSpace(emailSetting.TestEmail))
                    {
                        notificationInfo.Body = (notificationInfo.Body ?? string.Empty) + $"<br />Actual Bcc: {string.Join(";", notificationInfo.BlindCopyRecipients)}";
                    }
                    else
                    {
                        message.Bcc.AddRange(notificationInfo.BlindCopyRecipients.Select(address => new MailboxAddress(address)));
                    }
                }

                if (notificationInfo.CarbonCopyRecipients != null)
                {
                    if (!string.IsNullOrWhiteSpace(emailSetting.TestEmail))
                    {
                        notificationInfo.Body = (notificationInfo.Body ?? string.Empty) + $"<br />Actual cc: {string.Join(";", notificationInfo.CarbonCopyRecipients)}";
                    }
                    else
                    {
                        message.Cc.AddRange(notificationInfo.CarbonCopyRecipients.Select(address => new MailboxAddress(address)));
                    }
                }

                message.Subject = notificationInfo.Subject;
                message.Body = new TextPart("html")
                {
                    Text = notificationInfo.Body,
                };

                var fileName = $"{System.Guid.NewGuid()}.eml";
                var fullPath = $"{emailSetting.EmailDropLocation}/{fileName}";

                using (StreamWriter data = File.CreateText(fullPath))
                {
                    message.WriteTo(data.BaseStream);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "", "");
                return false;
            }

            return true;
        }
    }
}