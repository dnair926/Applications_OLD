namespace Applications.Core.Attributes
{
    /// <summary>
    /// Number form field
    /// </summary>
    public class NumberFieldAttribute : FormFieldAttribute
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public NumberFieldAttribute() : base(fieldType: FormFieldType.Number)
        {
        }

        /// <summary>
        /// Lowest number allowed in the field
        /// </summary>
        public int MinValue { get; set; } = int.MinValue;

        /// <summary>
        /// Highest number allowed in the field
        /// </summary>
        public int MaxValue { get; set; } = int.MaxValue;
    }
}
