using System;

namespace Applications.Core.Attributes
{
    /// <summary>
    /// Information regarding a field on the form
    /// </summary>
    public class FormFieldAttribute : Attribute
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="fieldType">Type of field.<see cref="FormFieldType"/></param>
        public FormFieldAttribute(
            FormFieldType fieldType)
        {
            FormFieldType = fieldType;
        }

        /// <summary>
        /// Name of property that has the information for the help text to be displayed for the field.
        /// </summary>
        public string HelpInfoPropertyName { get; set; }

        /// <summary>
        /// Caption to be displayed for the field
        /// </summary>
        public string Caption { get; set; }

        /// <summary>
        /// Order in which the field appears on the form
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// Type of field. <see cref="FormFieldType"/>
        /// </summary>
        public FormFieldType FormFieldType { get; private set; }

        /// <summary>
        /// Information to be displayed before the field. e.g Currency
        /// </summary>
        public string Prefix { get; set; }

        /// <summary>
        /// Information to be displayed after the field. e.g Percentage
        /// </summary>
        public string Suffix { get; set; }

        /// <summary>
        /// Show a button next to the field to clear the field's value
        /// </summary>
        public bool ShowClearButton { get; set; }

        /// <summary>
        /// Value to be displayed if the field's value is null
        /// </summary>
        public string DefaultValue { get; set; }
    }
}
