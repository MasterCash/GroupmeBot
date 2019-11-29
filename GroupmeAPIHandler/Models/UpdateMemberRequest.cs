using Newtonsoft.Json;

namespace GroupmeAPIHandler.Models
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
