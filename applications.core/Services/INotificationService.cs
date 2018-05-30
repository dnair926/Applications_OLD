namespace Applications.Core.Services
{
    using Applications.Core.Models;

    /// <summary>
    /// Notification service interface
    /// </summary>
    public interface INotificationService
    {
        /// <summary>
        /// Send notification
        /// </summary>
        /// <param name="notificationInfo">Information regarding notification. <see cref="NotificationInfo"/></param>
        /// <returns>True/False indicating the success of notification</returns>
        bool SendNotification(NotificationInfo notificationInfo);
    }
}