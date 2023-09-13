using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Web.Entities
{
    public class PatientVisit
    {
        [Key]
        public Guid VisitId { get; set; }
        public DateTime VisitDate { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public int BloodPressure { get; set; }
        public int BodyTemperature { get; set; }
        public int RespirationRate { get; set; }
        [ForeignKey("Scheduling")]
        public Guid ScheduleId { get; set; }
        public Scheduling Scheduling { get; set; }
        public ICollection<PatientMedication> PatientMedications { get; set; }
        public ICollection<PatientProcedure> PatientProcedures { get; set; }
        public ICollection<PatientDiagnosis> PatientDiagnoses { get; set; }

    }
}
