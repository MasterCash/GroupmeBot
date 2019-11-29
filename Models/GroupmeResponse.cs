using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupmeBot.Models;

namespace GroupmeBot.Models
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
