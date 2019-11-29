using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GroupmeBot.Models
{
    public class ChangeOwnerResponse
    {
        [JsonProperty("results")]
        public List<ChangeOwnerResult> Results { get; set; }

    }
}
