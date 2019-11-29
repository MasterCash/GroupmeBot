using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GroupmeBot.Models
{
    [JsonObject]
    public class ResultsMemberItem
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
        [JsonProperty("guid")]
        public string Guid { get; set; }
    }
}
