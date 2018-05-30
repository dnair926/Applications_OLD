namespace Applications.Core.Business.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using Applications.Core;
    using Applications.Core.Attributes;
    using Applications.Core.Business.Models;

    public class ModelService : IModelService
    {
        public void ProcessModel<TModel>(TModel model) where TModel : class, IBaseModel
        {
            profileService.SetProfileDescriptors(model);

            SetDefaultValues(model);

            SetReferenceDescriptionFieldValues(model);
        }

        public void ProcessModel<TModel>(IEnumerable<TModel> model) where TModel : class, IBaseModel
        {
            profileService.SetProfileDescriptors(model);

            SetDefaultValues(model);

            SetReferenceDescriptionFieldValues(model);
        }

        private static void SetDefaultValues<TModel>(TModel model) where TModel : class, IBaseModel
        {
            SetValues(model);
        }

        private static void SetDefaultValues<TModel>(IEnumerable<TModel> model) where TModel : class, IBaseModel
        {
            model?.ForEach(item =>
            {
                SetValues(item);
            });
        }

        private static void SetValues<TModel>(TModel model) where TModel : class, IBaseModel
        {
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
                        SetValues(baseModel);
                    }
                    else
                    {
                        var baseModels = propertyValue as IEnumerable<IBaseModel>;
                        baseModels?.ForEach(item =>
                        {
                            SetValues(item);
                        });
                    }
                }

                var inputFieldAttribute = propertyDescriptor.Attributes[typeof(FormFieldAttribute)] as FormFieldAttribute;
                if (inputFieldAttribute == null)
                {
                    continue;
                }

                var defaultValue = inputFieldAttribute.DefaultValue;
                if (defaultValue == null)
                {
                    continue;
                }

                if (propertyValue != null && !string.IsNullOrWhiteSpace(propertyValue.ToString()))
                {
                    continue;
                }

                propertyDescriptor.SetValue(model, defaultValue);

                var listFieldAttribute = inputFieldAttribute as ListFormFieldAttribute;
                if (listFieldAttribute == null)
                {
                    continue;
                }

                var descriptionPropertyName = listFieldAttribute.DescriptionPropertyName;
                if (string.IsNullOrWhiteSpace(descriptionPropertyName))
                {
                    continue;
                }

                var descriptionPropertyDescriptor = model.GetProperty(descriptionPropertyName);
                if (descriptionPropertyDescriptor == null)
                {
                    continue;
                }

                descriptionPropertyDescriptor.SetValue(model, defaultValue);
            }
        }

        private void SetReferenceDescriptionFieldValues<TModel>(IEnumerable<TModel> model) where TModel : class, IBaseModel
        {
            model?.ForEach(item =>
            {
                SetReferenceDescriptionFieldValues(item);
            });
        }

        private void SetReferenceDescriptionFieldValues<TModel>(TModel model) where TModel : class, IBaseModel
        {
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
                        SetReferenceDescriptionFieldValues(baseModel);
                    }
                    else
                    {
                        var baseModels = propertyValue as IEnumerable<IBaseModel>;
                        baseModels?.ForEach(item =>
                        {
                            SetReferenceDescriptionFieldValues(item);
                        });
                    }
                }

                var referenceFieldAttribute = propertyDescriptor.Attributes[typeof(ListFormFieldAttribute)] as ListFormFieldAttribute;
                if (referenceFieldAttribute == null)
                {
                    continue;
                }

                var descriptionFieldName = referenceFieldAttribute.DescriptionPropertyName;
                if (string.IsNullOrWhiteSpace(descriptionFieldName))
                {
                    continue;
                }

                IEnumerable<SelectListItem> list = null;
                if (!string.IsNullOrWhiteSpace(referenceFieldAttribute.ItemsPropertyName))
                {
                    var valueProperty = model?.GetType().GetProperty(referenceFieldAttribute.ItemsPropertyName);
                    list = valueProperty?.GetValue(model) as IEnumerable<SelectListItem>;
                }

                var value = list?.Where(l => string.Equals(l.Value, propertyValue?.ToString() ?? ""))?.Select(l => l.Text)?.FirstOrDefault();

                var descriptionProperty = model.GetProperty(descriptionFieldName);
                if (descriptionProperty == null)
                {
                    continue;
                }

                descriptionProperty.SetValue(model, value);
            }
        }

        public IEnumerable<KeyValuePair<string, object>> GetCurrentValues<TModel>(TModel model) where TModel : class, IBaseModel
        {
            var properties = model?.GetProperties();
            if ((properties?.Count ?? 0) == 0)
            {
                yield break;
            }

            foreach (var key in properties.Keys)
            {
                var propertyDescriptor = properties[key] as PropertyDescriptor;
                if (propertyDescriptor == null)
                {
                    continue;
                }

                var displayName = (propertyDescriptor.Attributes[typeof(DisplayAttribute)] as DisplayAttribute)?.Name;
                if (string.IsNullOrWhiteSpace(displayName))
                {
                    continue;
                }

                var propertyValue = model.GetPropertyValue(propertyDescriptor?.Name);
                yield return new KeyValuePair<string, object>(displayName, propertyValue);
            }
        }

        public ModelService(IProfileService profileService)
        {
            this.profileService = profileService;
        }

        private readonly IProfileService profileService;
    }
}