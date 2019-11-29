using Newtonsoft.Json;

namespace GroupmeAPIHandler.Models
{
    [JsonObject]
    public class GroupMessagesOverview
    {
        [JsonProperty("count")]
        public int Count { get; set; }
        [JsonProperty("last_message_id")]
        public string LastMessageId { get; set; }
        [JsonProperty("last_message_created_at")]
        public long LastMessageCreatedAt { get; set; }
        [JsonProperty("preview")]
        public GroupmeMessagePreview Preview { get; set; }

    }
}
