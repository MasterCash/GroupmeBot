using Newtonsoft.Json;

namespace GroupmeAPIHandler.Models
{
    [JsonObject]
    public class UpdateMemberResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("user_id")]
        public string UserId { get; set; }
        [JsonProperty("nickname")]
        public string Nickname { get; set; }
        [JsonProperty("muted")]
        public bool Muted { get; set; }
        [JsonProperty("image_url")]
        public string ImageUrl { get; set; }
        [JsonProperty("autokicked")]
        public bool AutoKicked { get; set; }
        [JsonProperty("app_installed")]
        public bool AppInstalled { get; set; }
    }
}
