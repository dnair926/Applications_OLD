using Applications.Core.Business.Data;
using Applications.Core.Business.Models;
using Applications.Core.Infrastructure;
using Applications.Core.Models;
using Applications.Core.Repository;
using System.Collections.Generic;
using System.Linq;

namespace Applications.Core.Business.Services
{
    public class PersonService : IPersonService
    {
        private readonly IRepositoryService repositoryService;
        private readonly IObjectMapper mapper;

        public PersonService(
            IRepositoryService repositoryService,
            IObjectMapper mapper)
        {
            this.repositoryService = repositoryService;
            this.mapper = mapper;
        }

        public void Save(FormInformation<PersonViewModel> formInformation)
        {
            if (formInformation?.Model == null)
            {
                return;
            }

            var person = mapper.Map<Person>(formInformation.Model);
            repositoryService.Save(person);
            formInformation.Model = mapper.Map<PersonViewModel>(person);
        }

        public void SetLookupValues(PersonViewModel viewModel)
        {
            if (viewModel == null)
            {
                return;
            }

            var prefixes = repositoryService.GetByCriteria<NamePrefix>(p => p.StatusID == 1)?.OrderBy(p => p.OrderInList)?.ThenBy(p => p.Name);
            viewModel.Prefixes = mapper.Map<IEnumerable<SelectListItem>>(prefixes);

            var suffixes = repositoryService.GetByCriteria<NameSuffix>(p => p.StatusID == 1)?.OrderBy(p => p.OrderInList)?.ThenBy(p => p.Name);
            viewModel.Suffixes = mapper.Map<IEnumerable<SelectListItem>>(suffixes);
        }
    }
}
