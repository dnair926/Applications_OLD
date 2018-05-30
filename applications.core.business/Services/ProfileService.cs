namespace Applications.Core.Business.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Applications.Core;
    using Applications.Core.Attributes;
    using System.ComponentModel;
    using Applications.Core.Repository;
    using Applications.Core.Business.Data;

    public class ProfileService : IProfileService
    {
        private readonly IRepositoryService repositoryService;

        public void SetProfileDescriptors<TModel>(TModel model) where TModel : class, IBaseModel
        {
            if (model == null)
            {
                return;
            }

            SetProfileIdentifiers(model);
        }

        public void SetProfileDescriptors<TModel>(IEnumerable<TModel> model) where TModel : class, IBaseModel
        {
            if (model == null)
            {
                return;
            }

            SetProfileIdentifiers(model);
        }

        private void SetProfileIdentifiers<TModel>(TModel model) where TModel : class, IBaseModel
        {
            var ids = GetProfileIdsFromModel(model);
            var profiles = GetProfilesByIds(ids);
            UpdateProfileDescriptionProperties(profiles, model);
        }

        private void SetProfileIdentifiers<TModel>(IEnumerable<TModel> model) where TModel : class, IBaseModel
        {
            var ids = GetProfileIdsFromModel(model);
            var profiles = GetProfilesByIds(ids);
            UpdateProfileDescriptionProperties(profiles, model);
        }

        private void UpdateProfileDescriptionProperties<TModel>(IEnumerable<Person> profiles, TModel model) where TModel : class, IBaseModel
        {
            UpdateProfileDescription(profiles, model);
        }

        private void UpdateProfileDescriptionProperties<TModel>(IEnumerable<Person> profiles, IEnumerable<TModel> model) where TModel : class, IBaseModel
        {
            model?.ForEach(item =>
            {
                UpdateProfileDescription(profiles, item);
            });
        }

        private void UpdateProfileDescription<TModel>(IEnumerable<Person> profiles, TModel model) where TModel : class, IBaseModel
        {
            if ((profiles?.Count() ?? 0) == 0)
            {
                return;
            }

            var properties = model?.GetProperties();
            if ((properties?.Count ?? 0) == 0)
            {
                return;
            }

            foreach (var key in properties.Keys)
            {
                var propertyDescriptor = properties[key] as PropertyDescriptor;
                if (propertyDescriptor == null)
                {
                    continue;
                }

                var propertyValue = propertyDescriptor.GetValue(model);
                if (propertyValue != null)
                {
                    if (propertyValue is IBaseModel baseModel)
                    {
                        UpdateProfileDescription(profiles, baseModel);
                    }
                    else
                    {
                        var baseModels = propertyValue as IEnumerable<IBaseModel>;
                        baseModels?.ForEach(item =>
                        {
                            UpdateProfileDescription(profiles, item);
                        });
                    }
                }
                var profileDescriptorAttribute = propertyDescriptor.Attributes[typeof(ProfileDescriptorAttribute)] as ProfileDescriptorAttribute;
                if (profileDescriptorAttribute == null)
                {
                    continue;
                }

                var identifierPropertyName = profileDescriptorAttribute.IdentifierPropertyName;
                if (string.IsNullOrWhiteSpace(identifierPropertyName))
                {
                    continue;
                }

                var identifierProperty = model.GetProperty(identifierPropertyName);
                if (identifierProperty == null)
                {
                    continue;
                }

                var identifierAttribute = identifierProperty.Attributes[typeof(ProfileIdentifierAttribute)] as ProfileIdentifierAttribute;
                if (identifierAttribute == null)
                {
                    continue;
                }

                var profileId = identifierProperty.GetValue(model);
                if (string.IsNullOrWhiteSpace(profileId?.ToString()))
                {
                    continue;
                }

                var profileProperty = profileDescriptorAttribute?.ProfilePropertyName;
                var valuePropertyName = !string.IsNullOrWhiteSpace(profileProperty) ? profileProperty : nameof(Person.UserID);

                var enumerable = (profileId as IEnumerable<string>)?.Where(p => !string.IsNullOrWhiteSpace(p));
                var profileIdentifierPropertyName = nameof(Person.ID);
                if (string.IsNullOrWhiteSpace(profileIdentifierPropertyName))
                {
                    continue;
                }
                object value = null;
                if (enumerable != null)
                {
                    var values = new List<string>();
                    value = enumerable?
                                .Select(item => (profiles?
                                    .Where(p => string.Equals(p.GetPropertyValue(profileIdentifierPropertyName)?.ToString(), item, StringComparison.OrdinalIgnoreCase))?
                                    .FirstOrDefault()?
                                    .GetPropertyValue(valuePropertyName) ?? item).ToString())?
                                .ToArray();
                }
                else
                {
                    value = profiles?
                        .Where(p => string.Equals(p.GetPropertyValue(profileIdentifierPropertyName)?.ToString(), profileId.ToString(), StringComparison.OrdinalIgnoreCase))?
                        .FirstOrDefault()?
                        .GetPropertyValue(valuePropertyName) ?? profileId;
                    if (value?.GetType() != propertyDescriptor.PropertyType)
                    {
                        continue;
                    }
                }

                propertyDescriptor.SetValue(model, value);
            }
        }

        public IEnumerable<Person> GetProfilesByIds(IEnumerable<int> ids)
        {
            var filteredIds = FilterProfileIds(ids);
            var totalIdCount = (filteredIds?.Count() ?? 0);
            if (totalIdCount == 0)
            {
                return null;
            }

            return repositoryService
                .GetByCriteria<Person>(p => Array.IndexOf(filteredIds.ToArray(), p.ID) > -1);
        }

        private IEnumerable<int> GetProfileIdsFromModel<TModel>(IEnumerable<TModel> model) where TModel : class, IBaseModel
        {
            var ids = new List<int>();
            model?.ForEach(item =>
            {
                var result = GetProfileIds(item);
                if ((result?.Count() ?? 0) > 0)
                {
                    ids.AddRange(result);
                }
            });

            return ids;
        }

        private IEnumerable<int> GetProfileIdsFromModel<TModel>(TModel model) where TModel : class, IBaseModel
        {
            return GetProfileIds(model);
        }

        private IEnumerable<int> GetProfileIds<TModel>(TModel model) where TModel : class, IBaseModel
        {
            var properties = model?.GetProperties();
            if ((properties?.Count ?? 0) == 0)
            {
                return null;
            }

            var ids = new List<int>();
            foreach (var key in properties.Keys)
            {
                var propertyDescriptor = properties[key] as PropertyDescriptor;
                if (propertyDescriptor == null)
                {
                    continue;
                }

                var propertyValue = propertyDescriptor.GetValue(model);
                if (string.IsNullOrWhiteSpace(propertyValue?.ToString()))
                {
                    continue;
                }

                if (propertyValue is IBaseModel baseModel)
                {
                    GetIds(baseModel, ids);
                }
                else
                {
                    var baseModels = propertyValue as IEnumerable<IBaseModel>;
                    baseModels?.ForEach(item =>
                    {
                        GetIds(item, ids);
                    });
                }

                var profileIdentifierAttribute = propertyDescriptor.Attributes[typeof(ProfileIdentifierAttribute)] as ProfileIdentifierAttribute;
                if (profileIdentifierAttribute == null)
                {
                    continue;
                }

                if (propertyValue is IEnumerable<int> enumerable)
                {
                    ids.AddRange(enumerable);
                }
                else
                {
                    ids.Add(Int32.TryParse((string)propertyValue, out int parsedValue) ? parsedValue : 0);
                }
            }

            return ids;
        }

        private void GetIds(IBaseModel item, List<int> ids)
        {
            if ((ids?.Count ?? 0) == 0)
            {
                return;
            }

            var profileIds = GetProfileIds(item);
            if ((profileIds?.Count() ?? 0) > 0)
            {
                ids.AddRange(profileIds);
            }
        }

        private IEnumerable<int> FilterProfileIds(IEnumerable<int> ids)
        {
            return ids?.Where(id => id > 0)?.Distinct()?.ToArray();
        }

        public ProfileService(IRepositoryService repositoryService)
        {
            this.repositoryService = repositoryService;
        }
    }
}