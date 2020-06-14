using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AuctionCore.Models.AuctionModel
{
    public class Image
    {
        [BsonRequired]
        [BsonElement(elementName: "bytes")]
        public byte[] Bytes { get; set; }

        [BsonRequired]
        [BsonElement(elementName: "size")]
        public int Size { get; set; }

        [BsonRequired]
        [BsonElement(elementName: "contentType")]
        public string ContentType { get; set; }
    }
}
