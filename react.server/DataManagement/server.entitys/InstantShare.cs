using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using react.server.ApplicationContext;

namespace server.entitys
{
    public class InstantShare
    {
        public int Id { get; set; }
        public string Share { get; set; }
        public DateTime Time { get; set; }
        public string UserId { get; set; }
        public Users User { get; set; }
        public int Count { get; set; }
        public int CommuntCount { get; set; } = 0;
    }
    public class DeletePost
    {

        public int Id { get; set; }
        public string Share { get; set; }
        public DateTime Time { get; set; }
        public string UserId { get; set; }
        public Users User { get; set; }

    }

}