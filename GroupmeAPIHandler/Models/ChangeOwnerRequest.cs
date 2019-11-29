using System.Collections.Generic;
using Newtonsoft.Json;

namespace GroupmeAPIHandler.Models
{
    [JsonObject]
    public class ChangeOwnerRequest
    {
        [JsonProperty("requests")]
        public List<ChangeOwnerRequestItem> Requests { get; set; }
    }
}
