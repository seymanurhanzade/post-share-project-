using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using react.server.ApplicationContext;

namespace react.server.DataManagement.server.entitys
{   //TAKİP EDİLEN 
    public class FollowedTable
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(UserId))]
        public string? UserId { get; set; }
       
        
        [ForeignKey(nameof(FollowedUserId))]
        public string? FollowedUserId { get; set; }=null; 
        
        [NotMapped]
        public Users? User { get; set; }
    }
}