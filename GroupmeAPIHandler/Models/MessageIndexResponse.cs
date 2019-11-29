using System.Collections.Generic;
using Newtonsoft.Json;

namespace GroupmeAPIHandler.Models
{
    [JsonObject]
    public class MessageIndexResponse
    {
        [JsonProperty("count")]
        public int Count { get; set; }
        [JsonProperty("messages")]
        public List<GroupmeMessage> Messages { get; set; }
    }
}
