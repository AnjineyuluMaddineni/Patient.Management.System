using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PMS.Web.Configuration;
using PMS.Web.Models;
using PMS.Web.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMS.Web.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IPatientService _patientService;

        private readonly DateTime today = DateTime.Today;

        public PatientController(IPatientService patientService, IUserService userService)
        {
            _userService = userService;
            _patientService = patientService;
        }
        [Authorize(Roles = UserRolesModels.Patient)]
        [HttpGet("{email}")]
        public async Task<ActionResult<PatientModel>> PatientDetails(string email)
        {
            string appUserId = await _userService.GetAppUserId(email);
            if (appUserId != null)
            {
                var result = await _patientService.GetPatientDetails(appUserId);
                if (result != null)
                    return Ok(result);
                else
                    return Ok(new { status = StatusCodes.Status400BadRequest, success = false, data = "Patient details not found" });
            }
            else
                return BadRequest(new { status = StatusCodes.Status400BadRequest, success = false, data = "App user id is null" });
        }
        [Authorize(Roles = UserRolesModels.Patient)]
        [HttpGet]
        [Route("PatientDetails/{email}/{role}")]
        public async Task<ActionResult<PatientModel>> PatientDetailsById(string email, string role)
        {
            Guid pId = await _userService.GetUserId(email, role);
            var result = await _patientService.GetPatientDetailsById(pId);
            if (result != null)
                return Ok(result);
            else
                return Ok(new { status = StatusCodes.Status400BadRequest, success = false, data = "Patient details not found" });
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(UserModel user)
        {
            if (user != null)
            {
                user.Id = Guid.NewGuid().ToString();
                user.UserName = user.FirstName + user.LastName;
                user.Status = 1;
                user.RegistrationDate = today;
                int result = await _patientService.Register(user);
                if (result == 1)
                    return Ok(new { status = StatusCodes.Status200OK, success = true, data = "Registered successfully" });
                else
                    return Ok(new { status = StatusCodes.Status400BadRequest, success = false, data = "Not able to register user" });
            }
            else
                return BadRequest(new { status = StatusCodes.Status400BadRequest, success = false, data = "User data is null" });
        }
        [AuthorizeRoles(UserRolesModels.Nurse, UserRolesModels.Physician, UserRolesModels.Patient)]
        [HttpPost("AddPatientDetails/{email}/{role}")]
        public async Task<IActionResult> AddDemographic([FromBody] PatientModel patient, string email, string role)
        {
            if (patient != null)
            {
                Guid pId = await _userService.GetUserId(email, role);
                patient.PId = pId;
                int result = await _patientService.AddDemographic(patient);
                if (result<0)
                    return Ok(new { status = StatusCodes.Status200OK, success = true, data = "Added demographic details successfully" });
                else
                    return Ok(new { status = StatusCodes.Status400BadRequest, success = false, data = "Not able to add" });

            }
            else
                return BadRequest(new { status = StatusCodes.Status400BadRequest, success = false, data = "Patient data is null" });

        }

    }
}
