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
    public class MedicationController : ControllerBase
    {
        private readonly IAdminService _adminService;
        public MedicationController(IAdminService adminService)
        {
            _adminService = adminService;
        }
        [AllowAnonymous]
        [HttpGet]        
        public async Task<IEnumerable<MedicationModel>> FetchMedication()
        {
            var res = await _adminService.AllMedication();
            return res;
        }
        [HttpPost]
        //[Route("Medication/AddMedication")]
        public async Task<IActionResult> AddMedication(MedicationModel newMedication)
        {
            var res = await _adminService.AddMedication(newMedication);
            if (res > 0)
                return Ok(new { status = StatusCodes.Status200OK, success = true, data = "Medication Added successfully" });
            else
                return BadRequest(new { status = StatusCodes.Status400BadRequest, success = false, data = "Error while adding the medication" });
        }
        [HttpPut]
        //[Route("Medication/EditMedication")]
        public async Task<IActionResult> EditMedication(MedicationModel newMedication)
        {
            var res = await _adminService.EditMedication(newMedication);
            if (res > 0)
                return Ok(new { status = StatusCodes.Status200OK, success = true, data = "Medication Edited successfully" });
            else
                return BadRequest(new { status = StatusCodes.Status400BadRequest, success = false, data = "Error while updating the medication" });
        }

        [HttpDelete]
        [Route("{mId}")]
        public async Task<IActionResult> DeleteDrug(Guid mId)
        {
            var res = await _adminService.DeleteMedication(mId);
            if (res > 0)
                return Ok(new { status = StatusCodes.Status200OK, success = true, data = "Medication Deleted successfully" });
            else
                return BadRequest(new { status = StatusCodes.Status400BadRequest, success = false, data = "Medication not deleted" });
        }
    }
}
