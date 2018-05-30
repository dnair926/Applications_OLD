using Applications.Core.Business.Data;
using Applications.Core.Business.Models;
using Applications.Core.Infrastructure;
using Applications.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Applications.Core.Business.Services
{
    public class AssignmentService : IAssignmentService
    {
        readonly DbContext context;
        readonly IObjectMapper mapper;
        readonly IListService<AssignmentViewModel> listService;

        public AssignmentService(
            DbContext context,
            IObjectMapper mapper,
            IListService<AssignmentViewModel> listService)
        {
            this.context = context;
            this.mapper = mapper;
            this.listService = listService;
        }

        public FilteredListInformation<AssignmentViewModel, AssignmentCriteria> GetWorklistItems(FilteredListInformation<AssignmentViewModel, AssignmentCriteria> listInformation)
        {
            if (listInformation == null)
            {
                return null;
            }

            var personId = listInformation?.FilterFormInformation?.Model?.PersonID ?? 0;
            var result = from a in this.context.Set<Data.Assignment>()
                         join p in this.context.Set<Person>() on a.AssignedToID equals p.ID
                         join t in this.context.Set<Task>() on a.TaskID equals t.ID
                         where personId == 0 || a.AssignedToID == personId
                         select new Models.Assignment()
                         {
                             AssignmentDescription = t.Name,
                             AssignedToID = a.AssignedToID,
                             AssignedOn = a.AssignedOn,
                             AssignedToFirstName = p.FirstName,
                             AssignedToLastName = p.LastName,
                             AssignedToMiddleName = p.MiddleName,
                             DueOn = (t.DueInMinutes ?? 0) > 0 ? a.AssignedOn.AddMinutes(t.DueInMinutes.Value) : default(DateTime?),
                             EscalateOn = (t.EscalateInMinutes ?? 0) > 0 ? a.AssignedOn.AddMinutes(t.EscalateInMinutes.Value) : default(DateTime?),
                             Escalated = a.Escalated ?? false,
                         };

            var items = result
                .GroupBy(r => new Models.Assignment()
                {
                    AssignmentDescription = r.AssignmentDescription,
                    AssignedOn = r.AssignedOn,
                    DueOn = r.DueOn,
                    EscalateOn = r.EscalateOn,
                    Escalated = r.Escalated
                })
                .Select(r => mapper.Map<AssignmentViewModel>(r))
                .ToList();

            listService.UpdateListInformation(listInformation, items);

            return listInformation;
        }
    }
}
