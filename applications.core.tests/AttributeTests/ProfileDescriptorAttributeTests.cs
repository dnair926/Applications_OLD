namespace Applications.Core.Tests.Attributes
{
    using Applications.Core.Attributes;
    using Xunit;

    public class ProfileDescriptorAttributeTests
    {
        [Fact]
        public void Values_Set_In_Constructor_Should_Be_Set_Correctly()
        {
            var identifier = "ProfileIdentifier";
            var property = "ProfileProperty";
            var profileDescriptorAttribute = new ProfileDescriptorAttribute(
                identifierPropertyName: identifier,
                profilePropertyName: property);

            var result = profileDescriptorAttribute.IdentifierPropertyName == identifier &&
                profileDescriptorAttribute.ProfilePropertyName == property;

            Assert.True(result);

            identifier = "";
            property = "";
            profileDescriptorAttribute = new ProfileDescriptorAttribute(
            identifierPropertyName: identifier,
            profilePropertyName: property);

            result = profileDescriptorAttribute.IdentifierPropertyName == identifier &&
            profileDescriptorAttribute.ProfilePropertyName == property;

            Assert.True(result);
        }
    }
}