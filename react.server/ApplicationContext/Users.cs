using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace react.server.ApplicationContext
{
    public class Users:IdentityUser
    {
        [Required]
        public string AtUserName { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Password { get; set; }
        public string Image { get; set; }
        public int TotalFollowed { get; set; } = 0;
        public int TotalFollower { get; set; } = 0;
    }
}