using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GroupmeBot.Models
{
    [JsonObject]
    public class ChangeOwnerRequest
    {
        [JsonProperty("requests")]
        public List<ChangeOwnerRequestItem> Requests { get; set; }
    }
}
