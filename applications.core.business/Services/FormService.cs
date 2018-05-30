using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Applications.Core.Attributes;
using Applications.Core.Business.Models;
using Applications.Core.Common;

namespace Applications.Core.Business.Services
{
    public class FormService : IFormService
    {
        private readonly IModelService modelService;

        public FormService(IModelService modelService)
        {
            this.modelService = modelService;
        }

        public void UpdateFormInformation<T>(FormInformation<T> formInformation) where T : class, IBaseModel
        {
            if (formInformation == null)
            {
                return;
            }

            this.modelService.ProcessModel(formInformation.Model);

            formInformation.Fields = GetFormFields(formInformation.Model)?.OrderBy(c => c.DisplayOrder)?.ToList();
        }

        private IEnumerable<Field> GetFormFields<T>(T item) where T : class, IBaseModel
        {
            if (item == null)
            {
                yield break;
            }

            var properties = item.GetProperties();
            if ((properties?.Count ?? 0) == 0)
            {
                yield break;
            }

            foreach (var key in properties.Keys)
            {
                if (!(properties[key] is PropertyDescriptor propertyDescriptor))
                {
                    continue;
                }

                if (!(propertyDescriptor.Attributes[typeof(FormFieldAttribute)] is FormFieldAttribute fieldAttribute))
                {
                    continue;
                }

                var listFieldAttribute = propertyDescriptor.Attributes[typeof(ListFormFieldAttribute)] as ListFormFieldAttribute;
                var listItemsPropertyName = listFieldAttribute?.ItemsPropertyName;
                //var items = !string.IsNullOrWhiteSpace(listItemsPropertyName) ? item.GetPropertyValue(listItemsPropertyName) as IEnumerable<SelectListItem> : null;

                var requiredAttribute = propertyDescriptor.Attributes[typeof(RequiredAttribute)] as RequiredAttribute;
                var caption = GetCaption(propertyDescriptor);
                var name = propertyDescriptor.Name;
                var field = new Field()
                {
                    Caption = caption,
                    Name  = name.ToCamelCase(),
                    Accessor = name?.ToCamelCase(),
                    Required = requiredAttribute != null,
                    FieldType = fieldAttribute.FormFieldType,
                    DisplayOrder = fieldAttribute.DisplayOrder,
                    ListItemsAccessor = listItemsPropertyName?.ToCamelCase(),
                    HelpInfoAccessor = fieldAttribute?.HelpInfoPropertyName?.ToCamelCase(),
                    DescriptionAccessor = listFieldAttribute?.DescriptionPropertyName?.ToCamelCase(),
                };

                yield return field;
            }
        }

        private string GetCaption(PropertyDescriptor propertyDescriptor)
        {
            var fieldAttribute = propertyDescriptor.Attributes[typeof(FormFieldAttribute)] as FormFieldAttribute;
            if (!string.IsNullOrWhiteSpace(fieldAttribute?.Caption))
            {
                return fieldAttribute.Caption;
            }

            var displayAttribute = propertyDescriptor.Attributes[typeof(DisplayAttribute)] as DisplayAttribute;
            if (!string.IsNullOrWhiteSpace(displayAttribute?.Name))
            {
                return displayAttribute.Name;
            }

            return propertyDescriptor.Name;
        }
    }
}
