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
            IMongoDatabase database = client.GetDatabase(settings.DatabaseName);
            _collection = database.GetCollection<Category>(settings.CollectionName);
        }

        public List<Category> GetAll() =>
            _collection.Find(category => true).ToList();

		public Category Get(string primary)
		{
			return _collection.Find(category => category.Name.Equals(primary))
				.FirstOrDefault();
		}

		public void Update(Category category) => 
			_collection.ReplaceOne(c => c.Id == category.Id, category);

	}
}
