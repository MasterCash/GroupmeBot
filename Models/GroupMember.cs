using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GroupmeBot.Models
{
    [JsonObject]
    public class GroupMember
    {
        [JsonProperty("user_id")]
        public string UserId { get; set; }
        [JsonProperty("nickname")]
        public string Nickname { get; set; }
        [JsonProperty("image_url")]
        public string ImageUrl { get; set; }
        [JsonProperty("muted")]
        public bool Muted { get; set; }
        [JsonProperty("autokicked")]
        public bool AutoKicked { get; set; }
        [JsonProperty("roles")]
        public List<string> Roles { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
