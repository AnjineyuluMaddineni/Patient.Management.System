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
    public class PatientAllergyController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IPatientAllergyService _patientAllergyService;

        public PatientAllergyController(IPatientAllergyService patientAllergyService, IUserService userService)
        {
            _userService = userService;
            _patientAllergyService = patientAllergyService;
        }
        [HttpGet("{email}/{role}")]
        public async Task<IActionResult> GetPatientAllergies(string email, string role)
        {
            Guid pId = await _userService.GetUserId(email, role);
            var result = await _patientAllergyService.GetPatientAllergies(pId);
            if (result != null)
                return Ok(result);
            else
                return Ok(new { status = StatusCodes.Status400BadRequest, success = false, data = "Patient Allergy details not found" });

        }
        [AuthorizeRoles(UserRolesModels.Nurse, UserRolesModels.Physician, UserRolesModels.Patient)]
        [HttpPost("{email}/{role}")]
        public async Task<IActionResult> AddPatientAllergy(PatientAllergyModel allergyModel,string email, string role)
        {            
            if (allergyModel != null)
            {
                Guid pId = await _userService.GetUserId(email, role);
                allergyModel.PId = pId;
                allergyModel.AllergyInfoId = Guid.NewGuid();
                int result = await _patientAllergyService.AddPatientAllergy(allergyModel);
                if (result == 1)
                    return Ok(new { status = StatusCodes.Status200OK, success = true, data = "Added Emergency Info Successfully" });
                else if (result < 0)
                    return Ok(new { status = StatusCodes.Status208AlreadyReported, success = false, data = "Already Added" });
                else
                    return Ok(new { status = StatusCodes.Status400BadRequest, success = false, data = "Not able to add" });

            }
            else
                return BadRequest(new { status = StatusCodes.Status400BadRequest, success = false, data = "Patient data is null" });

        }
        [Authorize(Roles = UserRolesModels.Patient)]
        [HttpPut("{email}/{role}")]
        public async Task<IActionResult> UpdatePatientAllergy(PatientAllergyModel allergyModel, string email, string role)
        {
            if (allergyModel != null)
            {
                Guid pId = await _userService.GetUserId(email, role);
                allergyModel.PId = pId;
                int result = await _patientAllergyService.UpdatePatientAllergy(allergyModel);
                if (result == 1)
                    return Ok(new { status = StatusCodes.Status200OK, success = true, data = "Added Emergency Info Successfully" });
                else
                    return Ok(new { status = StatusCodes.Status400BadRequest, success = false, data = "Not able to add" });

            }
            else
                return BadRequest(new { status = StatusCodes.Status400BadRequest, success = false, data = "Patient data is null" });

        }
        [AuthorizeRoles(UserRolesModels.Nurse, UserRolesModels.Physician,UserRolesModels.Patient)]
        [HttpDelete("{allergyInfoId}")]
        public async Task<IActionResult> DeletePatientAllergy(Guid allergyInfoId)
        {
            int result = await _patientAllergyService.DeletePatientAllergy(allergyInfoId);
            if (result == 1)
                return Ok(new { status = StatusCodes.Status200OK, success = true, data = "Added Patient Allergy Successfully" });
            else
                return Ok(new { status = StatusCodes.Status404NotFound, success = false, data = "Patient Allergy Not Found " });

        }

    }
}
