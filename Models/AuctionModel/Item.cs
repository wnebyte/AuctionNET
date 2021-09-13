using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AuctionCore.Utils.DataAnnotations;
using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AuctionCore.Models.AuctionModel
{
    public class Item
    {
        [Required]
        [Display(Name = "Auction's name")]
        [MaxLength(16)]
        [BsonRequired]
        [BsonElement(elementName: "name")]
        public string Name { get; set; }

        [Required]
        [BsonRequired]
        [BsonElement(elementName: "category")]
        public Category Category { get; set; }

        [Required]
        [Display(Name = "Auction's description")]
        [MaxLength(280)]
        [BsonRequired]
        [BsonElement(elementName: "description")]
        public string Description { get; set; }

        [BsonIgnoreIfDefault]
        [BsonElement(elementName: "keywords")]
        public List<string> Keywords { get; set; } = new List<string>();

        [BsonIgnoreIfDefault]
        [BsonElement(elementName: "images")]
        public List<Image> Images { get; set; } = new List<Image>();

        [BsonIgnore]
        [DataType(DataType.Upload)]
        [IFormFileExtensions(Extensions = ".jpg, .jpeg, .png", ErrorMessage = "Auction's images viable extensions include: jpeg, jpg, png.")]
        [IFormFileCollectionMaxSize(MaxSize = 12 * 1024 * 1024)]
        public List<IFormFile> ImageFiles { get; set; }

    }
}
