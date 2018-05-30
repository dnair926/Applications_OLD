namespace Applications.Core.Models
{
    /// <summary>
    /// Alert 
    /// </summary>
    public class Alert
    {
        /// <summary>
        /// Type of alert. <see cref="AlertType"/>
        /// </summary>
        public AlertType AlertType { get; set; }

        /// <summary>
        /// Alert message
        /// </summary>
        public string Message { get; set; }
    }
}
