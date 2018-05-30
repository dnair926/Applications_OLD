namespace Applications.Core.Attributes
{
    using System;

    /// <summary>
    /// Profile descriptor property identifier attribute
    /// </summary>
    public class ProfileDescriptorAttribute : Attribute
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="identifierPropertyName">Set <see cref="IdentifierPropertyName"/></param>
        /// <param name="profilePropertyName">Set <see cref="ProfilePropertyName"/></param>
        public ProfileDescriptorAttribute(string identifierPropertyName, string profilePropertyName)
        {
            ProfilePropertyName = profilePropertyName;
            IdentifierPropertyName = identifierPropertyName;
        }

        /// <summary>
        /// Name of property that has the profile identifier value
        /// </summary>
        public string IdentifierPropertyName { get; }

        /// <summary>
        /// Name of property that has the profile description value
        /// </summary>
        public string ProfilePropertyName { get; }
    }
}