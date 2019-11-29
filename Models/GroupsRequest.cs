using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GroupmeBot.Models
{
    [JsonObject]
    public class GroupsRequest
    {
        [JsonProperty("page")]
        public int? Page { get; set; }
        [JsonProperty("per_page")]
        public int? PerPage { get; set; }
        [JsonProperty("omit")]
        public string Omit { get; set; }
    }
}
