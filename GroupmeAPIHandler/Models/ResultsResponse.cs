using System.Collections.Generic;
using Newtonsoft.Json;

namespace GroupmeAPIHandler.Models
{
    [JsonObject]
    public class ResultsResponse
    {
        [JsonProperty("members")]
        public List<ResultsMemberItem> Members { get; set; }
    }
}
