using System.Collections.Generic;
using Newtonsoft.Json;

namespace GroupmeAPIHandler.Models
{
    [JsonObject]
    public class BotMessageRequest
    {
        [JsonProperty("bot_id", Required = Required.Always)]
        public string Id { get; set; }
        [JsonProperty("text", Required = Required.Always)]
        public string Text { get; set; }
        [JsonProperty("picture_url")]
        public string PictureUrl { get; set; }

        [JsonProperty("attachments")]
        public List<GroupmeAttachment> Attachments { get; set; }

    }
}
