using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Web.Entities
{
    public class Medication
    {
        [Key]
        public Guid MId { get; set; }
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ApplNo { get; set; }
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProductNo { get; set; }
        public string Form { get; set; }
        public string Strength { get; set; }
        public int ReferenceDrug { get; set; }
        public string DrugName { get; set; }
        public string ActiveIngredient { get; set; }
        public string? ReferenceStandard { get; set; }
        public virtual ICollection<PatientMedication> PatientMedications { get; set; }

    }
}
