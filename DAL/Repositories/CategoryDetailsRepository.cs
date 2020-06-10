using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;
using AuctionCore.Models.CategoryDetailsModel;
using AuctionCore.DAL.Settings;

namespace AuctionCore.DAL.Repositories
{
    public class CategoryDetailsRepository
    {
        private readonly IMongoCollection<CategoryDetails> _categories;
        private readonly ICategoryDatabaseSettings settings = new CategoryDatabaseSettings
        {
            ConnectionString = "mongodb://192.168.99.100:27017", 
            DatabaseName = "auctionDb", 
            CollectionName = "categories"
        };

        public CategoryDetailsRepository()
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _categories = database.GetCollection<CategoryDetails>(settings.CollectionName);
        }

        public List<CategoryDetails> Get() =>
            _categories.Find<CategoryDetails>(category => true).ToList();

    }
}
