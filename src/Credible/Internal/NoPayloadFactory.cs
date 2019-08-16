using System;
using System.Collections.Generic;

namespace Credible.Internal
{
    internal class NoPayloadFactory<TIdentity> : IPayloadFactory<TIdentity>
    {
        public IDictionary<string, object> Create(TIdentity identity)
        {
            throw new NotImplementedException();
        }
    }
}
