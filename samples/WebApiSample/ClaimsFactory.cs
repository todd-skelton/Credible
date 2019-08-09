using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Credible;

namespace WebApiSample
{
public class ClaimsFactory : IClaimsFactory<UserIdentity>
{
    public IEnumerable<Claim> Create(UserIdentity identity)
    {
        return new List<Claim>(identity.Permissions.Select(e => new Claim("permission", e)))
        {
            new Claim("userId", identity.UserId.ToString()),
            new Claim("username",  identity.Username)
        };
    }
}
}
