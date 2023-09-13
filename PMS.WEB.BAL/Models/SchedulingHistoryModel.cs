using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Web.Models
{
    public class SchedulingHistoryModel
    {
        public Guid ScheduleHistoryId { get; set; }
        public string ReasonForEdit { get; set; }
        public string EditedTime { get; set; }
        public string PreviousDetails { get; set; }
        public Guid ScheduleId { get; set; }
        public SchedulingModel Scheduling { get; set; }
        public string AppUserId { get; set; }
        public UserModel AppUser { get; set; }
    }
}
