using Applications.Core.Attributes;
using Applications.Core.Business.Data;
using Applications.Core.Models;
using AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace Applications.Core.Business.Models
{
    public class TaskViewModel : BaseListModel, IMap
    {
        public int ID { get; set; }

        [Display(Name = "Name")]
        [MaxLength(100, ErrorMessage = "Name must be 100 characters or less.")]
        [FormField(fieldType: FormFieldType.TextBox, DisplayOrder = 10)]
        [ListColumn(displayOrder: 10)]
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        [MaxLength(1000, ErrorMessage = "Description must be 1000 characters or less.")]
        [FormField(fieldType: FormFieldType.TextBox, DisplayOrder = 20)]
        [ListColumn(displayOrder: 20)]
        public string Description { get; set; }

        [Display(Name = "Due In (Minutes)")]        
        [NumberField(MinValue = 0, DisplayOrder = 30)]
        [ListColumn(displayOrder: 30)]
        public int DueAfter { get; set; }

        [Display(Name = "Escalate After (Minutes)")]
        [NumberField(MinValue = 0, DisplayOrder = 40)]
        [ListColumn(displayOrder: 40)]
        public int EscalateAfter { get; set; }

        public void CreateMap(Profile profile)
        {
            profile.CreateMap<Task, TaskViewModel>()
                .AfterMap((src, dest) => {
                    dest.ShowEdit = true;
                    dest.ShowRemove = true;
                });

            profile.CreateMap<TaskViewModel, Task>();
        }
    }
}
