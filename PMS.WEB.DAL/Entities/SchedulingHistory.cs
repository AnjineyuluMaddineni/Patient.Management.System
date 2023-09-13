using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Web.Entities
{
    public class SchedulingHistory
    {
        [Key]
        public Guid ScheduleHistoryId { get; set; }
        public string ReasonForEdit { get; set; }
        public DateTime EditedTime { get; set; }
        public string PreviousDetails { get; set; }
        [ForeignKey("Scheduling")]
        public Guid ScheduleId { get; set; }
        public Scheduling Scheduling{ get; set; }
        [ForeignKey("AppUser")]
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
