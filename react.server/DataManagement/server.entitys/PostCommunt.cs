using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using react.server.ApplicationContext;
using server.entitys;

namespace react.server.DataManagement.server.entitys
{
    public class PostCommunt
    {
        public int Id { get; set; }
        public string Communt { get; set; }
        public string UserId { get; set; }
        public Users User { get; set; }
        public int PostId { get; set; }
        public InstantShare PostTable { get; set; }
        public DateTime Time { get; set; }
    }
}