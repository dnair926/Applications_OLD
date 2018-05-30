using System;

namespace Applications.Core.Business.Models
{
    public class Assignment
    {
        public DateTime AssignedOn { get; set; }

        public DateTime? DueOn { get; set; }

        public bool Escalated { get; set; }

        public string AssignedToFirstName { get; set; }

        public string AssignedToLastName { get; set; }

        public string AssignedToMiddleName { get; set; }

        public int AssignedToID { get; set; }

        public string AssignmentDescription { get; set; }

        public DateTime? EscalateOn { get; set; }
    }
}
