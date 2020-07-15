using System;
using System.Security.Claims;
using System.Security.Principal;

namespace Gallery.DAL.Models
{
    public static class Identity
    {
        public static string GetUserRole(this IIdentity identity)
        {
            if (identity == null)
            {
                throw new ArgumentNullException(nameof(identity));
            }

            var ci = identity as ClaimsIdentity;
            string role = String.Empty;
            var claim = ci?.FindFirst(ClaimsIdentity.DefaultRoleClaimType);
            if (claim != null)
            {
                role = claim.Value;
            }

            return role;
        }
    }
}