using Applications.Core;
using Applications.Core.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Applications.Core.Repository
{
    public class ODataQueryBuilder : IODataQueryBuilder
    {
        public string GetServiceUrl(object criteria, CrudAction crudAction)
        {
            var entityName = criteria?.GetType()?.FullName;
            if (string.IsNullOrWhiteSpace(entityName))
            {
                return string.Empty;
            }

            var odataCriteria = criteria as IODataCriteria;

            ODataDefinition definition = GetODataDefinition(entityName, crudAction);

            var connetionString = !string.IsNullOrWhiteSpace(definition?.ConnectionStringName) ? configuration.GetConnectionString(definition.ConnectionStringName) : null;
            if (!string.IsNullOrWhiteSpace(connetionString))
            {
                var filter = odataCriteria.GetServiceUrl();
                return connetionString + (!string.IsNullOrWhiteSpace(filter) ? $"?$filter={filter}" : "");
            }

            return "";
        }

        private ODataDefinition GetODataDefinition(string entityName, CrudAction crudAction)
        {
            var entityConfiguration = odataMapperConfigurationSection?.Value?.FirstOrDefault(m => string.Equals(entityName, m.Name, StringComparison.OrdinalIgnoreCase));
            if (entityConfiguration == null)
            {
                return null;
            }

            ODataDefinition storedProcedureConfiguration = entityConfiguration.CommandName(crudAction);

            return storedProcedureConfiguration;
        }

        public ODataQueryBuilder(
           IConfiguration configuration,
           IOptions<List<ODataEntity>> odataMapperConfigurationSection)
        {
            this.configuration = configuration;
            this.odataMapperConfigurationSection = odataMapperConfigurationSection;
        }

        private readonly IConfiguration configuration;
        private readonly IOptions<List<ODataEntity>> odataMapperConfigurationSection;
    }
}
