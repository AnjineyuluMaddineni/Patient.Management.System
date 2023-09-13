using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Web.Helper
{
    public class JsonError
    {
        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }
        [JsonProperty(PropertyName = "success")]
        public bool Success { get; set; }
        [JsonProperty(PropertyName = "error")]
        public CustomError Error { get; set; }
    }
}
