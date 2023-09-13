using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Web.Entities
{
    [Index(nameof(AllergyId), nameof(Isoforms), IsUnique = true)]
    public class Allergy
    {
        [Key]
        public Guid AId { get; set; }
        public string AllergyId { get; set; }
        public string Isoforms { get; set; }
        public string AllergyType { get; set; }
        public string AllergyName { get; set; }
        public string AllergenSource { get; set; }       
        public string Allerginicity { get; set; }
    }
}
