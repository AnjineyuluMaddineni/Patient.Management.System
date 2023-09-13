using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace PMS.Web.Models
{
    public class UserModel:IdentityUser
    {
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public DateTime? BirthDate { get; set; }
        public int Status { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public string EmployeeId { get; set; }
        public int UserTypeId { get; set; }
        public string CountryCode { get; set; }

        public string Role { get; set; }
    }
}
