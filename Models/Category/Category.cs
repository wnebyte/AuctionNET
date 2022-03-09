using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AuctionCore.Models.Category
{
    public class Category
    {
        [BsonId]
        [BsonElement(elementName: "_id"), BsonRepresentation(BsonType.Int32)]
        public int Id { get; set; }

        [BsonRequired]
        [BsonElement(elementName: "name")]
        public string Name { get; set; }

        [BsonRequired]
        [BsonElement(elementName: "children")]
        public List<SubCategory> Children { get; set; }

        public int Count() =>
            Children.Sum(s => s.Count);
    }

}
