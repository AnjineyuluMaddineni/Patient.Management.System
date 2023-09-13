using PMS.Web.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Web.Models
{
    public class RefreshTokenModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Token { get; set; }
        public string JwtId { get; set; }
        public bool IsUsed { get; set; }
        public bool IsRevorked { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime ExpiryDate { get; set; }

        [ForeignKey(nameof(UserId))]
        public AppUser User { get; set; }
    }
}
