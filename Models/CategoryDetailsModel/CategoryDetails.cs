using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace AuctionCore.Models.CategoryDetailsModel
{
    public class CategoryDetails
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

        public int GetCount() =>
            Subs.Sum(s => s.Count);
    }

}
