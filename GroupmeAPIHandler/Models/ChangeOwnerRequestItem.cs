using Newtonsoft.Json;

namespace GroupmeAPIHandler.Models
{
    [JsonObject]
    public class ChangeOwnerRequestItem
    {
        [JsonProperty("group_id")]
        public string GroupId { get; set; }
        [JsonProperty("owner_id")]
        public string OwnerId { get; set; }
    }
}
