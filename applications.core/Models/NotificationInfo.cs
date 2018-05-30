namespace Applications.Core.Models
{
    using System.Collections.Generic;
    /// <summary>
    /// Notification information
    /// </summary>
    public class NotificationInfo
    {
        /// <summary>
        /// Notification recipients
        /// </summary>
        public IEnumerable<string> Recipients { get; set; }

        /// <summary>
        /// Blind copy recipients
        /// </summary>
        public IEnumerable<string> BlindCopyRecipients { get; set; }

        /// <summary>
        /// Carbon copy recipients
        /// </summary>
        public IEnumerable<string> CarbonCopyRecipients { get; set; }

        /// <summary>
        /// Notification subject
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Notification body
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// Sender email
        /// </summary>
        public string SenderEmail { get; set; }
    }
}