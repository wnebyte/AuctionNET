using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace AuctionCore.Models.Auction
{
    public class Image
    {
        [BsonRequired]
        [BsonElement(elementName: "bytes")]
        public byte[] Bytes { get; set; }

        [BsonRequired]
        [BsonElement(elementName: "contentType")]
        public string ContentType { get; set; } 

        [BsonRequired]
        [BsonElement(elementName: "size")]
        public int Size { get; set; }

        public static bool AcceptedContentType(string contentType) =>
            new List<string>() { "image/jpg", "image/jpeg", "image/png" }.Contains(contentType);

    }
}
