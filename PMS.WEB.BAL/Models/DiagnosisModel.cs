﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Web.Models
{
    public class DiagnosisModel
    {
        public Guid DiagnosisId { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public bool IsDeprecated { get; set; }
        public virtual ICollection<PatientDiagnosisModel> PatientDiagnoses { get; set; }

    }
}
