namespace Applications.Core.Business.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// List information
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ListInformation<T>
    {
        /// <summary>
        /// Show/Hide header
        /// </summary>
        public bool ShowHeader { get; set; } = true;

        /// <summary>
        /// Show/Hide footer
        /// </summary>
        public bool ShowFooter { get; set; } = false;

        /// <summary>
        /// List items
        /// </summary>
        public IEnumerable<T> Items { get; set; }

        /// <summary>
        /// Item for footer
        /// </summary>
        public T FooterItem { get; set; }

        /// <summary>
        /// Name of column the list is being sorted by
        /// </summary>
        public string SortExpression { get; set; }

        /// <summary>
        /// Name of column to sort the list by
        /// </summary>
        public string NewSortExpression { get; set; }

        /// <summary>
        /// Direction in which the column is being sorted.
        /// </summary>
        /// <seealso cref="ListSortDirection"/>
        public ListSortDirection SortDirection { get; set; }

        /// <summary>
        /// Direction in which the column should be sorted by default.
        /// </summary>
        /// <seealso cref="ListSortDirection"/>
        public ListSortDirection DefaultSortDirection { get; set; }

        /// <summary>
        /// Name of column to sort the list by default
        /// </summary>
        public string DefaultSortExpression { get; set; }

        /// <summary>
        /// List pager information
        /// </summary>
        /// <seealso cref="PagerInformation"/>/>
        public PagerInformation Pager { get; set; }

        /// <summary>
        /// Show/Hide edit link column
        /// </summary>
        public bool ShowEdit { get; set; }

        /// <summary>
        /// Show/Hide remove link column
        /// </summary>
        public bool ShowRemove { get; set; }

        /// <summary>
        /// Show/Hide item count
        /// </summary>
        public bool ShowCount { get; set; }

        /// <summary>
        /// Enable/Disable manual list refresh
        /// </summary>
        public bool AllowManualRefresh { get; set; }

        /// <summary>
        /// Message to be displayed when no items to be displayed
        /// </summary>
        public string EmptyDataMessage { get; set; }

        /// <summary>
        /// Message to be displayed when loading the list
        /// </summary>
        public string DataLoadingMessage { get; set; }

        /// <summary>
        /// Add Button text
        /// </summary>
        public string AddButtonText { get; set; }

        /// <summary>
        /// List title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// List column information
        /// </summary>
        /// <seealso cref="ColumnInformation"/>
        public List<ColumnInformation> Columns { get; set; }
    }
}