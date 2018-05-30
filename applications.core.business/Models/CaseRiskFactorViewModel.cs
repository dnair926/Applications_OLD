using Applications.Core.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Applications.Core.Business.Models
{
    public class CaseRiskFactorViewModel: BaseModel
    {
        [Display(Name = "Matter represented by another firm/counsel?")]
        [ListFormField(fieldType: FormFieldType.RadiobuttonList, itemsPropertyName: nameof(Confirmations), descriptionPropertyName: nameof(RepresentedByAnotherFirmDescription), DisplayOrder = 10, ShowClearButton = true)]
        public string RepresentedByAnotherFirm { get; set; }

        public string RepresentedByAnotherFirmDescription { get; set; }

        [Display(Name = "Name of previous firm/counsel")]
        [MaxLength(100, ErrorMessage = "Name of previous firm/counsel must be 100 characters or less.")]
        [FormField(fieldType: FormFieldType.TextBox, DisplayOrder = 20)]
        public string NameOfPriorFirm { get; set; }

        [Display(Name = "Money owed to previous firm/counsel by client")]
        [ListFormField(fieldType: FormFieldType.RadiobuttonList, itemsPropertyName: nameof(Confirmations), descriptionPropertyName: nameof(ClientOweMoneyToPreviousFirmDescription), DisplayOrder = 30, ShowClearButton = true)]
        public string ClientOweMoneyToPreviousFirm { get; set; }

        public string ClientOweMoneyToPreviousFirmDescription { get; set; }

        [Display(Name = "Money owed")]
        [MaxLength(25, ErrorMessage = "Money owed cannot be more than 25 characters.")]
        [FormField(fieldType: FormFieldType.TextBox, DisplayOrder = 40)]
        public string MoneyOwed { get; set; }

        [Display(Name = "Have you obtained permission from client to talk to previous firm/counsel?")]
        [ListFormField(fieldType: FormFieldType.RadiobuttonList, itemsPropertyName: nameof(Confirmations), descriptionPropertyName: nameof(PermissionToTalktoPreviousFirmDescription), DisplayOrder = 50, ShowClearButton = true)]
        public string PermissionToTalkToPreviousFirm { get; set; }

        public string PermissionToTalktoPreviousFirmDescription { get; set; }

        [Display(Name = "Reason for not obtaining permission")]
        [MaxLength(100, ErrorMessage = "Reason for not obtaining permission must be 100 characters or less")]
        [FormField(fieldType: FormFieldType.TextBox, DisplayOrder = 60)]
        public string ReasonForNotObtainingPermission { get; set; }

        [Display(Name = "Have you discussed with previous firm/counsel?")]
        [ListFormField(fieldType: FormFieldType.RadiobuttonList, itemsPropertyName: nameof(Confirmations), descriptionPropertyName: nameof(DiscussedWithPriorFirmDescription), DisplayOrder = 70, ShowClearButton = true)]
        public string DiscussedWithPriorFirm { get; set; }

        public string DiscussedWithPriorFirmDescription { get; set; }

        [Display(Name = "Reason for changing counsel")]
        [MaxLength(100, ErrorMessage = "Reason for changing counsel must be 100 characters or less")]
        [FormField(fieldType: FormFieldType.TextBox, DisplayOrder = 80)]
        public string ReasonForChangingFirm { get; set; }

        [Display(Name = "Reason for not discussing with previous firm/counsel")]
        [MaxLength(100, ErrorMessage = "Reason for not discussing with previous firm/counsel must be 100 characters or less")]
        [FormField(fieldType: FormFieldType.TextBox, DisplayOrder = 90)]
        public string ReasonNotDiscussingWithPriorFirm { get; set; }

        public IEnumerable<SelectListItem> Confirmations { get; set; }
    }
}
