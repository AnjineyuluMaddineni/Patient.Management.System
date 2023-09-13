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
    public class VisitController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IVisitService _visitService;

        public VisitController(IVisitService visitService, IUserService userService)
        {
            _userService = userService;
            _visitService = visitService;
        }

        [HttpGet]
        [Route("{email}/{role}")]
        public async Task<IEnumerable<PatientVisitModel>> GetPatientVisits(string email, string role)
        {
            Guid pId;
            Guid employeeId;
            IEnumerable<PatientVisitModel> res;
            if (role == UserRolesModels.Patient)
            {
                pId = await _userService.GetUserId(email, role);
                res = await _visitService.GetAllVisits(pId);
                return res;
            }
            else
            {
                employeeId = await _userService.GetUserId(email, role);
                res = await _visitService.GetAllVisits(role, employeeId);
                return res;
            }
            
        }
        [HttpPost]
        public async Task<IActionResult> AddPatientVisit(PatientVisitModel visitModel)
        {
            if (visitModel != null)
            {
                visitModel.VisitId = Guid.NewGuid();
                visitModel.VisitDate = DateTime.Now;
                foreach(var diagnosis in visitModel.PatientDiagnoses)
                {
                    diagnosis.Id = Guid.NewGuid();
                    diagnosis.VisitId = visitModel.VisitId;
                }
                foreach(var procedure in visitModel.PatientProcedures)
                {
                    procedure.Id = Guid.NewGuid();
                    procedure.VisitId = visitModel.VisitId;
                }
                foreach(var medication in visitModel.PatientMedications)
                {
                    medication.Id = Guid.NewGuid();
                    medication.VisitId = visitModel.VisitId;
                }
                int result = await _visitService.AddPatientVisit(visitModel);
                if (result == 1)
                    return Ok(new { status = StatusCodes.Status200OK, success = true, data = "Added Visit Successfully" });
                else
                    return Ok(new { status = StatusCodes.Status400BadRequest, success = false, data = "Not able to add" });

            }
            else
                return BadRequest(new { status = StatusCodes.Status400BadRequest, success = false, data = "Patient Visit data is null" });

        }
        [HttpPut]
        public async Task<IActionResult> UpdatePatientVisit(PatientVisitModel visitModel)
        {
            if (visitModel != null)
            {
                int result = await _visitService.UpdatePatientVisit(visitModel);
                if (result == 1)
                    return Ok(new { status = StatusCodes.Status200OK, success = true, data = "Updated Visit Successfully" });
                else
                    return Ok(new { status = StatusCodes.Status400BadRequest, success = false, data = "Not able to Update" });

            }
            else
                return BadRequest(new { status = StatusCodes.Status400BadRequest, success = false, data = "Patient Visit data is null" });
        }


    }
}