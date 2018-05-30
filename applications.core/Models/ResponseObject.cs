using System.Collections.Generic;

namespace Applications.Core.Models
{
    /// <summary>
    /// API response object
    /// </summary>
    public class ResponseObject
    {
        /// <summary>
        /// Validation messages to be returned
        /// </summary>
        public IEnumerable<string> ValidationMessages { get; set; }

        /// <summary>
        /// Result type
        /// </summary>
        /// <seealso cref="ResultTypes"/>
        public ResultTypes Result { get; set; }

        /// <summary>
        /// Alert information
        /// </summary>
        /// <seealso cref="Alert"/>
        public Alert Alert { get; set; }

        /// <summary>
        /// Message to display
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Return object
        /// </summary>
        public object ReturnObject { get; set; }
    }
}
