using Microsoft.Extensions.Options;
using System;
using System.IdentityModel.Tokens.Jwt;

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
        protected readonly IClaimsFactory<TIdentity> _claimsFactory;

        /// <summary>
        /// Options used to configure the Json Web Token Authentication
        /// </summary>
        protected JsonWebTokenFactoryOptions _options;

        /// <summary>
        /// Constructs a new <see cref="JsonWebTokenFactory{TIdentity}"/> with options and claims factory.
        /// </summary>
        /// <param name="options"></param>
        /// <param name="claimsFactory"></param>
        public JsonWebTokenFactory(IOptions<JsonWebTokenFactoryOptions> options, IClaimsFactory<TIdentity> claimsFactory)
        {
            _options = options.Value ?? throw new ArgumentNullException(nameof(options));
            _claimsFactory = claimsFactory;
        }

        /// <summary>
        /// Generates a token
        /// </summary>
        /// <returns>A signed JSON Web Token that can be validated on requests</returns>
        public virtual JsonWebToken Create(TIdentity identity)
        {
            var claims = _claimsFactory.Create(identity);

            var now = DateTimeOffset.Now;

            var token = new JwtSecurityToken
            (
                 issuer: _options.Issuer,
                 audience: _options.Audience,
                 claims: claims,
                 notBefore: now.UtcDateTime,
                 expires: now.Add(_options.Expiration).UtcDateTime,
                 signingCredentials: _options.SigningCredentials
            );

            var handler = new JwtSecurityTokenHandler();
            handler.InboundClaimTypeMap.Clear();

            return new JsonWebToken(token, handler);
        }
    }
}
