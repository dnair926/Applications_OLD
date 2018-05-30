using Humanizer;

namespace Applications.Core.Common
{

    /// <summary>
    /// Helper extension methods for strings
    /// </summary>
    public static class StringHelpers
    {
        /// <summary>
        /// Convert a string value to camel case
        /// </summary>
        /// <param name="value">Value to convert</param>
        /// <returns>Camel case representation of the specified string value</returns>
        public static string ToCamelCase(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return string.Empty;
            }

            return value.Camelize();
        }

        /// <summary>
        /// Convert a string value to integer
        /// </summary>
        /// <param name="value">Integer value, if successfully converted, else <code>default(int?)</code>  </param>
        /// <returns></returns>
        public static int? ToInt(this string value)
        {
            return int.TryParse(value, out int parsedValue) ? parsedValue : default(int?);
        }
    }
}
