﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GroupmeBot.Models
{
    [JsonObject]
    public class GroupUpdateRequest
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("image_url")]
        public string ImageUrl { get; set; }
        [JsonProperty("office_mode")]
        public bool? OfficeMode { get; set; }
        [JsonProperty("share")]
        public bool? Share { get; set; }
    }
}
