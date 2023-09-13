using PMS.Web.Entities;
using PMS.Web.Models;
using PMS.Web.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Web.Services
{
    public interface IAccountService
    {
        Task<List<UserModel>> GetPatientUsers();
        Task<List<UserModel>> GetHospitalUsers(string appUserId);
        Task<List<UserModel>> GetHospitalUsers();
        Task<List<UserModel>> GetAllPhysicianNurseUsers(string appUserId);
        Task<List<PatientModel>> GetAllPatientUsers();
        Task<List<HospitalUserModel>> GetAllPhysician();
        Task<List<AppUserRoles>> GetAllRoles();
        Task<int> EmployeeRegistration(UserModel user, string roleId);
        Task<AppUser> Login(UserLoginModel user);
        bool ForgotPassword(string Email);
        Task<int> ChangeStatus(string userId, string command);
        Task<bool> ChangePassword(string Email, string oldPassword,string newPassword);
    }
}
