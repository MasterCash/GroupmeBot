using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GroupmeBot.Models
{
    [JsonObject]
    public class MemberAddRequestItem
    {
        [JsonProperty("nickname", Required = Required.Always)]
        public string Nickname { get; set; }
        [JsonProperty("user_id")]
        public string UserId { get; set; }
        [JsonProperty("phone_number")]
        public string PhoneNumber { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("guid")]
        public string Guid { get; set; }
    }
}
