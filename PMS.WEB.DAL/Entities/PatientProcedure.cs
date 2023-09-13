using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Web.Entities
{
    public class PatientProcedure
    {
        [Key]
        public Guid Id { get; set; }
        [ForeignKey("Procedure")]
        public Guid ProcedureId { get; set; }
        [ForeignKey("PatientVisit")]
        public Guid VisitId { get; set; }
        public string Description { get; set; }
        public PatientVisit PatientVisit { get; set; }
        public Procedure Procedure { get; set; }
    }
}
