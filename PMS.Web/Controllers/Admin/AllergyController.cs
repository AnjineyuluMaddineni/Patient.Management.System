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
    public class AllergyController : ControllerBase
    {
        private readonly IAdminService _adminService;
        public AllergyController( IAdminService adminService)
        {
            _adminService = adminService;
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IEnumerable<AllergyModel>> FetchAllergies()
        {
            return await _adminService.AllAllergies();             
        }
        [HttpPost]
        public async Task<IActionResult> AddAllergy(AllergyModel newAllergy)
        {
            var res = await _adminService.AddAllergy(newAllergy);
            if (res > 0)
                return Ok(new { status = StatusCodes.Status200OK, success = true, data = "Allergy added successfully" });
            else
                return BadRequest(new { status = StatusCodes.Status400BadRequest, success = false, data = "Error while adding allergy" });
        }

        [HttpPut]
        public async Task<IActionResult> EditAllergy(AllergyModel newAllergy)
        {
            var res = await _adminService.EditAllergy(newAllergy);
            if (res > 0)
                return Ok(new { status = StatusCodes.Status200OK, success = true, data = "Allergy Updated successfully" });
            else
                return BadRequest(new { status = StatusCodes.Status400BadRequest, success = false, data = "Error while updating allergy" });
        }

        [HttpDelete]
        [Route("{aId}")]
        public async Task<IActionResult> DeleteAllergy(Guid aId)
        {
            var res = await _adminService.DeleteAllergy(aId);
            if (res > 0)
                return Ok(new { status = StatusCodes.Status200OK, success = true, data = "Allergy Deleted successfully" });
            else
                return BadRequest(new { status = StatusCodes.Status400BadRequest, success = false, data = "Allergy not deleted" });
        }

    }
}
