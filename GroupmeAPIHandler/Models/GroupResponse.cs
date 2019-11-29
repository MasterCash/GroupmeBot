using System.Collections.Generic;
using Newtonsoft.Json;

namespace GroupmeAPIHandler.Models
{
    [JsonObject]
    public class GroupResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("group_id")]
        public string GroupId { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("phone_number")]
        public string PhoneNumber { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("image_url")]
        public string ImageUrl { get; set; }
        [JsonProperty("creator_user_id")]
        public string CreatorUserId { get; set; }
        [JsonProperty("created_at")]
        public long CreatedAt { get; set; }
        [JsonProperty("updated_at")]
        public long UpdatedAt { get; set; }
        [JsonProperty("office_mode")]
        public bool OfficeMode { get; set; }
        [JsonProperty("members")]
        public List<GroupMember> Members { get; set; }
        [JsonProperty("share_url")]
        public string ShareUrl { get; set; }
        [JsonProperty("share_qr_code_url")]
        public string ShareQRCodeUrl { get; set; }
        [JsonProperty("messages")]
        public GroupMessagesOverview MessagesOverview { get; set; }
        [JsonProperty("max_members")]
        public int MaxMembers { get; set; }
    }
}
