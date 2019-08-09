using System.Collections.Generic;
using System.Security.Claims;

namespace Credible
{
    /// <summary>
    /// Factory used to create claims from an Identity model.
    /// </summary>
    /// <typeparam name="TIdentity">The type representing the identity.</typeparam>
    public interface IClaimsFactory<in TIdentity>
    {
        /// <summary>
        /// Creates a list of claims based on the identity model.
        /// </summary>
        /// <param name="identity">The identity model.</param>
        /// <returns>The list of claims.</returns>
        IEnumerable<Claim> Create(TIdentity identity);
    }
}
