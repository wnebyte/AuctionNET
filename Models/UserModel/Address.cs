using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AuctionCore.Models.UserModel
{
    public class Address
    {
        [BsonRequired]
        [BsonElement(elementName: "country")]
        [Required, Display(Name = "Country")]
        public string Country { get; set; }

        [BsonRequired]
        [BsonElement(elementName: "state")]
        [Required, Display(Name = "State/County/Region")]
        public string State { get; set; }

        [BsonRequired]
        [BsonElement(elementName: "zipCode")]
        [Required, Display(Name = "Zip Code/Area Code")]
        public int ZipCode { get; set; }

        [BsonRequired]
        [BsonElement(elementName: "street")]
        [Required, Display(Name = "Street Name")]
        public string Street { get; set; }

        [BsonRequired]
        [BsonElement(elementName: "streetNumber")]
        [Required, Display(Name = "Street Number")]
        public string StreetNumber { get; set; }

    }
}
