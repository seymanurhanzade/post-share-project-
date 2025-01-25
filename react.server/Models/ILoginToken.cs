using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace react.server.Models
{
    public class ILoginToken
    {
        public string Token { get; set; }
        public string UserId { get; set; }
        public string AtUserName { get; set; }
        public string FullName { get; set; }
        public string UserImages { get; set; }
    }
    public class LoginUserId{
        public string UserId { get; set; }
    }
}