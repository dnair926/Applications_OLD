namespace Applications.Core.Business.Models
{
    /// <summary>
    /// Column information for a list
    /// </summary>
    public class ColumnInformation
    {
        /// <summary>
        /// Specify whether the column is sortable
        /// </summary>
        public bool Sortable { get; set; }

        /// <summary>
        /// Name of the property to sort the collection
        /// </summary>
        public string SortColumnName { get; set; }

        /// <summary>
        /// Column heading text
        /// </summary>
        public string HeaderText { get; set; }

        /// <summary>
        /// Name of item property to get the value from
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// Display order of column in the list
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// Specify whether the column is a profile identifier column
        /// </summary>
        public string ProfileIdentifierColumn { get; set; }
    }
}
