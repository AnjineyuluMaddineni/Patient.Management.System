using PMS.Web.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.WEB.DAL.Entities
{
    public class Notification
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string TranType { get; set; }
        [ForeignKey("AppUser")]
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public bool IsNew { get; set; }
    }
}
