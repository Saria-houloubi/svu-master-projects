using System.Linq;
using System.Security.Claims;

namespace SVU.Web.UI.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="ClaimsPrincipal"/>
    /// </summary>

    public static class UserClaimsExtension
    {
        /// <summary>
        /// Gets the claim value if found
        /// </summary>
        /// <param name="modelState"></param>
        /// <returns></returns>
        public static string GetClaimValue(this ClaimsPrincipal principal, string type)
        {
            var claim = principal.Claims.SingleOrDefault(item => item.Type == type);

            return claim != null ? claim.Value : "";
        }
    }
}
