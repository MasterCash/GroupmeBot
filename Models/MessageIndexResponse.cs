using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GroupmeBot.Models
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
