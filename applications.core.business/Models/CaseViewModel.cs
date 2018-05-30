using Applications.Core.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Applications.Core.Business.Models
{
    public class CaseViewModel : BaseModel
    {
        [Display(Name = "Name")]
        [Required(ErrorMessage = "Name is required.")]
        [FormField(fieldType: FormFieldType.TextBox, DisplayOrder = 10)]
        [MaxLength(256)]
        public string Name { get; set; }

        [Display(Name = "Description")]
        [FormField(fieldType: FormFieldType.MultilineTextBox, DisplayOrder = 20)]
        [MaxLength(500)]
        public string Description { get; set; }

        [Display(Name = "Department")]
        [ListFormField(fieldType: FormFieldType.Dropdown, itemsPropertyName: nameof(Departments), descriptionPropertyName: nameof(DepartmentName), HelpInfoPropertyName = nameof(DepartmentHelpInfo), DisplayOrder = 30)]
        public string DepartmentID { get; set; }

        public string DepartmentName { get; set; }

        public IEnumerable<SelectListItem> Departments { get; set; }

        public string DepartmentHelpInfo { get; set; } = "Select department pertaining to the matter.";

        [Display(Name = "Practice Group")]
        [ListFormField(fieldType: FormFieldType.Dropdown, itemsPropertyName: nameof(PracticeGroups), descriptionPropertyName: nameof(PracticeGroupName), DisplayOrder = 40)]
        public string PracticeGroupID { get; set; }

        public string PracticeGroupName { get; set; }

        public IEnumerable<SelectListItem> PracticeGroups { get; set; }

        [Display(Name = "Jurisdictions")]
        [ListFormField(fieldType: FormFieldType.CheckBoxList, itemsPropertyName: nameof(Jurisdictions), descriptionPropertyName: "", DisplayOrder = 50)]
        public IEnumerable<string> JurisdictionIds { get; set; } = Enumerable.Empty<string>();

        public IEnumerable<SelectListItem> Jurisdictions { get; set; }

        [Display(Name = "Confidential Matter")]
        [FormField(fieldType: FormFieldType.CheckBox, DisplayOrder = 70)]
        public bool Confidential { get; set; }

        public IEnumerable<SelectListItem> Confirmations { get; set; }
    }
}