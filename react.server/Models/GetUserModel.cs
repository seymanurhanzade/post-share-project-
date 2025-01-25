using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace react.server.Models
{
    #nullable disable
    public class GetUserModel
    {
        public string UserId { get; set; }
        public string AtUserName { get; set; }
        public string FullName { get; set; }
        public string UserImages { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int TakipEdilenSayisi { get; set; }
        public int TakipciSayisi { get; set; }
    }

    public class ProfileEditModel
    {
        public string UserId { get; set; }
        public string FullName { get; set; }
        public IFormFile Images { get; set; }
       
    }
}