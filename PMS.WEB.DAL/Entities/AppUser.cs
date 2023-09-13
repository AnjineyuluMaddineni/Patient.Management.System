using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Web.Entities
{
    public class AppUser : IdentityUser
    {
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public DateTime? BirthDate { get; set; }
        public int Status { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public int UserTypeId { get; set; }
        public string CountryCode { get; set; }
        public string Token { get; set; }
        public ICollection<HospitalUser> HospitalUsers { get; set; }
        public ICollection<Patient> Patients { get; set; }


        [NotMapped]
        public string Role { get; set; }
    }
}
