using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace react.server.Models
{
    public class CommuntModel
    {
        public string Communt { get; set; }
        //yorum yapan
        public string UserId { get; set; }
        //yorum yapılan
        public int PostId { get; set; }
    }
    
    public class GetPostModel{
        public int Id { get; set; }
        public string Share { get; set; }
        public string Time { get; set; }
        public int Count { get; set; }
        public string FullName { get; set; }
        public string AtUserName { get; set; }
        public string Images { get; set; }
        
        public List<GetCommuntModel> GetPostModelList { get; set; }
    }
    
    public class GetCommuntModel
    {
        public int Id { get; set; }
        public string Communt { get; set; }
        public int PostId { get; set; }
        public string UserId { get; set; }
        public string FullName { get; set; }
        public string AtUserName { get; set; }
        public string Images { get; set; }
        public string Time { get; set; }
    }

}