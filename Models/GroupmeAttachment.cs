using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupmeBot.Models
{
    [JsonObject]
    public class GroupmeAttachment
    {
        [JsonProperty("type", Required = Required.Always)]
        public string Type { get; set; }
    }


    [JsonObject]
    public class ImageAttachment : GroupmeAttachment
    {
        [JsonProperty("url", Required = Required.Always)]
        public string Url { get; set; }
        [JsonProperty("source_url")]
        public string SourceUrl { get; set; }

        public ImageAttachment()
        {
            Type = "image";
        }
    }

    [JsonObject]
    public class LinkedImageAttachment : GroupmeAttachment
    {
        [JsonProperty("url", Required = Required.Always)]
        public string Url { get; set; }

        public LinkedImageAttachment()
        {
            Type = "linked_image";
        }
    }


    [JsonObject]
    public class MentionsAttachment : GroupmeAttachment
    {
        [JsonProperty("user_ids", Required = Required.Always)]
        public List<string> UserIds { get; set; }
        [JsonProperty("loci", Required = Required.Always)]
        public List<int[]> Locations { get; set; }

        public MentionsAttachment()
        {
            Type = "mentions";
        }
    }

    [JsonObject]
    public class VideoAttachment : GroupmeAttachment
    {
        [JsonProperty("preview_url")]
        public string PreviewUrl { get; set; }
        [JsonProperty("url", Required = Required.Always)]
        public string Url { get; set; }

        public VideoAttachment()
        {
            Type = "video";
        }
    }

    [JsonObject]
    public class EventAttachment : GroupmeAttachment
    {
        [JsonProperty("event_id")]
        public string EventId { get; set; }
        [JsonProperty("view")]
        public string View { get; set; }

        public EventAttachment()
        {
            Type = "event";
        }
    }

    [JsonObject]
    public class PollAttachment : GroupmeAttachment
    {
        [JsonProperty("poll_id")]
        public string PollId { get; set; }

        public PollAttachment()
        {
            Type = "poll";
        }
    }

    [JsonObject]
    public class FileAttachment : GroupmeAttachment
    {
        [JsonProperty("file_id")]
        public string FileId { get; set; }

        public FileAttachment()
        {
            Type = "file";
        }
    }

    [JsonObject]
    public class EmojiAttachment : GroupmeAttachment
    {
        [JsonProperty("charmap")]
        public List<int[]> CharacterMap { get; set; }
        [JsonProperty("placeholder")]
        public char Placeholder { get; set; }

        public EmojiAttachment()
        {
            Type = "emoji";
        }
    }

    [JsonObject]
    public class LocationAttachment : GroupmeAttachment
    {
        [JsonProperty("lat")]
        public string Latitude { get; set; }
        [JsonProperty("lng")]
        public string Longitude { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }

        public LocationAttachment()
        {
            Type = "location";
        }
    }
}


