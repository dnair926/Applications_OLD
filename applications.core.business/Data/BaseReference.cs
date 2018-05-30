using Applications.Core.Repository.Models;
using System.ComponentModel.DataAnnotations;

namespace Applications.Core.Business.Data
{
    /// <summary>
    /// Base class for reference classes
    /// </summary>
    public abstract class BaseReference : IEntity
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Name of item
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Order of item in list
        /// </summary>
        public int OrderInList { get; set; }

        /// <summary>
        /// Status of item represented by <see cref="StatusID"/>
        /// </summary>
        [Required]
        public Status Status { get; set; }

        /// <summary>
        /// Foreign key to <see cref="Status.ID"/>
        /// </summary>
        public int StatusID { get; set; }
    }
}
