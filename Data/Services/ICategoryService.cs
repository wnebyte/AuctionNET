using System.Collections.Generic;
using AuctionCore.Models.Category;

namespace AuctionCore.Data.Services
{
    public interface ICategoryService
    {
        List<Category> GetAll();

		void IncrementCount(string primary, string secondary);
    }
}
