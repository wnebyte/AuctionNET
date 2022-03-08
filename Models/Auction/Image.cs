using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AuctionCore.Models.Auction
{
    public class Image
    {
        public static bool IsContentTypeAccepted(string contentType)
        { 
            return CONTENT_TYPES.Contains(contentType);
        }

        private static readonly ICollection<string> CONTENT_TYPES = new List<string>()
        { 
            "image/jpg",
            "image/jpeg",
            "image/png"
        };

        [BsonRequired]
        [BsonElement(elementName: "data")]
        public byte[] Data { get; set; }

        [BsonRequired]
        [BsonElement(elementName: "contentType")]
        public string ContentType { get; set; } 

        [BsonRequired]
        [BsonElement(elementName: "size")]
        public int Size { get; set; }

    }
}
