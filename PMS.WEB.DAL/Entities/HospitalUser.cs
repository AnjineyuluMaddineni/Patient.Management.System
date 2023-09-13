using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PMS.Web.Entities
{
    public class HospitalUser
    {
        [Key]
        public Guid EmployeeId { get; set; }
        [ForeignKey("AppUser")]
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
