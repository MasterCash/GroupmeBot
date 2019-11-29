using System.Collections.Generic;
using Newtonsoft.Json;

namespace GroupmeAPIHandler.Models
{
    public class ChangeOwnerResponse
    {
        [JsonProperty("results")]
        public List<ChangeOwnerResult> Results { get; set; }

    }
}
