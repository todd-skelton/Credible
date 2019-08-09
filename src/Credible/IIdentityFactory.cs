using System.Security.Claims;

namespace Credible
{
    /// <summary>
    /// Factory used to create an identity model from claims
    /// </summary>
    /// <typeparam name="TIdentity">The identity model</typeparam>
    public interface IIdentityFactory<out TIdentity>
    {
        TIdentity Create(ClaimsPrincipal principal);
    }
}
