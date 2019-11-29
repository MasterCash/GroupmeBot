using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GroupmeBot.Models
{
    [JsonObject]
    public class MemberAddRequest
    {
        [JsonProperty("members")]
        public List<MemberAddRequestItem> Members { get; set; }
    }
}
