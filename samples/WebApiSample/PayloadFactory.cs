using Credible;
using System.Collections.Generic;

namespace WebApiSample
{
    public class PayloadFactory : IPayloadFactory<UserIdentity>
    {
        public IDictionary<string, object> Create(UserIdentity identity)
        {
            return new Dictionary<string, object>
            {
                { "userId", identity.UserId },
                { "username", identity.Username },
                { "permissions", identity.Permissions }
            };
        }
    }
}
