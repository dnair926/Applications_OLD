using System.ComponentModel.DataAnnotations;

namespace Applications.Core.Business.Data
{
    /// <summary>
    /// Relationship entity
    /// </summary>
    public class Relationship
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Foreign key to <see cref="Person.ID"/>
        /// </summary>
        [Required]
        public int FirstPersonID { get; set; }

        /// <summary>
        /// First person in relationship represented by <see cref="FirstPersonID"/>
        /// </summary>
        public Person FirstPerson { get; set; }

        /// <summary>
        /// Foreign key to <see cref="Person.ID"/>
        /// </summary>
        [Required]
        public int SecondPersonID { get; set; }

        /// <summary>
        /// Second person in relationship represented by <see cref="SecondPersonID"/>
        /// </summary>
        public Person SecondPerson { get; set; }

    }
}
