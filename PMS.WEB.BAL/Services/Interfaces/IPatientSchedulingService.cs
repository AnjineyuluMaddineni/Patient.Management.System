using PMS.Web.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMS.Web.Services
{
    public interface IPatientSchedulingService
    {
        Task<IEnumerable<SchedulingModel>> GetAppointmentDetails(Guid PatientId);
        Task<int> AddSchedule(SchedulingModel schedulingModel);
        Task<IEnumerable<SchedulingModel>> GetSchedulededAppointments(Guid patientId);
        Task<IEnumerable<SchedulingModel>> GetPhysicianSchedulededAppointments(Guid EmployeeId);
        Task<IEnumerable<SchedulingModel>> GetAllSchedules(DateTime appointmentDate);
        Task<SchedulingModel> GetScheduleById(Guid ScheduleId);
        Task<int> EditSchedule(SchedulingModel schedulingModel,string userId,string role);
        Task<int> DeleteSchedule(Guid scheduleId);
        int UpdateSchedulingHistory(SchedulingHistoryModel schedulingHistoryModel);
        Task<IEnumerable<TimeSlotModel>> AvailableTimeSlot(Guid employeeId, DateTime selecetedDate);
        Task<IEnumerable<SchedulingHistoryModel>> GetAppointmentEditHistory(Guid scheduleId);

        Task<int> UpdateScheduleStatus(Guid scheduleId, string scheduleStatus);

    }
}
