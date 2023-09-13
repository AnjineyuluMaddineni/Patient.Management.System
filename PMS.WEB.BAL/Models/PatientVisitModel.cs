using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Web.Models
{
    public class PatientVisitModel
    {
        public Guid VisitId { get; set; }
        public DateTime VisitDate { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public int BloodPressure { get; set; }
        public int BodyTemperature { get; set; }
        public int RespirationRate { get; set; }
        public Guid ScheduleId { get; set; }
        public SchedulingModel Scheduling { get; set; }
        public ICollection<PatientMedicationModel> PatientMedications { get; set; }
        public ICollection<PatientProcedureModel> PatientProcedures { get; set; }
        public ICollection<PatientDiagnosisModel> PatientDiagnoses { get; set; }

    }
}
