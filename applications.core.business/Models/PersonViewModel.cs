using Applications.Core.Attributes;
using Applications.Core.Business.Data;
using AutoMapper;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Applications.Core.Business.Models
{
    public class PersonViewModel : BaseModel, IMap
    {
        [Display(Name = "Prefix")]
        [ListFormField(fieldType: FormFieldType.Dropdown, itemsPropertyName: nameof(Prefixes), descriptionPropertyName: nameof(PrefixDescription), DisplayOrder = 0)]
        public string PrefixId { get; set; }

        public IEnumerable<SelectListItem> Prefixes { get; set; }

        [Required(ErrorMessage = "First Name is required.")]
        [Display(Name = "First Name")]
        [FormField(fieldType: FormFieldType.TextBox, DisplayOrder = 10)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        [Display(Name = "Last Name")]
        [FormField(fieldType: FormFieldType.TextBox, DisplayOrder = 30)]
        public string LastName { get; set; }

        [Display(Name = "Middle Name")]
        [FormField(fieldType: FormFieldType.TextBox, DisplayOrder = 20)]
        public string MiddleName { get; set; }

        public string PrefixDescription { get; set; }

        [Display(Name = "Suffix")]
        [ListFormField(fieldType: FormFieldType.Dropdown, itemsPropertyName: nameof(Suffixes), descriptionPropertyName: nameof(SuffixDescription), DisplayOrder = 50)]
        public int SuffixId { get; set; }

        public IEnumerable<SelectListItem> Suffixes { get; set; }

        public string SuffixDescription { get; set; }

        public string FamilyTitleDescription { get; set; }

        public string OtherFamilyTitle { get; set; }

        public void CreateMap(Profile profile)
        {
            profile.CreateMap<Person, PersonViewModel>()
                .ReverseMap();
        }
    }
}
