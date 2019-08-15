using System.Collections.Generic;

namespace Credible
{
    /// <summary>
    /// Factory used to create a payload from an Identity model.
    /// </summary>
    /// <typeparam name="TIdentity">The type representing the identity.</typeparam>
    public interface IPayloadFactory<in TIdentity>
    {
        /// <summary>
        /// Creates a payload based on the identity model to be added to the token.
        /// </summary>
        /// <param name="identity">The identity model.</param>
        /// <returns>The payload.</returns>
        IDictionary<string, object> Create(TIdentity identity);
    }
}
