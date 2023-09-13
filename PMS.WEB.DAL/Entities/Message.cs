using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Web.Entities
{
    public class Message
    {
        [Key]
        public Guid MessageId { get; set; }
        public string FromAppUserId { get; set; }
        public string ToAppUserId { get; set; }
        public string MessageBody { get; set; }
        public ICollection<ReplyMessage> ReplyMessage { get; set; }
        [NotMapped]
        public string FromName { get; set; }
        [NotMapped]
        public string ToName { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsUrgent { get; set; }
        [NotMapped]
        public string CurrentAppUserId { get; set; }
    }
}
