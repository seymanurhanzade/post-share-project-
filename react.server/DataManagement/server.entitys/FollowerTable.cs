using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using react.server.ApplicationContext;

namespace react.server.DataManagement.server.entitys
{   //TAKİPÇİ
    public class FollowerTable
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(UserId))]
        public string? UserId { get; set; }
        

        [ForeignKey(nameof(FollowerUserId))]
        public string? FollowerUserId { get; set; }=null;
        public DateTime Time { get; set; }
        
        [NotMapped]
        public Users? User { get; set; }
    }

    public class DeleteFollowerTable
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(UserId))]
        public string? UserId { get; set; }

        [ForeignKey(nameof(FollowerUserId))]
        public string? FollowerUserId { get; set; }=null;
        public DateTime Time { get; set; }
        
        [NotMapped]
        public Users? User { get; set; }
    }
}