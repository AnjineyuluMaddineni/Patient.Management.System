using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Web.Entities
{
    public class PatientDiagnosis
    {
        [Key]
        public Guid Id { get; set; }
        [ForeignKey("Diagnosis")]
        public Guid DiagnosisId { get; set; }
        [ForeignKey("PatientVisit")]
        public Guid VisitId { get; set; }
        public string Description { get; set; }
        public PatientVisit PatientVisit { get; set; }
        public Diagnosis Diagnosis { get; set; }

    }
}
