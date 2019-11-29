using Newtonsoft.Json;

namespace GroupmeAPIHandler.Models
{
    [JsonObject]
    public class MessageCreateRequest
    {
        [JsonProperty("message")]
        public GroupmeMessage Message { get; set; }
    }
}
