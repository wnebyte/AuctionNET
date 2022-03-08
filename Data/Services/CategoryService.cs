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
    }
}
