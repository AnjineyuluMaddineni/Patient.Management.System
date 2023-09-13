using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Web.Entities
{
    public class PatientAllergy
    {
        [Key]
        public Guid AllergyInfoId { get; set; }
        public bool IsFatal { get; set; }
        public string Description { get; set; }
        public string ClinicalInfo { get; set; }
        [ForeignKey("Allergy")]
        public Guid AId { get; set; }       
        public Allergy Allergy { get; set; }       
        [ForeignKey("Patient")]
        public Guid PId { get; set; }
        public Patient Patient { get; set; }
    }
}
