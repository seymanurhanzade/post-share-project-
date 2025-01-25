using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace react.server.Models
{
    #nullable disable
    public class InstantShareModel
    {
        public string userId { get; set; }
        [JsonPropertyName("share")]
        public string Share { get; set; }
    }

    public class UserProfileModel
    {
        public string userId { get; set; }
        public string UserName { get; set; }
        public string Image { get; set; }
        public string FullName { get; set; }
        public List<UserProfileModelList> UserProfileList { get; set; }
        public List<LikeModelList> LikePosts { get; set; } // Beğenilen postlar
    }
    public class UserProfileModelList{
        public int Id { get; set; }
        public string AtUserName { get; set; }
        public string fullName { get; set; }
        public string Image { get; set; }
        public string share { get; set; }
        public string Time { get; set; }
        public int Count { get; set; } = 0;
        public int CommentCount { get; set; }
        public bool IsLiked { get; set; }

    }
    public class HomePostList{
        public List<InstantShareModelList> HomePostLists { get; set; }
        public List<InstantShareModelList> TakipEdilenlerPostLists { get; set; }
    }

    public class InstantShareModelList
    {
        public int Id { get; set; }
        public string userId { get; set; }
        public string AtUserName { get; set; }
        public string fullName { get; set; }
        public string Image { get; set; }
        public string share { get; set; }
        public string Time { get; set; }
        public int Count { get; set; } 
        public int CommentCount { get; set; }
        public bool IsLiked { get; set; }
    }

    public class  IsLikeStateModel
    {
        public string UserId { get; set; }
        public int PostId { get; set; }
    }

}