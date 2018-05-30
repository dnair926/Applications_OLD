using Applications.Core.Repository.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace Applications.Core.Business.Data
{
    /// <summary>
    /// Assignment class
    /// </summary>
    public class Assignment : IEntity
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Foreign key to <see cref="Task.ID"/>.
        /// </summary>
        [Required]
        public int TaskID { get; set; }

        /// <summary>
        /// Assigned Task represented by <see cref="TaskID"/>
        /// </summary>
        public Task Task { get; set; }

        /// <summary>
        /// Foreign key to <see cref="Person.ID"/>.
        /// </summary>
        [Required]
        public int AssignedToID { get; set; }

        /// <summary>
        /// Person the task is assigned to represented by <see cref="AssignedToID"/>
        /// </summary>
        public Person AssignedTo { get; set; }

        /// <summary>
        /// Date the task was assigned on
        /// </summary>
        [Required]
        public DateTime AssignedOn { get; set; }

        /// <summary>
        /// Date the task was completed
        /// </summary>
        public DateTime? CompletedOn { get; set; }

        /// <summary>
        /// Date the task was escalatted
        /// </summary>
        public bool? Escalated { get; set; }
    }
}
