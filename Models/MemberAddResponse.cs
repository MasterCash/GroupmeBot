using Newtonsoft.Json;

namespace GroupmeBot.Models
{
    [JsonObject]
    public class MemberAddResponse
    {
        [JsonProperty("results_id")]
        public string ResultsId { get; set; }
    }
}
