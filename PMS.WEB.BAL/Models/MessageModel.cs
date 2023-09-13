using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PMS.Web.Models
{
    public class MessageModel
    {      
        public Guid MessageId { get; set; }
        public string FromAppUserId { get; set; }
        public string ToAppUserId { get; set; }
        public string MessageBody { get; set; }
        public ICollection<ReplyMessageModel> ReplyMessage { get; set; }
        public string FromName { get; set; }
        public string ToName { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsUrgent { get; set; }
        [NotMapped]
        public string CurrentAppUserId { get; set; }
    }
}
