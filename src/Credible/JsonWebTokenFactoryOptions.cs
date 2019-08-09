using Microsoft.IdentityModel.Tokens;
using System;

namespace Credible
{
    public class JsonWebTokenFactoryOptions
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public SigningCredentials SigningCredentials { get; set; }
        public TimeSpan Expiration { get; set; }
    }
}
