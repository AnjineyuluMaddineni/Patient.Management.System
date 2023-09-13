using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Web.Models
{
    public static class UserRolesModels
    {
        public const string Admin = "Admin";
        public const string Patient = "Patient";
        public const string Physician = "Physician";
        public const string Nurse = "Nurse";
    }
    public static class ScheduleStatusModels
    {
        public const int Requested = 1;
        public const int Approved = 2;
        public const int Declined = 3;
    }
    public static class UserStatusModels
    {
        public const int Active = 1;
        public const int InActive = 2;
        public const int Blocked = 3;
    }
}
