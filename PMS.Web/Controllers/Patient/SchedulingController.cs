using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PMS.Web.Models;
using PMS.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Web.Controllers.Patient
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchedulingController : ControllerBase
    {

        private readonly IUserService _userService;

        private readonly IPatientSchedulingService _patientSchedulingService;

        public SchedulingController(IPatientSchedulingService patientSchedulingService,IUserService userService)
        {
            _userService = userService;
            _patientSchedulingService = patientSchedulingService;
        }

        [HttpGet("AvailableSlots/{employeeId}/{appointmentDate}")]
        public async Task<object> AvailableTimeSlot(Guid employeeId, DateTime appointmentDate)
        {
            var res = await _patientSchedulingService.AvailableTimeSlot(employeeId, appointmentDate);
            if (res != null)
                return Ok(res);
            else
                return BadRequest(new { status = StatusCodes.Status400BadRequest, success = false, data = "Procedure not retrieved" });
        }

        [HttpGet("AvailableSlotsForPhysician/{email}/{role}/{appointmentDate}")]
        public async Task<object> AvailableSlotsForPhysician(string email, string role, DateTime appointmentDate)
        {
            Guid employeeId = await _userService.GetUserId(email, role);
            var res = await _patientSchedulingService.AvailableTimeSlot(employeeId, appointmentDate);
            if (res != null)
                return Ok(res);
            else
                return BadRequest(new { status = StatusCodes.Status400BadRequest, success = false, data = "Procedure not retrieved" });
        }

        [HttpGet("FetchAppointmentsDetail/{email}/{role}")]
        public async Task<object> FetchAppointmentsDetail(string email, string role)
        {
            Guid pId = await _userService.GetUserId(email, role);
            var res = await _patientSchedulingService.GetAppointmentDetails(pId);
            if (res != null)
                return Ok(res);
            else
                return BadRequest(new { status = StatusCodes.Status400BadRequest, success = false, data = "Procedure not retrieved" });
        }

        [HttpGet("FetchAppointmentsDetail/{pId}")]
        public async Task<object> FetchAppointmentsDetail(Guid pId)
        {
            
            var res = await _patientSchedulingService.GetAppointmentDetails(pId);
            if (res != null)
                return Ok(res);
            else
                return BadRequest(new { status = StatusCodes.Status400BadRequest, success = false, data = "Procedure not retrieved" });
        }


        [HttpPost("{email}/{role}")]
        public async Task<IActionResult> AddSchedule(SchedulingModel newSchedule,string email, string role)
        {
            if (newSchedule != null)
            {
                
                if (role ==UserRolesModels.Patient) {
                    Guid pId = await _userService.GetUserId(email, role);
                    newSchedule.PId = pId;
                }
                if(role == UserRolesModels.Physician)
                {
                    Guid employeeID = await _userService.GetUserId(email, role);
                    newSchedule.EmployeeId = employeeID;
                }
                var res = await _patientSchedulingService.AddSchedule(newSchedule);
                if (res > 0)
                    return Ok(new { status = StatusCodes.Status200OK, success = true, data = "Schedule Added successfully" });
                if (res < 0)
                    return Ok(new { status = StatusCodes.Status208AlreadyReported, success = false, data = "Schedule Already available" });
                else
                    return Ok(new { status = StatusCodes.Status400BadRequest, success = false, data = "Error while adding the Schedule" });
            }
            else
                return BadRequest(new { status = StatusCodes.Status400BadRequest, success = false, data = "Error while adding the Schedule" });

        }

        [HttpGet("{email}/{role}")]
        public async Task<IEnumerable<SchedulingModel>> GetSchedulededAppointments(string email, string role)
        {
            var res = Enumerable.Empty<SchedulingModel>();
            if(role == UserRolesModels.Patient)
            {
                Guid pId = await _userService.GetUserId(email, role);
                res = await _patientSchedulingService.GetSchedulededAppointments(pId);
            }
            if(role == UserRolesModels.Physician)
            {
                Guid EmployeeId = await _userService.GetUserId(email, role);
                res = await _patientSchedulingService.GetPhysicianSchedulededAppointments(EmployeeId);
            }
            if (res != null)
                return res;
            else
                return Enumerable.Empty<SchedulingModel>();     
        }

        [HttpGet("{pId}")]
        public async Task<IEnumerable<SchedulingModel>> GetSchedulededPatientAppointments(Guid pId)
        {
           
            var res = await _patientSchedulingService.GetSchedulededAppointments(pId);
            if (res != null)
                return res;
            else
                return Enumerable.Empty<SchedulingModel>();
        }

        [HttpGet]
        [Route("GetPatientAppointmentEditHistory/{scheduleId}")]
        public async Task<IEnumerable<SchedulingHistoryModel>> GetPatientAppointmentEditHistory(Guid scheduleId)
        {
            var res = await _patientSchedulingService.GetAppointmentEditHistory(scheduleId);
            return res;
        }
        [HttpGet("GetById/{ScheduleId}")]
        public async Task<SchedulingModel> GetSchedule(Guid ScheduleId)
        {
            var res = await _patientSchedulingService.GetScheduleById(ScheduleId);
            return res;
        }

        [HttpGet("GetByDate/{appointmentDate}")]
        public async Task<IEnumerable<SchedulingModel>> GetAllSchedules(DateTime? appointmentDate)
        {
            if (appointmentDate == null)
            {
                appointmentDate = DateTime.Now;
            }
            var res = await _patientSchedulingService.GetAllSchedules((DateTime)appointmentDate);
            return res;
        }

        [HttpDelete("{scheduleId}")]
        public async Task<IActionResult> DeletePatientAllergy(Guid scheduleId)
        {
            int result = await _patientSchedulingService.DeleteSchedule(scheduleId);
            if (result == 1)
                return Ok(new { status = StatusCodes.Status200OK, success = true, data = "Deleted schedule successfully" });
            else
                return Ok(new { status = StatusCodes.Status404NotFound, success = false, data = "error while deleting schedule" });

        }

        [HttpPut("{email}/{role}")]
        public async Task<IActionResult> UpdateSchedule(SchedulingModel schedulingModel,string email, string role)
        {
            if (schedulingModel != null)
            {
                int result;
                if (role == UserRolesModels.Patient)
                {
                    Guid pId = await _userService.GetUserId(email, role);
                    schedulingModel.PId = pId;
                    string appId = await _userService.GetAppUserId(email);
                    result = await _patientSchedulingService.EditSchedule(schedulingModel, appId, role);
                }
                else
                {
                    string appUserId = await _userService.GetAppUserId(email);
                    result = await _patientSchedulingService.EditSchedule(schedulingModel, appUserId, role);
                }
                 
                if (result > 0)
                    return Ok(new { status = StatusCodes.Status200OK, success = true, data = "Edited Schedule Successfully"});
                else
                    return Ok(new { status = StatusCodes.Status400BadRequest, success = false, data = "error while editing schedule" });
            }
            else
                return BadRequest(new { status = StatusCodes.Status400BadRequest, success = false, data = "Schedule data is null" });

        }

        [HttpPut("UpdateScheduleStatus/{ScheduleId}/{Schedulestatus}")]
        public async Task<IActionResult> UpdateScheduleStatus(Guid ScheduleId,string Schedulestatus)
        {
            int result = await _patientSchedulingService.UpdateScheduleStatus(ScheduleId, Schedulestatus);
            if (result == 1)
                return Ok(new { status = StatusCodes.Status200OK, success = true, data = "Updated status Successfully" });
            else
                return Ok(new { status = StatusCodes.Status400BadRequest, success = false, data = "error while updating status" });
        }
    }
}
