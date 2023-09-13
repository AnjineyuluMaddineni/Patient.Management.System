using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Web.Entities
{
    public class TimeSlot
    {
        [Key]
        public int TimeSlotId { get; set; }
        public string Timing { get; set; }
    }
}
