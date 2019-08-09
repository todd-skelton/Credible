using System;
using System.IdentityModel.Tokens.Jwt;

namespace Credible
{
    /// <summary>
    /// Java web token object that can be serialized to the client.
    /// </summary>
    public class JsonWebToken
    {
        /// <summary>
        /// Takes a <see cref="JwtSecurityToken"/> and extracts the token string and expiration.
        /// </summary>
        /// <param name="token"></param>
        /// <param name="handler"></param>
        public JsonWebToken(JwtSecurityToken token, JwtSecurityTokenHandler handler = null)
        {
            Value = (handler ?? new JwtSecurityTokenHandler()).WriteToken(token);
            Expiration = new DateTimeOffset(token.ValidTo).ToUnixTimeMilliseconds();
        }

        /// <summary>
        /// Token value written out as a string.
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// Token expiration written as Unix time in milliseconds.
        /// </summary>
        public long Expiration { get; }
    }
}
