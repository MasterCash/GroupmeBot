using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupmeBot.Models;
using Newtonsoft.Json;

namespace GroupmeBot.Models
{
    [JsonObject]
    public class GroupmeMessagePreview
    {
        [JsonProperty("nickname")]
        public string Nickname { get; set; }
        [JsonProperty("text")]
        public string Text { get; set; }
        [JsonProperty("image_url")]
        public string ImageUrl { get; set; }
        [JsonProperty("attachments")]
        public List<GroupmeAttachment> Attachments { get; set; }
    }
}
