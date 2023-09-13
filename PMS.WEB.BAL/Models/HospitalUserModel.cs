using System;
using System.ComponentModel.DataAnnotations;

namespace PMS.Web.Models
{
    public class HospitalUserModel
    {
        public Guid EmployeeId { get; set; }
        public Guid AppUserId { get; set; }
        public UserModel AppUser { get; set; }
    }
}
