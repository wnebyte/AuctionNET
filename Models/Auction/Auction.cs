using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using ExpressiveAnnotations.Attributes;

namespace AuctionCore.Models.Auction
{
    public class Auction
    {
        [BsonId]
        [BsonElement(elementName: "_id"), BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonRequired]
        [BsonElement(elementName: "item")]
        public Item Item { get; set; }

        [Required]
        [Range(1.0, double.MaxValue, ErrorMessage = "The Auction's starting price must be greater than or equal to 1.0 [auction's currency].")]
        [Display(Name = "Auction's starting price"), DataType(DataType.Currency)]
        [BsonRequired]
        [BsonElement(elementName: "startingPrice"), BsonRepresentation(BsonType.Decimal128)]
        public decimal StartingPrice { get; set; }

        [AssertThat("BuyoutPrice > StartingPrice", 
            ErrorMessage = "The Auction's buyout price if assigned, must be greater than the starting price.")]
        [Display(Name = "Auction's buyout price"), DataType(DataType.Currency)]
        [BsonDefaultValue(null), BsonIgnoreIfDefault]
        [BsonElement(elementName: "buyoutPrice"), BsonRepresentation(BsonType.Decimal128)]
        public decimal? BuyoutPrice { get; set; }

        [BsonRequired]
        [BsonRepresentation(BsonType.DateTime), BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        [BsonElement(elementName: "posted")]
        public DateTime Posted { get; set; } = DateTime.UtcNow;

        [BsonRequired]
        [BsonRepresentation(BsonType.DateTime), BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        [BsonElement(elementName: "expires")]
        public DateTime Expires { get; set; } = DateTime.UtcNow.AddDays(30);

        [Display(Name = "Auctioneer")]
        [BsonRequired]
        [BsonElement(elementName: "auctioneer")]
        public string Auctioneer { get; set; }

        [BsonRequired]
        [BsonElement(elementName: "bids")]
        public List<Bid> Bids { get; set; } = new List<Bid>();

        [BsonRequired]
        [BsonElement(elementName: "schemaVersion")]
        public string SchemaVersion { get; set; } = "1.0.0";

        [Required]
        [Display(Name = "Auction's currency")]
        [BsonRequired]
        [BsonElement(elementName: "currency")]
        public string Currency { get; set; }

        public override string ToString() =>
            JsonConvert.SerializeObject(this, Formatting.Indented);

        public string ToJSON() =>
            ToString();

    }
}
