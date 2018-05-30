using System;

namespace Applications.Core.Attributes
{
    /// <summary>
    /// Description of list column
    /// </summary>
    public sealed class ListColumnAttribute : Attribute
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="enableSorting">Set <see cref="EnableSorting"/></param>
        /// <param name="headerText">Set <see cref="HeaderText"/></param>
        /// <param name="displayOrder">Set <see cref="DisplayOrder"/></param>
        /// <param name="sortColumnName">Set <see cref="SortColumnName"/></param>
        public ListColumnAttribute(bool enableSorting = true, string headerText = "", int displayOrder = 0, string sortColumnName = "")
        {
            EnableSorting = enableSorting;
            HeaderText = headerText;
            DisplayOrder = displayOrder;
            SortColumnName = sortColumnName;
        }

        /// <summary>
        /// Column heading caption.
        /// </summary>
        public string HeaderText { get; set; }

        /// <summary>
        /// Order which the column appears in the list.
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// Enable/Disable sorting
        /// </summary>
        public bool EnableSorting { get; set; }

        /// <summary>
        /// Name of column to sort the list when sorting by this column.
        /// </summary>
        public string SortColumnName { get; set; }

        /// <summary>
        /// Specify whether to hide the column if all the rows of this column are empty.
        /// </summary>
        public bool HideEmptyColumn { get; set; }

        /// <summary>
        /// Show/Hide heading caption
        /// </summary>
        public bool HideCaption { get; set; }
    }
}
