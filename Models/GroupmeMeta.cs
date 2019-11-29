using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GroupmeBot.Models
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
