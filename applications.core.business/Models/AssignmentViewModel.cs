using System;
using System.Collections.Generic;
using System.Linq;
using Applications.Core.Attributes;
using Applications.Core.Models;
using AutoMapper;

namespace Applications.Core.Business.Models
{
    public class AssignmentViewModel : BaseListModel, IMap
    {
        [ListColumn(headerText: "Description", displayOrder: 0)]
        public string TaskDescription { get; set; }

        public DateTime AssignedOn { get; set; }

        [ListColumn(headerText: "Assigned On", displayOrder: 10, sortColumnName: nameof(AssignedOn))]
        public string AssignedOnFormatted { get; set; }

        public DateTime? DueOn { get; set; }

        [ListColumn(headerText: "Due On", displayOrder: 20, sortColumnName: nameof(DueOn))]
        public string DueOnFormatted { get; set; }

        public DateTime? EscalateOn { get; set; }

        [ListColumn(headerText: "Escalate On", displayOrder: 30, sortColumnName: nameof(EscalateOn))]
        public string EscalateOnFormatted { get; set; }

        public bool Escalated { get; set; }

        public IEnumerable<ApplicationPerson> AssignedToName { get; set; }

        public void CreateMap(Profile profile)
        {
            profile.CreateMap<Assignment, AssignmentViewModel>()
                .ForMember(dest => dest.AssignedOnFormatted, opt => opt.MapFrom(src => src.AssignedOn))
                .ForMember(dest => dest.DueOnFormatted, opt => opt.MapFrom(src => src.DueOn));

            profile.CreateMap<IGrouping<Assignment, Assignment>, AssignmentViewModel>()
                .ForMember(m => m.TaskDescription, opt => opt.MapFrom(src => src.Key.AssignmentDescription))
                .ForMember(m => m.AssignedOn, opt => opt.MapFrom(src => src.Key.AssignedOn))
                .ForMember(m => m.DueOn, opt => opt.MapFrom(src => src.Key.DueOn))
                .ForMember(m => m.AssignedOnFormatted, opt => opt.MapFrom(src => src.Key.AssignedOn))
                .ForMember(m => m.DueOnFormatted, opt => opt.MapFrom(src => src.Key.DueOn))
                .ForMember(m => m.EscalateOnFormatted, opt => opt.MapFrom(src => src.Key.EscalateOn))
                .ForMember(m => m.Escalated, opt => opt.MapFrom(src => src.Key.Escalated))
                .ForMember(m => m.AssignedToName, opt => opt.MapFrom(src => src.ToList()));
        }
    }
}
