using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Applications.Core.Common
{
    /// <summary>
    /// XML utility methods
    /// </summary>
    public static class XmlExtensions
    {
        /// <summary>
        /// Convert a string representation of XML to an object
        /// </summary>
        /// <typeparam name="T">Type of object to convert the XML to</typeparam>
        /// <param name="XMLString">XML string to convert</param>
        /// <returns><para name="T" />, if the string can be converted </returns>
        public static T ToObject<T>(string XMLString) where T : class
        {
            try
            {
                if (string.IsNullOrWhiteSpace(XMLString))
                {
                    return default(T);
                }

                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                object convertedObject = xmlSerializer.Deserialize(new StringReader(XMLString));

                T convertToObject = convertedObject as T;

                return convertToObject;
            }
            catch (System.Exception)
            {
                return default(T);
            }
        }

        /// <summary>
        /// Convert an object to XML string
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <param name="convertFromObject">Object to convert</param>
        /// <returns></returns>
        public static string ToXMLString<T>(this T convertFromObject) where T : class
        {
            try
            {
                if (convertFromObject == null)
                {
                    return "NULL object passed in";
                }

                XmlDocument xmlDoc = new XmlDocument();
                XmlSerializer xmlSerializer = new XmlSerializer(convertFromObject.GetType());
                using (MemoryStream xmlStream = new MemoryStream())
                {
                    xmlSerializer.Serialize(xmlStream, convertFromObject);
                    xmlStream.Position = 0;
                    xmlDoc.Load(xmlStream);
                    return xmlDoc.InnerXml;
                }

            }
            catch (System.Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// Remove invalid characters from XML string
        /// </summary>
        /// <param name="xml">XMl string to sanitize</param>
        /// <returns>Sanitized XML string</returns>
        public static string SanitizeXmlString(string xml)
        {
            if (string.IsNullOrWhiteSpace(xml))
            {
                return "";
            }

            StringBuilder buffer = new StringBuilder(xml.Length);
            foreach (char c in xml)
            {

                if (!IsLegalXmlChar(c))
                {
                    continue;
                }

                buffer.Append(c);
            }
            return buffer.ToString();
        }

        /// <summary>
        /// Check whether a character is valid for XML 1.0
        /// </summary>
        /// <param name="character">Character to validate</param>
        /// <returns>True of False indicating validity of character</returns>
        public static bool IsLegalXmlChar(int character)
        {
            return
            (
            character == 0x9 /* == '\t' == 9 */ ||
            character == 0xA /* == '\n' == 10 */ ||
            character == 0xD /* == '\r' == 13 */ ||
            (character >= 0x20 && character <= 0xD7FF) ||
            (character >= 0xE000 && character <= 0xFFFD) ||
            (character >= 0x10000 && character <= 0x10FFFF)
            );
        }
    }
}
