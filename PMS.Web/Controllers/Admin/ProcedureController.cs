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
    public class ProcedureController : ControllerBase
    {

        private readonly IAdminService _adminService;
        public ProcedureController(IAdminService adminService)
        {
            _adminService = adminService;
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IEnumerable<ProcedureModel>> FetchProcedure()
        {
            var res = await _adminService.AllProcedure();
            return res;
        }
        [HttpPost]
        public async Task<IActionResult> AddProcedure(ProcedureModel newMedication)
        {
            if (newMedication != null)
            {
                newMedication.ProcedureId = Guid.NewGuid();

                var res = await _adminService.AddProcedure(newMedication);
                if (res > 0)
                    return Ok(new { status = StatusCodes.Status200OK, success = true, data = "Procedure Added successfully" });
                else
                    return Ok(new { status = StatusCodes.Status400BadRequest, success = false, data = "Error while adding the procedure" });
            }
            else
                return BadRequest(new { status = StatusCodes.Status400BadRequest, success = false, data = "Error while adding the procedure" });

        }
        [HttpPut]
        public async Task<IActionResult> EditProcedure(ProcedureModel newProcedure)
        {
            var res = await _adminService.EditProcedure(newProcedure);
            if (res > 0)
                return Ok(new { status = StatusCodes.Status200OK, success = true, data = "Procedure updated successfully" });
            else
                return BadRequest(new { status = StatusCodes.Status400BadRequest, success = false, data = "Error while updating the procedure" });
        }

        [HttpDelete]
        [Route("{procedureId}")]
        public async Task<IActionResult> DeleteDrug(string procedureId)
        {
            var id = Guid.Parse(procedureId);
            var res = await _adminService.DeleteProcedure(id);
            if (res > 0)
                return Ok(new { status = StatusCodes.Status200OK, success = true, data = "Procedure Deleted successfully" });
            else
                return BadRequest(new { status = StatusCodes.Status400BadRequest, success = false, data = "Procedure not deleted" });
        }
    }
}

