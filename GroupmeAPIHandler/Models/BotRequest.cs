using Newtonsoft.Json;

namespace GroupmeAPIHandler.Models
{
    [JsonObject]
    public class BotRequest
    {
        [JsonProperty("name", Required = Required.Always)]
        public string Name { get; set; }
        [JsonProperty("group_id", Required = Required.Always)]
        public string GroupId { get; set; }
        [JsonProperty("avatar_url")]
        public string AvatarUrl { get; set; }
        [JsonProperty("callback_url")]
        public string CallbackUrl { get; set; }
        [JsonProperty("dm_notification")]
        public string DmNotification { get; set; }
    }
}
