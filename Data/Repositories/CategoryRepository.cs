using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using AuctionCore.Models.Category;
using AuctionCore.Data.Settings;

namespace AuctionCore.Data.Repositories
{
    public class CategoryRepository
    {
        private readonly IMongoCollection<Category> _collection;

        public CategoryRepository(ICategoryDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _collection = database.GetCollection<Category>(settings.CollectionName);
        }

        public List<Category> GetAll() =>
            _collection.Find(category => true).ToList();

    }
}
