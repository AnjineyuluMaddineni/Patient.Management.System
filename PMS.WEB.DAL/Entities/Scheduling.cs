using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Web.Entities
{
    public class Scheduling
    {
        [Key]
        public Guid ScheduleId { get; set; }
        public string Status { get; set; }      
        public string Description { get; set; }
        public DateTime AppointmentDate { get; set; }
        [ForeignKey("TimeSlot")]
        public int TimeSlotId { get; set; }
        public TimeSlot TimeSlot { get; set; }
        public string MeetingTitle { get; set; }
        [ForeignKey("Patient")]
        public Guid PId { get; set; }
        public Patient Patient { get; set; }
        [ForeignKey("HospitalUser")]
        public Guid EmployeeId { get; set; }
        public HospitalUser HospitalUser { get; set; }
        public ICollection<SchedulingHistory> SchedulingsHistory { get; set; }
        public ICollection<PatientVisit> PatientVisit { get;set; }
    }
}
