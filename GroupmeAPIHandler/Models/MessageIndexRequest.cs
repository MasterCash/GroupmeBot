using Newtonsoft.Json;

namespace GroupmeAPIHandler.Models
{
    [JsonObject]
    public class MessageIndexRequest
    {
        [JsonProperty("before_id")]
        public string BeforeId { get; set; }
        [JsonProperty("since_id")]
        public string SinceId { get; set; }
        [JsonProperty("after_id")]
        public string AfterId { get; set; }
        [JsonProperty("limit")]
        public int? Limit { get; set; }
    }
}
