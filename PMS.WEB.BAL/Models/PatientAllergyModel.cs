using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Web.Models
{
    public class PatientAllergyModel
    {
        public Guid AllergyInfoId { get; set; }
        public bool IsFatal { get; set; }
        public string Description { get; set; }
        public string ClinicalInfo { get; set; }
        public Guid AId { get; set; }
        public AllergyModel Allergy { get; set; }
        public Guid PId { get; set; }
        public PatientModel Patient { get; set; }
    }
}
