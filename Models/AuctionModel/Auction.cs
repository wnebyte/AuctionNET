using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using ExpressiveAnnotations.Attributes;

namespace AuctionCore.Models.AuctionModel
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
        [Range(0.1, double.MaxValue, ErrorMessage = "Minimum bid must be greater than or equal to 0.1 UNIT")]
        [Display(Name = "Minimum Bid"), DataType(DataType.Currency)]
        [BsonRequired]
        [BsonElement(elementName: "startingPrice"), BsonRepresentation(BsonType.Decimal128)]
        public decimal StartingPrice { get; set; }

        [AssertThat("BuyoutPrice > StartingPrice", 
            ErrorMessage = "The buyout price if specified must be greater than the minimum bid")]
        [Display(Name = "Buyout Price"), DataType(DataType.Currency)]
        [BsonDefaultValue(null), BsonIgnoreIfDefault]
        [BsonElement(elementName: "buyoutPrice"), BsonRepresentation(BsonType.Decimal128)]
        public decimal? BuyoutPrice { get; set; }

        [BsonRequired]  
        [BsonRepresentation(BsonType.DateTime), BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        [BsonElement(elementName: "posted")]
        public DateTime Posted { get; set; } = DateTime.Now;

        [BsonRequired]
        [BsonRepresentation(BsonType.DateTime), BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        [BsonElement(elementName: "expires")]
        public DateTime Expires { get; set; }

        [BsonRequired]
        [BsonElement(elementName: "auctioneer")]
        public string Auctioneer { get; set; }

        [BsonRequired]
        [BsonElement(elementName: "bids")]
        public List<Bid> Bids { get; set; } = new List<Bid>();

        [BsonRequired]
        [BsonElement(elementName: "schemaVersion")]
        public string SchemaVersion { set;  get; } = "1.0.0";

        [BsonRequired]
        [BsonElement(elementName: "currency")]
        public string Currency { get; set; } = "SEK"; 

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        public string ToJSON() =>
            ToString();
    }
}
