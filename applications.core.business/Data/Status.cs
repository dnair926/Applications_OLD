using Applications.Core.Repository.Models;
using System.ComponentModel.DataAnnotations;

namespace Applications.Core.Business.Data
{
    /// <summary>
    /// Status reference entity
    /// </summary>
    public class Status : IEntity
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Status description
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Description { get; set; }
    }
}
