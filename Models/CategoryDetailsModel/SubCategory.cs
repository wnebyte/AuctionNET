using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;


namespace AuctionCore.Models.CategoryDetailsModel
{
    public class SubCategory
    {
        [BsonRequired]
        [BsonElement(elementName: "_id"), BsonRepresentation(BsonType.Double)]
        public double Id { get; set; }

        [BsonRequired]
        [BsonElement(elementName: "name")]
        public string Name { get; set; }

        [BsonRequired]
        [BsonElement(elementName: "count")]
        public int Count { get; set; }

    }
}
