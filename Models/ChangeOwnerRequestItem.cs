using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GroupmeBot.Models
{
    [JsonObject]
    public class ChangeOwnerRequestItem
    {
        [JsonProperty("group_id")]
        public string GroupId { get; set; }
        [JsonProperty("owner_id")]
        public string OwnerId { get; set; }
    }
}
