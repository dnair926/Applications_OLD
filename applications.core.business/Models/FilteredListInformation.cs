namespace Applications.Core.Business.Models
{
    /// <summary>
    /// Filtered list information
    /// </summary>
    /// <typeparam name="TListItem">Type of the items in list</typeparam>
    /// <typeparam name="TCriteria">Type of search criteria model</typeparam>
    public class FilteredListInformation<TListItem, TCriteria> : ListInformation<TListItem>
    {
        /// <summary>
        /// Show/Hide filter form
        /// </summary>
        public bool ShowFilterForm { get; set; }

        /// <summary>
        /// Filter Form Information
        /// </summary>
        public FormInformation<TCriteria> FilterFormInformation { get; set; }

        /// <summary>
        /// Specify whether to show add button for list
        /// </summary>
        public bool ShowAddButton { get; set; }
    }
}
