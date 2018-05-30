using System.Collections.Generic;

namespace Applications.Core.Business.Models
{
    /// <summary>
    /// List pager information
    /// </summary>
    public class PagerInformation
    {
        /// <summary>
        /// Number of page buttons to show
        /// </summary>
        public int? PageNumberButtonCount { get; set; }

        /// <summary>
        /// Total item count
        /// </summary>
        public int TotalItems { get; set; }

        /// <summary>
        /// Current page index
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// Page size
        /// </summary>
        public int PageSize { get; set; } = 10;

        /// <summary>
        /// Total page count
        /// </summary>
        public int TotalPages { get; set; }

        /// <summary>
        /// Start page index
        /// </summary>
        public int StartPage { get; set; }

        /// <summary>
        /// End page index
        /// </summary>
        public int EndPage { get; set; }

        /// <summary>
        /// Start item index
        /// </summary>
        public int StartItemIndex { get; set; }

        /// <summary>
        /// End item index
        /// </summary>
        public int EndItemIndex { get; set; }

        /// <summary>
        /// Page numbers to show as page buttons
        /// </summary>
        public IEnumerable<int> Pages { get; set; }
    }
}