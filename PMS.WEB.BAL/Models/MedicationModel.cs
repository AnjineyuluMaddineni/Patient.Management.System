using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Web.Models
{
    public class MedicationModel
    {
        public Guid MId { get; set; }
        [Required]
        public int ApplNo { get; set; }
        [Required]
        public int ProductNo { get; set; }
        [Required]
        public string Form { get; set; }
        [Required]
        public string Strength { get; set; }
        [Required]
        public int ReferenceDrug { get; set; }
        [Required]
        public string DrugName { get; set; }
        [Required]
        public string ActiveIngredient { get; set; }
        public string? ReferenceStandard { get; set; }
        public virtual ICollection<PatientMedicationModel> PatientMedications { get; set; }

    }
}
