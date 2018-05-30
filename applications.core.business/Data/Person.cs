using Applications.Core.Repository.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Applications.Core.Business.Data
{
    /// <summary>
    /// Person entity
    /// </summary>
    public class Person: BaseModel, IEntity
    {
        /// <summary>
        /// ID. Primary key
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// First name
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        /// <summary>
        /// Last name
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        /// <summary>
        /// Middle Name
        /// </summary>
        [MaxLength(10)]
        public string MiddleName { get; set; }

        /// <summary>
        /// User id
        /// </summary>
        [MaxLength(300)]
        public string UserID { get; set; }

        /// <summary>
        /// Assignments
        /// </summary>
        public List<Assignment> Assignments { get; set; }

        /// <summary>
        /// Birth day
        /// </summary>
        public DateTime BirthDay { get; set; }
    }
}
