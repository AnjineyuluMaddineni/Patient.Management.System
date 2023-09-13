using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Web.Entities
{
    public class InboxMessage
    {
        [Key]
        public Guid MId { get; set; }
        public Guid MessageId { get; set; }
        public Guid ReplyMessageId { get; set; }
    }
}
