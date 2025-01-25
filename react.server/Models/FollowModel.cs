using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace react.server.Models
{
    public class FollowModel
    {
        public string UserId { get; set; }
        public string FollowerUserId { get; set; }
    }
    public class FollowUserModel
    {
        public string UserId { get; set; }
        public string FullName { get; set; }
        public string AtUserName { get; set; }
        public string Images { get; set; }
    }
    public class FollowingList
    {
        public List<FollowUserModel> TakipEdilenlerList { get; set; }
        public List<FollowUserModel> TakipcilerList { get; set; }
    }

}