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
        [BsonElement(elementName: "subs")]
        public List<SubCategory> Subs { get; set; }

        public int Count() =>
            Subs.Sum(s => s.Count);
    }

}
