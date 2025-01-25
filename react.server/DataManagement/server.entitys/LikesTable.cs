using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using react.server.ApplicationContext;
using server.entitys;

namespace react.server.DataManagement.server.entitys
{
    public class LikesTable
    {
        public int Id { get; set; }
        public int  PostId { get; set; }
        public InstantShare PostTable { get; set; }
        public string UserId { get; set; }
        public Users User { get; set; }
    }
}