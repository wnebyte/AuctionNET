using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AuctionCore.Models.AuctionModel
{
    public class Item
    {
        [Required]
        [StringLength(maximumLength: 30)]
        [Display(Name = "Title")]
        [BsonRequired]
        [BsonElement(elementName: "name")]
        public string Name { get; set; }

        [BsonRequired]
        [BsonElement(elementName: "category")]
        public Category Category { get; set; }

        [StringLength(maximumLength: 200)]
        [Display(Name = "Description")]
        [BsonElement(elementName: "description")]
        public string Description { get; set; }

        [BsonRequired]
        [BsonElement(elementName: "keywords")]
        public List<string> Keywords { get; set; } = new List<string>();

        [BsonIgnoreIfDefault]
        [BsonElement(elementName: "images")]
        public List<Image> Images { get; set; } = new List<Image>();
    }
}
