using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace AuctionCore.Models.AuctionModel
{
    public class Category
    {
        [Required]
        [Display(Name = "Auction's main category")]
        [BsonRequired]
        [BsonElement(elementName: "main")]
        public string Main { get; set; }

        [Required]
        [Display(Name = "Auction's sub category")]
        [BsonRequired]
        [BsonElement(elementName: "sub")]
        public string Sub { get; set; }
    }
}
