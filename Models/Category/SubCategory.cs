using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AuctionCore.Models.Category
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
