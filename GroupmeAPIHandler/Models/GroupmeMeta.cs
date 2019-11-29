using System.Collections.Generic;
using Newtonsoft.Json;

namespace GroupmeAPIHandler.Models
{
    [JsonObject]
    public class GroupmeMeta
    {
        [JsonProperty("code")]
        public string Code { get; set; }
        [JsonProperty("errors")]
        public List<string> Errors { get; set; }
    }
}
