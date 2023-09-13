using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PMS.Web.Configuration;
using PMS.Web.Entities;
using PMS.Web.Models;
using PMS.Web.Models.Requests;
using PMS.Web.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMS.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IUserService _userService;
        private readonly DateTime today = DateTime.Today;
        public AccountController(IAccountService accountService,IUserService userService)
        {
            _accountService = accountService;
            _userService = userService;
        }
        [AuthorizeRoles(UserRolesModels.Admin,UserRolesModels.Nurse)]
        [HttpGet]
        public async Task<Object> GetAllPatientUsers()
        {
            var res = await _accountService.GetPatientUsers();
            return res;
        }
        
        [AuthorizeRoles(UserRolesModels.Nurse,UserRolesModels.Physician)]
        [HttpGet("GetAllPatients")]
        public async Task<IEnumerable<PatientModel>> GetAllPatients()
        {
            var res = await _accountService.GetAllPatientUsers();
            return res;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("GetEmployee")]
        public async Task<Object> GetAllHospitalUsers()
        {
            var res = await _accountService.GetHospitalUsers();
            return res;
        }

        [HttpGet]
        [Route("GetEmployee/{email}")]
        public async Task<IEnumerable<UserModel>> GetAllHospitalUsers(string email)
        {
            string appUserId = await _userService.GetAppUserId(email);
            var res = await _accountService.GetHospitalUsers(appUserId);
            return res;
        }

        [HttpGet]
        [Route("GetAllPhysicianNurseUsers/{email}")]
        public async Task<IEnumerable<UserModel>> GetAllPhysicianNurseUsers(string email)
        {
            string appUserId = await _userService.GetAppUserId(email);
            var res = await _accountService.GetAllPhysicianNurseUsers(appUserId);
            return res;
        }

    

        [HttpGet("GetAllPhysicians")]
        public async Task<IEnumerable<HospitalUserModel>> GetAllPhysicians()
        {

            var physiciansList = await _accountService.GetAllPhysician();

            return physiciansList;
        }

        [HttpGet]
        [Route("GetRoles")]
        public async Task<Object> GetRoles()
        {
            var res = await _accountService.GetAllRoles();
            return res;
        }

        [HttpPost("register/{roleId}")]
        public async Task<IActionResult> EmployeeRegistration([FromBody]UserModel user,string roleId)
        {
            if (user != null)
            {
                user.Id = Guid.NewGuid().ToString();
                user.UserName = user.FirstName + user.LastName;
                user.Status = 1;
                user.RegistrationDate = today;
                int result = await _accountService.EmployeeRegistration(user, roleId);
                if (result == 1)
                    return Ok(new { status = StatusCodes.Status200OK, success = true, data = "Registered successfully" });
                else if(result<0)
                    return Ok(new { status = StatusCodes.Status406NotAcceptable, success = false, data = "Duplicate user name" });
                else
                    return Ok(new { status = StatusCodes.Status400BadRequest, success = false, data = "Duplicate emailId" });
            }           
            else
                return BadRequest(new { status = StatusCodes.Status400BadRequest, success = false, data = "Not able to register user" });
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody]UserLoginModel model)
        {            
            if (!string.IsNullOrEmpty(model.Email) && !string.IsNullOrEmpty(model.Password))
            {
                AppUser result = await _accountService.Login(model);

                if (result != null)
                {
                    if(result.Status==UserStatusModels.Active)
                        return Ok(new { status = StatusCodes.Status200OK, success = true, data = result.Token });
                    else
                        return Ok(new { status = StatusCodes.Status403Forbidden, success = false, data = "Blocked/ Deactivated" });
                }
                else
                    return Ok(new { status = StatusCodes.Status203NonAuthoritative, success = false, data = "Invalid credentials" });
               
            }
            else
                return BadRequest(new { status = StatusCodes.Status400BadRequest, success = false, data = "Email & Passowrd are null" });
        }
        [HttpPost("changeStatus/{userId}/{command}")]
        public async Task<IActionResult> ChangeStatus(string userId, string command)
        {
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(command))
            {
                var result = await _accountService.ChangeStatus(userId, command);

                if (result>0)
                    return Ok(new { status = StatusCodes.Status200OK, success = true, data = "Login successfully" });
                else
                    return Ok(new { status = StatusCodes.Status203NonAuthoritative, success = false, data = "Invalid credentials" });
            }
            else
                return BadRequest(new { status = StatusCodes.Status400BadRequest, success = false, data = "Email & Passowrd are null" });
        }
        [HttpPost("{email}")]
        public IActionResult ForgotPassword(string email)
        {
            if (!string.IsNullOrEmpty(email))
            {
                var result = _accountService.ForgotPassword(email);

                if (result)
                    return Ok(new { status = StatusCodes.Status200OK, success = true, data = "Password Sent" });
                else
                    return Ok(new { status = StatusCodes.Status203NonAuthoritative, success = false, data = "email id not found" });
            }
            else
                return BadRequest(new { status = StatusCodes.Status400BadRequest, success = false, data = "Email is null" });
        }
        [HttpPost("changePassword/{email}/{oldPassword}/{newPassword}")]
        public async Task<IActionResult> ChangePassword(string email, string oldPassword,string newPassword)
        {
            
            if (!string.IsNullOrEmpty(email))
            {
                var result = await _accountService.ChangePassword(email, oldPassword,newPassword);

                if (result)
                    return Ok(new { status = StatusCodes.Status200OK, success = true, data = "Password changed successfully" });
                else
                    return BadRequest(new { status = StatusCodes.Status400BadRequest, success = false, data = "please enter correct old password" });
            }
            else
                return BadRequest(new { status = StatusCodes.Status400BadRequest, success = false, data = "Email is null" });
        }

    }
}
