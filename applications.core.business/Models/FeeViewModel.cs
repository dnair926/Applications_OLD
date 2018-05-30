using Applications.Core.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Applications.Core.Business.Models
{
    public class FeeViewModel: BaseModel
    {
        [Display(Name = "Rate Type")]
        [ListFormField(fieldType: FormFieldType.Dropdown, itemsPropertyName: nameof(RateTypes), descriptionPropertyName: nameof(RateTypeDescription), DisplayOrder = 10)]
        public string RateTypeId { get; set; }

        public IEnumerable<SelectListItem> RateTypes { get; set; }

        public string RateTypeDescription { get; set; }

        [Display(Name = "Retainer?")]
        [FormField(fieldType: FormFieldType.CheckBox, DisplayOrder = 20)]
        public bool Retainer { get; set; }

        [Display(Name = "Retainer Amount")]
        [FormField(fieldType: FormFieldType.Number, DisplayOrder = 30)]
        public string RetainerAmount { get; set; }

        [Display(Name = "Deposit?")]
        [FormField(fieldType: FormFieldType.CheckBox, DisplayOrder = 40)]
        public bool Deposit { get; set; }

        [Display(Name = "Deposit Amount")]
        [FormField(fieldType: FormFieldType.Number, DisplayOrder = 50)]
        public string DepositAmount { get; set; }
    }
}
