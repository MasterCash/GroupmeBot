using Newtonsoft.Json;

namespace GroupmeAPIHandler.Models
{
    [JsonObject]
    public class ChangeOwnerResult
    {
        [JsonProperty("group_id")]
        public string GroupId { get; set; }
        [JsonProperty("owner_id")]
        public string OwnerId { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
