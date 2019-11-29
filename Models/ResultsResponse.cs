using System.Collections.Generic;
using Newtonsoft.Json;

namespace GroupmeBot.Models
{
    [JsonObject]
    public class ResultsResponse
    {
        [JsonProperty("members")]
        public List<ResultsMemberItem> Members { get; set; }
    }
}
