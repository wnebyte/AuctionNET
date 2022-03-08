using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AuctionCore.Models.Auction
{
    public class Category
    {
        [Required]
        [Display(Name = "Auction's main category")]
        [BsonRequired]
        [BsonElement(elementName: "primary")]
        public string Primary { get; set; }

        [Required]
        [Display(Name = "Auction's sub category")]
        [BsonRequired]
        [BsonElement(elementName: "secondary")]
        public string Secondary { get; set; }
    }
}
