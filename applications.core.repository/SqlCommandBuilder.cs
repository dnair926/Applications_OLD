using Humanizer;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Applications.Core.Attributes;
using Applications.Core;
using Applications.Core.Services;

namespace Applications.Core.Repository
{
    public class SqlCommandBuilder : ISqlCommandBuilder
    {
        public void BuildSqlCommand(SqlCommand command, IBaseModel newEntity, CrudAction crudAction)
        {
            if (command == null || newEntity == null)
            {
                return;
            }

            var entityType = newEntity.GetType();
            var entityName = entityType.FullName;
            CommandDefinition commandDefinition = GetStoredProcedureConfiguration(entityName, crudAction);

            SetCommandText(commandDefinition, command, newEntity, crudAction);

            SetCommandType(commandDefinition, command);

            AddParameters(commandDefinition, command, newEntity, crudAction);

            SetupConnection(commandDefinition, command);
        }

        public static string PrintCommand(SqlCommand cmd)
        {
            if (cmd == null)
            {
                return null;
            }

            StringBuilder sb = new StringBuilder();
            if ((cmd.Connection?.ConnectionString?.Trim()?.Length ?? 0) > 0)
            {
                sb.AppendLine(cmd.Connection.ConnectionString);
            }
            sb.Append(cmd.CommandText);

            int parameterCount = 0;
            foreach (SqlParameter param in cmd.Parameters)
            {
                if (param == null)
                {
                    continue;
                }
                parameterCount += 1;
                if (parameterCount > 1)
                {
                    sb.Append(", ");
                }
                sb.Append(string.Format(" {0} = [{1}]", param.ParameterName, (param.Value == DBNull.Value || param.Value == null ? "NULL" : param.Value.ToString())));
            }
            sb.AppendLine();
            Debug.WriteLine(sb.ToString());

            return sb.ToString();
        }

        private void SetCommandType(CommandDefinition commandDefnition, SqlCommand command)
        {
            if (command == null)
            {
                return;
            }

            command.CommandType = (commandDefnition != null) ? commandDefnition.CommandType : CommandType.Text;
        }

        private void SetupConnection(CommandDefinition commandDefinition, SqlCommand command)
        {
            if (commandDefinition == null && command == null)
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(command?.Connection?.ConnectionString))
            {
                var connectionStringName = commandDefinition?.ConnectionStringName;
                connectionStringName = configuration.GetConnectionString(!string.IsNullOrWhiteSpace(connectionStringName) ? connectionStringName : "Application");
                command.Connection = new SqlConnection(connectionStringName);
            }

            if (command.Connection.State == ConnectionState.Closed)
            {
                command.Connection.Open();
            }
        }

        private void AddParameters<T>(CommandDefinition commandDefinition, SqlCommand command, T newEntity, CrudAction crudAction) where T : IBaseModel
        {
            var entityName = newEntity.GetType().Name;

            var parameters = commandDefinition?.CommandType == CommandType.Text ? SetTextCommandParameters(command, newEntity) : GetParametersWithValues<T>(newEntity);
            if (parameters == null)
            {
                return;
            }
            command.Parameters.AddRange(parameters.ToArray());
        }

        private IEnumerable<SqlParameter> GetParametersWithValues<T>(T criteriaEntity) where T : IBaseModel
        {
            var properties = GetPropertiesWithValues(criteriaEntity)?.ToList();
            if ((properties?.Count ?? 0) == 0)
            {
                yield break;
            }

            DataFieldAttribute dataFieldAttribute = null;
            string propertyName;
            foreach (var propertyInfo in properties)
            {
                dataFieldAttribute = propertyInfo.Attributes[typeof(DataFieldAttribute)] as DataFieldAttribute;
                propertyName = dataFieldAttribute?.RepositoryName;
                if (string.IsNullOrWhiteSpace(propertyName))
                {
                    propertyName = propertyInfo.Name;
                }

                var propertyValue = propertyInfo.GetValue(criteriaEntity);
                yield return new SqlParameter(string.Format(PARAMETER_FORMAT, propertyName), propertyValue);
            }
        }

        private IEnumerable<PropertyDescriptor> GetPropertiesWithValues<T>(T criteriaEntity) where T : IBaseModel
        {
            var properties = criteriaEntity?.GetProperties();
            if ((properties?.Count ?? 0) == 0)
            {
                yield break;
            }

            foreach (var key in properties.Keys)
            {
                var propertyValue = criteriaEntity.GetPropertyValue(key.ToString());
                if (propertyValue == null)
                {
                    continue;
                }

                var propertyInfo = properties[key] as PropertyDescriptor;
                if (propertyInfo == null)
                {
                    continue;
                }

                yield return propertyInfo;
            }
        }

        private IEnumerable<SqlParameter> SetTextCommandParameters<T>(SqlCommand command, T criteriaEntity) where T : IBaseModel
        {
            if ((command?.CommandText?.Trim()?.Length ?? 0) == 0)
            {
                yield break;
            }

            Regex parameterSearch = new Regex(REGEX_SEARCH_PARAMETERS, RegexOptions.Singleline | RegexOptions.CultureInvariant);
            MatchCollection matchCollection = parameterSearch.Matches(command.CommandText);
            if ((matchCollection?.Count ?? 0) == 0)
            {
                yield break;
            }

            object propertyValue = null;
            string parameterFormat = "";
            List<string> matches = new List<string>();
            foreach (Match match in matchCollection)
            {
                if (!match.Success)
                {
                    continue;
                }
                matches.Add(match.Value.ToUpperInvariant());
            }

            foreach (var value in matches.Distinct())
            {
                parameterFormat = string.Format(PARAMETER_FORMAT, value);

                if (command.Parameters.Contains(parameterFormat))
                {
                    continue;
                }

                propertyValue = criteriaEntity.GetPropertyValue(value);

                yield return new SqlParameter(string.Format(PARAMETER_FORMAT, value), propertyValue ?? DBNull.Value);
            }
        }

        private void SetCommandText(CommandDefinition commandDefinition, SqlCommand command, IBaseModel entityType, CrudAction crudAction)
        {
            var commandText = GetCommandText(commandDefinition, entityType, crudAction);
            if (string.IsNullOrWhiteSpace(commandText))
            {
                return;
            }

            command.CommandText = commandText;
        }

        private string GetCommandText(CommandDefinition commandDefinition, IBaseModel entityType, CrudAction crudAction)
        {
            var entityName = entityType?.GetType()?.Name;

            if (!string.IsNullOrWhiteSpace(commandDefinition?.Command) || !string.IsNullOrWhiteSpace(commandDefinition?.QueryFilePath))
            {
                return !string.IsNullOrWhiteSpace(commandDefinition.Command) ? commandDefinition.Command : fileService.GetFileContent(commandDefinition.QueryFilePath);
            }

            string commandText = "";
            var tableName = entityName?.Pluralize();
            if (crudAction == CrudAction.Delete)
            {
                commandText = $"DELETE FROM {tableName}";
            }
            else if (crudAction == CrudAction.Insert)
            {
                commandText = $"INSERT INTO {tableName}";
            }
            else if (crudAction == CrudAction.Select)
            {
                commandText = $"SELECT * FROM {tableName}";
            }
            else if (crudAction == CrudAction.Update)
            {
                commandText = $"UPDATE {tableName}";
            }

            var parameters = GetPropertiesWithValues(entityType)?.ToList();
            if (crudAction == CrudAction.Delete ||
                crudAction == CrudAction.Select)
            {
                var whereClause = "";
                if ((parameters?.Count ?? 0) > 0)
                {
                    whereClause = string.Join(" AND ", parameters.Select(p => $"{p.Name} = @{p.Name}"));
                    if ((whereClause?.Length ?? 0) > 0)
                    {
                        whereClause = $" WHERE {whereClause}";
                    }
                }
                commandText = $"{commandText} {whereClause}".Trim();
            }
            else if (crudAction == CrudAction.Insert)
            {
                var parameterNames = string.Join(", ", parameters?.Select(p => $"@{p.Name}")?.ToList() ?? Enumerable.Empty<string>());
                var columns = string.Join(", ", parameters?.Select(p => p.Name)?.ToList() ?? Enumerable.Empty<string>());
                commandText = $"{commandText} ({columns}) VALUES ({parameterNames})";

                var identityProperty = GetIdentityProperty(entityType)?.FirstOrDefault();
                if (identityProperty != null)
                {
                    commandText = $"{commandText} SELECT * FROM {tableName} WHERE {identityProperty.Name} = SCOPE_IDENTITY()";
                }
                else
                {
                    commandText = $"{commandText} {GetCommandText(commandDefinition, entityType, CrudAction.Select)}";
                }
            }
            else if (crudAction == CrudAction.Update)
            {
                var primaryKeyColumnNames = GetPrimaryKeyProperties(entityType)?.Select(p => p.Name)?.ToArray();
                var columnNames = entityType.GetPropertyNames()?.Where(c => Array.IndexOf(primaryKeyColumnNames, c) == -1)?.ToList();
                var updateClause = string.Join(",", columnNames?.Select(c => $"{c} = {ParameterPrefix}{c}")?.ToList() ?? Enumerable.Empty<string>());
                var whereClause = string.Join(",", primaryKeyColumnNames?.Select(c => $"{c} = {ParameterPrefix}{c}")?.ToList() ?? Enumerable.Empty<string>());
                commandText = $"SET {updateClause} {whereClause}";
            }

            return commandText;
        }

        private IEnumerable<PropertyDescriptor> GetIdentityProperty(IBaseModel entity)
        {
            var propertySet = entity.GetProperties();
            if (propertySet == null)
            {
                yield break;
            }

            foreach (var key in propertySet.Keys)
            {
                var propertyInfo = propertySet[key] as PropertyDescriptor;
                if (propertyInfo == null)
                {
                    continue;
                }

                var propertyValue = propertyInfo.GetValue(entity);
                if (propertyValue == null)
                {
                    continue;
                }

                var dataFieldAttribute = propertyInfo.Attributes[typeof(DataFieldAttribute)] as DataFieldAttribute;
                if (!dataFieldAttribute.IsIdentity)
                {
                    continue;
                }

                yield return propertyInfo;
            }
        }

        private CommandDefinition GetStoredProcedureConfiguration(string entityName, CrudAction crudAction)
        {
            var entityConfiguration = sqlMapperConfigurationSection?.Value?.FirstOrDefault(m => string.Equals(entityName, m.Name, StringComparison.OrdinalIgnoreCase));
            if (entityConfiguration == null)
            {
                return null;
            }

            CommandDefinition storedProcedureConfiguration = entityConfiguration.CommandName(crudAction);

            return storedProcedureConfiguration;
        }

        public IEnumerable<PropertyDescriptor> GetPrimaryKeyProperties(IBaseModel entity)
        {
            var propertySet = entity.GetProperties();
            if (propertySet == null)
            {
                yield break;
            }

            foreach (var key in propertySet.Keys)
            {
                var propertyInfo = propertySet[key] as PropertyDescriptor;
                if (propertyInfo == null)
                {
                    continue;
                }

                var propertyValue = propertyInfo.GetValue(entity);
                if (propertyValue == null)
                {
                    continue;
                }

                var dataFieldAttribute = propertyInfo.Attributes[typeof(DataFieldAttribute)] as DataFieldAttribute;
                if (!dataFieldAttribute.PrimaryKey)
                {
                    continue;
                }

                yield return propertyInfo;
            }
        }

        public SqlCommandBuilder(
           IConfiguration configuration,
           IOptions<List<SqlEntity>> sqlMapperConfigurationSection,
           IFileService fileService)
        {
            this.configuration = configuration;
            this.sqlMapperConfigurationSection = sqlMapperConfigurationSection;
            this.fileService = fileService;
        }

        private const string ParameterPrefix = "@";
        private const string REGEX_SEARCH_PARAMETERS = "(?<=" + ParameterPrefix + ")\\w+"; //"(?<=@)\\w+(?=[\\,\\]\\s\\=\\\"])?";
        private const string PARAMETER_FORMAT = ParameterPrefix + "{0}";
        private readonly IConfiguration configuration;
        private readonly IOptions<List<SqlEntity>> sqlMapperConfigurationSection;
        private readonly IFileService fileService;
    }
}