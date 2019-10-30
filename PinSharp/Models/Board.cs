using System;
using Newtonsoft.Json;
using PinSharp.Models.Counts;
using PinSharp.Models.Images;

namespace PinSharp.Models
{
    public class Board : IDetailedBoard, IUserBoard, IBoard
    {
        public string Id { get; set; }

        public string Url { get; set; }

        public string Name { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        public string Description { get; set; }

        public IBoardCounts Counts { get; set; }

        [JsonProperty("image")]
        public IBoardImageList Images { get; set; }

        public string Reason { get; set; }

        public string Privacy { get; set; }

    }
}
