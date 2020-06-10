using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuctionCore.DAL.Repositories;
using AuctionCore.Models.CategoryDetailsModel;

namespace AuctionCore.BLL.Services
{
    public class CategoryDetailsService
    {
        private readonly CategoryDetailsRepository repo;

        public CategoryDetailsService()
        {
            repo = new CategoryDetailsRepository();
        }

        public List<CategoryDetails> Get() =>
            repo.Get();
    }
}
