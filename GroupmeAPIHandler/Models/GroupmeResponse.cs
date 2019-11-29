using Newtonsoft.Json;

namespace GroupmeAPIHandler.Models
{
    [JsonObject]
    public class GroupmeResponse
    {
         [JsonProperty("meta")]
         public GroupmeMeta Meta { get; set; }
         [JsonProperty("response")]
         public object Response { get; set; }
    }
}
