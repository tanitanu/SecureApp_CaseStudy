using System.Security.Claims;
using System.Security.Principal;
/*
* Created By: Kishore
*/
namespace DXC.BlogConnect.WebApp.ServiceExtension
{
    public static class IdentityExtensions
    {
        public static string GetUserName(this IIdentity identity)
        {
            ClaimsIdentity? claimsIdentity = identity as ClaimsIdentity;
            Claim? claim = claimsIdentity?.FindFirst("UserName");

            return claim?.Value ?? string.Empty;
        }

        public static string GetRoles(this IIdentity identity)
        {
            ClaimsIdentity? claimsIdentity = identity as ClaimsIdentity;
            Claim? claim = claimsIdentity?.FindFirst("Roles");

            return claim?.Value ?? string.Empty;
        }

        public static string GetUserId(this IIdentity identity)
        {
            ClaimsIdentity? claimsIdentity = identity as ClaimsIdentity;
            Claim? claim = claimsIdentity?.FindFirst("UserId");

            return claim?.Value ?? string.Empty;
        }
    }
}
