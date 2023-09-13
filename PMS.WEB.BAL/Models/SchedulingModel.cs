using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Web.Models
{
    public class SchedulingModel
    {
        public Guid ScheduleId { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public DateTime AppointmentDate { get; set; }
        public int TimeSlotId { get; set; }
        public TimeSlotModel TimeSlot { get; set; }
        public string MeetingTitle { get; set; }
        public Guid PId { get; set; }
        public PatientModel Patient { get; set; }
        public Guid EmployeeId { get; set; }
        public HospitalUserModel HospitalUser { get; set; }
        public ICollection<SchedulingHistoryModel> SchedulingsHistory { get; set; }
        public ICollection<PatientVisitModel> PatientVisit { get; set; }
    }
}
