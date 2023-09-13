using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Web.Helper
{
    public class CustomError
    {
        [JsonProperty(PropertyName = "code")]
        public string Code { get; set; }
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }
        [JsonProperty(PropertyName = "detail")]
        public string Detail { get; set; }
        [JsonProperty(PropertyName = "docUrl")]
        public string DocUrl { get; set; }
        [JsonProperty(PropertyName = "traceId")]
        public string TraceId { get; set; }
    }
}
