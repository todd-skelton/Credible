using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace WebApiSample.Controllers
{
    [Route("api/user")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly UserIdentity _user;

        public UserController(UserIdentity user)
        {
            _user = user ?? throw new ArgumentNullException(nameof(user));
        }

        // GET api/admin
        [HttpGet("")]
        public ActionResult<UserIdentity> Get()
        {
            return _user;
        }
    }
}
