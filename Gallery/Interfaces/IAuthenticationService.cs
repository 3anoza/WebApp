using System.Security.Claims;
using Microsoft.Owin;

namespace Gallery.Interfaces
{
    public interface IAuthenticationService
    {
        void AuthorizeContext(IOwinContext ctx, ClaimsIdentity claim);
        ClaimsIdentity CreateClaim(string PersonId);
    }
}