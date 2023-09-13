using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Web.Entities
{
    public class ReplyMessage
    {
        [Key]
        public Guid ReplyMessageId { get; set; }
        public string FromAppUserId { get; set; }
        public string ToAppUserId { get; set; }
        public string MessageBody { get; set; }
        [ForeignKey("Message")]
        public Guid MessageId { get; set; }
        public Message Message { get; set; }
        [NotMapped]
        public string FromName { get; set; }
        [NotMapped]
        public string ToName { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
