using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace react.server.Models
{
    public class LikeModel
    {
        public int PostId { get; set; }
        public string UserId { get; set; }
    }

    public class LikeModelList{
        public int PostId { get; set;}
        public string UserId { get; set; }
        public string AtUserName { get; set; }
        public string fullName { get; set; }
        public string Image { get; set; }
        public string share { get; set; }
        public string Time { get; set; }
        public int Count { get; set; } 
        public int CommentCount { get; set; }
        public bool IsLiked { get; set; }
    }
     public class LikeModelPostIdList{
        public int PostId { get; set;}
    }
    public class LikeModel2
    {
        public string UserId { get; set; }
        public List<LikeModelList> LikeModelList { get; set; }

    }
}