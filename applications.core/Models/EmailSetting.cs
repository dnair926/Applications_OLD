namespace Applications.Core.Models
{
    /// <summary>
    /// Email settings for the application
    /// </summary>
    public class EmailSetting
    {
        /// <summary>
        /// From address
        /// </summary>
        public string EmailFromAddress { get; set; }

        /// <summary>
        /// Location to drop the emails to be picked up by exchange
        /// </summary>
        public object EmailDropLocation { get; set; }

        /// <summary>
        /// Test email to sent the emails to for non-prod environments
        /// </summary>
        public string TestEmail { get; set; }
    }
}