using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Web.Entities
{
    public class PatientMedication
    {
        [Key]
        public Guid Id { get; set; }
        [ForeignKey("PatientVisit")]
        public Guid VisitId { get; set; }
        [ForeignKey("Medication")]
        public Guid MId { get; set; }
        public string Description { get; set; }
        public string Dosage { get; set; }
        public PatientVisit PatientVisit { get; set; }
        public Medication Medication { get; set; }
    }
}
