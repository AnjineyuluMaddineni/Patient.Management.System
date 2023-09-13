using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Web.Models
{
    public class AllergyModel
    {
        public Guid AId { get; set; }
        public string AllergyId { get; set; }
        public string AllergyType { get; set; }
        public string AllergyName { get; set; }
        public string AllergenSource { get; set; }
        public string Isoforms { get; set; }
        public string Allerginicity { get; set; }
        
    }
}
