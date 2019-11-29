using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GroupmeBot.Models
{
    [JsonObject]
    public class UpdateMemberRequest
    {
        [JsonProperty("membership")]
        public UpdateMemberItem Membership { get; set; }
    }

    [JsonObject]
    public class UpdateMemberItem
    {
        [JsonProperty("nickname")]
        public string Nickname { get; set; }
    }

}
