namespace Applications.Core.Attributes
{
    /// <summary>
    /// Form field types
    /// </summary>
    public enum FormFieldType
    {
        /// <summary>
        /// Default value
        /// </summary>
        Unspecified,
        /// <summary>
        /// Textbox field
        /// </summary>
        TextBox,
        /// <summary>
        /// Textarea field
        /// </summary>
        MultilineTextBox,
        /// <summary>
        /// Textbox with date picker
        /// </summary>
        DatePicker,
        /// <summary>
        /// Checkbox field
        /// </summary>
        CheckBox,
        /// <summary>
        /// Radiobutton list field
        /// </summary>
        RadiobuttonList,
        /// <summary>
        /// Checkbox list field
        /// </summary>
        CheckBoxList,
        /// <summary>
        /// Dropdown list field
        /// </summary>
        Dropdown,
        /// <summary>
        /// Cascading dropdown list field
        /// </summary>
        CascadingDropdown,
        /// <summary>
        /// Label field
        /// </summary>
        Label,
        /// <summary>
        /// Textbox with autocomplete field
        /// </summary>
        Autocomplete,
        /// <summary>
        /// Phone number textbox field
        /// </summary>
        Phone,
        /// <summary>
        /// number textbox field
        /// </summary>
        Number,
    }
}
