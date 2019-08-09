using System.Collections.Generic;

namespace WebApiSample
{
    public class UserIdentity
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public IEnumerable<string> Permissions { get; set; }
    }
}
