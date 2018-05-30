namespace Applications.Core
{
    using Microsoft.AspNetCore.Http;
    using System.Security.Claims;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Windows authentication service implementation of <see cref="IAuthenticationService"/>
    /// </summary>
    public class WindowsAuthenticationService : IAuthenticationService
    {
        readonly IHttpContextAccessor httpContextAccessor;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        public WindowsAuthenticationService(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Get currently logged in user id from HttpContext
        /// </summary>
        /// <returns>User id as string, excluding domain name, if any.</returns>
        public string GetCurrentUserId()
        {
            var currentUserId = httpContextAccessor.HttpContext.User.FindFirst(c => c.Type == ClaimTypes.Name)?.Value;
            if (string.IsNullOrWhiteSpace(currentUserId))
            {
                return string.Empty;
            }

            return Regex.Replace(currentUserId, ".*\\\\(.*)", "$1", RegexOptions.None);
        }
    }
}