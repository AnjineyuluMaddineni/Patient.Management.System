﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Web.Configuration
{
    public class JwtConfig
    {
        public string Secret { get; set; }
        public string ValidIssuer { get; set; }
        public string ValidAudience { get; set; }
    }
}
