using Credible;
using Microsoft.AspNetCore.Mvc;
using System;

namespace WebApiSample.Controllers
{
    [Route("api/token")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly JsonWebTokenFactory<UserIdentity> _tokenFactory;

        public TokenController(JsonWebTokenFactory<UserIdentity> tokenFactory)
        {
            _tokenFactory = tokenFactory ?? throw new ArgumentNullException(nameof(tokenFactory));
        }

        // GET api/token
        [HttpPost("")]
        public ActionResult<JsonWebToken> Get(UserIdentity identity)
        {
            return _tokenFactory.Create(identity);
        }
    }
}
