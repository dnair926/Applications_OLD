using Applications.Core.Attributes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Applications.Core.Business.Models
{
    public class UserProfile
    {
        [Description("First Name")]
        [Required(ErrorMessage = "First Name is required.")]
        [FormField(fieldType: FormFieldType.TextBox)]
        public string FirstName { get; set; }

        [Description("Last Name")]
        [Required(ErrorMessage = "Last Name is required.")]
        [FormField(fieldType: FormFieldType.TextBox)]
        public string LastName { get; set; }

        [Description("Middle Name")]
        [FormField(fieldType: FormFieldType.TextBox)]
        public string MiddleName { get; set; }
    }
}