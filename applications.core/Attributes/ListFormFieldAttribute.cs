namespace Applications.Core.Attributes
{
    /// <summary>
    /// List field attribute
    /// </summary>
    public class ListFormFieldAttribute : FormFieldAttribute
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="fieldType">Field type. <see cref="FormFieldType"/></param>
        /// <param name="itemsPropertyName">Set <see cref="ItemsPropertyName"/></param>
        /// <param name="descriptionPropertyName">Set <see cref="DescriptionPropertyName"/></param>
        public ListFormFieldAttribute(FormFieldType fieldType, string itemsPropertyName, string descriptionPropertyName) : base(fieldType)
        {
            ItemsPropertyName = itemsPropertyName;
            DescriptionPropertyName = descriptionPropertyName;
        }

        /// <summary>
        /// Name of property that has the list of items to be displayed in the list field.
        /// </summary>
        public string ItemsPropertyName { get; private set; }

        /// <summary>
        /// Name of property that has the value to be displayed when the list field is disabled.
        /// </summary>
        public string DescriptionPropertyName { get; private set; }
    }
}
