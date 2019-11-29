using Newtonsoft.Json;

namespace GroupmeAPIHandler.Models
{
    [JsonObject]
    public class GroupsRequest
    {
        [JsonProperty("page")]
        public int? Page { get; set; }
        [JsonProperty("per_page")]
        public int? PerPage { get; set; }
        [JsonProperty("omit")]
        public string Omit { get; set; }
    }
}
