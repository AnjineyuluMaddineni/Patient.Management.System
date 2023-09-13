using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Web.Models
{
    public class ReplyMessageModel
    {
        public Guid ReplyMessageId { get; set; }
        public string FromAppUserId { get; set; }
        public string ToAppUserId { get; set; }
        public string MessageBody { get; set; }
        public Guid MessageId { get; set; }
        public MessageModel Message { get; set; }
        public string FromName { get; set; }
        public string ToName { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
