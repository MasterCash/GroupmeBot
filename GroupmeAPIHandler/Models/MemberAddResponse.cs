using Newtonsoft.Json;

namespace GroupmeAPIHandler.Models
{
    [JsonObject]
    public class MemberAddResponse
    {
        [JsonProperty("results_id")]
        public string ResultsId { get; set; }
    }
}
