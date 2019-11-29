using System.Collections.Generic;
using Newtonsoft.Json;

namespace GroupmeBot.Models
{
    public class GroupmeMessage
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("source_guid")]
        public string SourceGuid { get; set; }
        
        [JsonProperty("created_at")]
        public long CreatedAt { get; set; }
        
        [JsonProperty("user_id")]
        public string UserId { get; set; }
        
        [JsonProperty("group_id")]
        public string GroupId { get; set; }
        
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("avatar_url")]
        public string AvatarUrl { get; set; }
        
        [JsonProperty("text")]
        public string Text { get; set; }
        
        [JsonProperty("system")]
        public bool System { get; set; }
        
        [JsonProperty("favorited_by")]
        public List<string> FavoritedBy { get; set; }

        [JsonProperty("sender_id")]
        public string SenderId { get; set; }
        
        [JsonProperty("sender_type")]
        public string SenderType { get; set; }
        
        [JsonProperty("attachments")]
        public List<GroupmeAttachment> Attachments { get; set; }
        [JsonProperty("platform")]
        public string Platform { get; set; }
    }
}
