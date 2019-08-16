using System.Security.Claims;

namespace Credible.Internal
{
    internal class NoIdentityFactory<TIdentity> : IIdentityFactory<TIdentity>
    {
        public TIdentity Create(ClaimsPrincipal principal)
        {
            throw new System.NotImplementedException();
        }
    }
}
