using System.Collections.Generic;
using AuctionCore.Models.Category;
using AuctionCore.Data.Repositories;

namespace AuctionCore.Data.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly CategoryRepository _repo;

        public CategoryService(CategoryRepository repo)
        {
            _repo = repo;
        }

        public List<Category> GetAll() =>
            _repo.GetAll();

		public void IncrementCount(string primary, string secondary)
		{
			Category category = _repo.Get(primary);

			if (category != null)
			{
				SubCategory subCategory = category.Children.Find((c) => c.Name.Equals(secondary));

				if (subCategory != null)
				{
					subCategory.Count++;
					_repo.Update(category);
				}
			}
		}
	}
}
