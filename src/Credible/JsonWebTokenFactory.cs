using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Credible
{
    /// <summary>
    /// Used for authenticating users using JWT Tokens
    /// </summary>
    public class JsonWebTokenFactory<TIdentity>
    {
        /// <summary>
        /// Factory used to create claims based on the identity model.
        /// </summary>
        protected readonly IPayloadFactory<TIdentity> _payloadFactory;

        /// <summary>
        /// Options used to configure the Json Web Token Authentication
        /// </summary>
        protected JsonWebTokenFactoryOptions _options;

        /// <summary>
        /// Constructs a new <see cref="JsonWebTokenFactory{TIdentity}"/> with options and claims factory.
        /// </summary>
        /// <param name="options"></param>
        /// <param name="payloadFactory"></param>
        public JsonWebTokenFactory(IOptions<JsonWebTokenFactoryOptions> options, IPayloadFactory<TIdentity> payloadFactory)
        {
            _options = options.Value ?? throw new ArgumentNullException(nameof(options));
            _payloadFactory = payloadFactory;
        }

        /// <summary>
        /// Generates a token
        /// </summary>
        /// <returns>A signed JSON Web Token that can be validated on requests</returns>
        public virtual JsonWebToken Create(TIdentity identity)
        {
            var dict = _payloadFactory.Create(identity);

            var now = DateTimeOffset.Now;

            var payload = new JwtPayload(
                _options.Issuer,
                _options.Audience,
                new List<Claim>(),
                now.UtcDateTime,
                now.Add(_options.Expiration).UtcDateTime);

            foreach (var item in dict)
            {
                payload.Add(item.Key, item.Value);
            }

            var token = new JwtSecurityToken(new JwtHeader(_options.SigningCredentials), payload);

            var handler = new JwtSecurityTokenHandler();
            handler.InboundClaimTypeMap.Clear();

            return new JsonWebToken(token, handler);
        }
    }
}
