namespace Applications.Core.Models
{
    using System;
    using System.Text;

    public class Person : DataModelBase
    {
        public Person()
            : this(new PropertyHandler())
        {
        }

        public Person(IPropertyHandler propertyHandler)
            : base(propertyHandler)
        {
        }

        public string TimekeeperID { get; set; }

        public string NetworkID { get; set; }

        public string NameFormattedFirstLast
        {
            get
            {
                return $"{(this.FirstName ?? string.Empty)} {this.MiddleName} {(this.LastName ?? "")}".Replace("  ", " ");
            }

            set { }
        }

        public string NameFormattedStandard
        {
            get
            {
                StringBuilder nameBuilder = new StringBuilder();
                if (this.TerminationDate > DateTime.MinValue)
                {
                    nameBuilder.Append("(Terminated) ");
                }
                nameBuilder.Append($"{LastName ?? ""}, {FirstName ?? ""}");
                if (!string.IsNullOrWhiteSpace(this.MiddleName))
                {
                    nameBuilder.Append($" {this.MiddleName}");
                }
                if (!string.IsNullOrWhiteSpace(this.OfficeLocationName))
                {
                    nameBuilder.Append($" ({this.OfficeLocationName})");
                }

                return nameBuilder.ToString();
            }

            set
            {
            }
        }

        public string FullName1 { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public string OfficeLocationName { get; set; }

        public string DepartmentName { get; set; }

        public DateTime? HireDate { get; set; }

        public DateTime? TerminationDate { get; set; }

        public string WorkEmail { get; set; }

        public string DepartmentCode { get; set; }

        public string OfficeLocationID { get; set; }

        public string OfficeLocationBillCode { get; set; }

        public string ExtWorkPhone { get; set; }

        public string WorkPhone { get; set; }

        public int? EmpTypeID { get; set; }

        public int? PositionID { get; set; }

        public string PositionName { get; set; }

        public bool Exists
        {
            get
            {
                return true;
            }
        }

        public bool DataModified
        {
            get
            {
                return false;
            }
        }
    }
}