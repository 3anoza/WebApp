using System.Security.Claims;
using Gallery.Interfaces;
using Microsoft.Owin;
using Microsoft.Owin.Security;

namespace Gallery.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        public void AuthorizeContext(IOwinContext ctx, ClaimsIdentity claim)
        {
            ctx.Authentication.SignOut();
            ctx.Authentication.SignIn(new AuthenticationProperties
            {
                IsPersistent = true
            }, claim);
        }
        public ClaimsIdentity CreateClaim(string PersonId)
        {
            ClaimsIdentity claim = new ClaimsIdentity(
                "ApplicationCookie",
                ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType
                );
            claim.AddClaim(
                new Claim(
                    ClaimsIdentity.DefaultNameClaimType,
                    PersonId,
                    ClaimValueTypes.String
                    )
                );
            claim.AddClaim(
                new Claim(
                    "http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider",
                    "OwinProvider",
                    ClaimValueTypes.String
                    )
                );
            return claim;
        }
    }
}