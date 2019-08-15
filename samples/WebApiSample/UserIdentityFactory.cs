using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Credible;

namespace WebApiSample
{
    public class UserIdentityFactory : IIdentityFactory<UserIdentity>
    {
        public UserIdentity Create(ClaimsPrincipal principal)
        {
            return new UserIdentity
            {
                UserId = int.Parse(principal.FindFirst("userId")?.Value ?? "0"),
                Username = principal.FindFirst("username")?.Value ?? "",
                Permissions = principal.FindAll("permissions").Select(e => e.Value)
            };
        }
    }
}
