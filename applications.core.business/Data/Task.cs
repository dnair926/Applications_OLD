using Applications.Core.Repository.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Applications.Core.Business.Data
{
    /// <summary>
    /// Task entity
    /// </summary>
    public class Task : IEntity, IMap
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Name of task
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// Task description
        /// </summary>
        [MaxLength(1000)]
        public string Description { get; set; }

        /// <summary>
        /// When to escalate the assignment
        /// </summary>
        public int? EscalateInMinutes { get; set; }

        /// <summary>
        /// Assignment due date
        /// </summary>
        public int? DueInMinutes { get; set; }

        /// <summary>
        /// Assignments
        /// </summary>
        public List<Assignment> Assignments { get; set; }

        /// <summary>
        /// Create object maps
        /// </summary>
        /// <param name="profile">Automapper <see cref="Profile"/></param>
        public void CreateMap(Profile profile)
        {
            profile.CreateMap<Task, Task>();
        }
    }
}
