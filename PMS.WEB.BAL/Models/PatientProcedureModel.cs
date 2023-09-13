using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Web.Models
{
    public class PatientProcedureModel
    {
        public Guid Id { get; set; }
        public Guid ProcedureId { get; set; }
        public Guid VisitId { get; set; }
        public string Description { get; set; }
        public PatientVisitModel PatientVisit { get; set; }
        public ProcedureModel Procedure { get; set; }
    }
}
