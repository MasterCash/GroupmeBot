using Newtonsoft.Json;

namespace GroupmeAPIHandler.Models
{
    [JsonObject]
    public class BotResponse
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("bot_id")]
        public string Id { get; set; }
        [JsonProperty("group_id")]
        public string GroupId { get; set; }
        [JsonProperty("group_name")]
        public string GroupName { get; set; }
        [JsonProperty("avatar_url")]
        public string AvatarUrl { get; set; }
        [JsonProperty("callback_url")]
        public string CallbackUrl { get; set; }
        [JsonProperty("dm_notification")]
        public string DmNotification { get; set; }
    }
}
