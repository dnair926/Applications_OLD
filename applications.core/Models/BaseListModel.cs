namespace Applications.Core.Models
{
    using Applications.Core;

    /// <summary>
    /// Base class for list model
    /// </summary>
    public class BaseListModel : BaseModel
    {
        /// <summary>
        /// Show/Hide Edit button column
        /// </summary>
        public bool ShowEdit { get; set; }

        /// <summary>
        /// Show/Hide remove column
        /// </summary>
        public bool ShowRemove { get; set; }
    }
}