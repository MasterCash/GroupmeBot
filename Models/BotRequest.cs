using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GroupmeBot.Models
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
