using System.Collections.Generic;
using Newtonsoft.Json;

namespace GroupmeAPIHandler.Models
{
    [JsonObject]
    public class MemberAddRequest
    {
        [JsonProperty("members")]
        public List<MemberAddRequestItem> Members { get; set; }
    }
}
