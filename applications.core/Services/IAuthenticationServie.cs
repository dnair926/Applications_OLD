namespace Applications.Core
{
    /// <summary>
    /// Authentication service interface
    /// </summary>
    public interface IAuthenticationService
    {
        /// <summary>
        /// Get current user id
        /// </summary>
        /// <returns>Currently logged in user id</returns>
        string GetCurrentUserId();
    }
}