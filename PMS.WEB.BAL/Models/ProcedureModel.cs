using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Web.Models
{
    public class ProcedureModel
    {
       public Guid ProcedureId { get; set; }
       public string Code { get; set; }
       public string Description { get; set; }
       public bool IsDeprecated { get; set; }
       public virtual ICollection<PatientProcedureModel> PatientProcedures { get; set; }

    }
}
