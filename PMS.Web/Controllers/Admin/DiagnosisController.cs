using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PMS.Web.Models;
using PMS.Web.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMS.Web.Controllers.Admin
{
    [Authorize(Roles = UserRolesModels.Admin)]
    [Route("api/[controller]")]
    [ApiController]
    public class DiagnosisController : Controller
    {

        private readonly IAdminService _adminService;
        public DiagnosisController(IAdminService adminService)
        {
            _adminService = adminService;
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IEnumerable<DiagnosisModel>> FetchDiagnoses()
        {
            var res = await _adminService.AllDiagnosis();
            return res;
        }
        [HttpPost]
        public async Task<IActionResult> AddDiagnosis(DiagnosisModel newDiagnosis)
        {
            if (newDiagnosis != null)
            {
                newDiagnosis.DiagnosisId = Guid.NewGuid();
                var res = await _adminService.AddDiagnosis(newDiagnosis);
                if (res > 0)
                    return Ok(new { status = StatusCodes.Status200OK, success = true, data = "Diagnosis Added successfully" });
                else
                    return Ok(new { status = StatusCodes.Status400BadRequest, success = false, data = "Error while adding the Diagnosis" });
            }
            else
                return BadRequest(new { status = StatusCodes.Status400BadRequest, success = false, data = "diagnosis data is null" });
        }
        [HttpPut]
        public async Task<IActionResult> EditDiagnosis(DiagnosisModel newDiagnosis)
        {
            if (newDiagnosis != null)
            {
                var res = await _adminService.EditDiagnosis(newDiagnosis);
                if (res > 0)
                    return Ok(new { status = StatusCodes.Status200OK, success = true, data = "Diagnosis Edited successfully" });
                else
                    return Ok(new { status = StatusCodes.Status400BadRequest, success = false, data = "Error while updating the Diagnosis" });
            }
            else
                return BadRequest(new { status = StatusCodes.Status400BadRequest, success = false, data = "diagnosis data is null" });
        }

        [HttpDelete]
        [Route("{diagnosisId}")]
        public async Task<IActionResult> DeleteDrug(Guid diagnosisId)
        {
            var res = await _adminService.DeleteDiagnosis(diagnosisId);
            if (res > 0)
                return Ok(new { status = StatusCodes.Status200OK, success = true, data = "Diagnosis Deleted successfully" });
            else
                    return Ok(new { status = StatusCodes.Status400BadRequest, success = false, data = "Diagnosis not deleted" });
        }
    }
}
