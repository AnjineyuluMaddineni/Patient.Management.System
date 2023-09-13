using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Web.Entities
{
    public class Procedure
    {           
        [Key]
        public Guid ProcedureId { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public bool IsDeprecated { get; set; }
        public virtual ICollection<PatientProcedure> PatientProcedures { get; set; }

    }
}
