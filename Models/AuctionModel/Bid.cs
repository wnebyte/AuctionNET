using System;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AuctionCore.Models.AuctionModel
{
    public class Bid
    {
        [BsonId]
        [BsonElement(elementName: "_id"), BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

        [BsonRequired]
        [BsonElement(elementName: "amount"), BsonRepresentation(BsonType.Decimal128)]
        public decimal Amount { get; set; }

        [BsonRequired]
        [BsonDefaultValue(false)]
        [BsonElement(elementName: "isBuyout")]
        public bool IsBuyout { get; set; } = false; 

        [BsonRequired]
        [BsonElement(elementName: "bidder")]
        public string Bidder { get; set; }

        [BsonRequired]
        [BsonRepresentation(BsonType.DateTime), BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        [BsonElement(elementName: "timestamp")]
        public DateTime Timestamp { get; set; } = DateTime.Now;
    }
}
