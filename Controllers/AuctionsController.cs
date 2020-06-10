using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AuctionCore.Models.AuctionModel;
using AuctionCore.Models.CategoryDetailsModel;
using AuctionCore.Models;
using AuctionCore.BLL.Services;
using AuctionCore.BLL.Queries;
using AuctionCore.Utils;
using AuctionCore.BLL.Validation; 

namespace AuctionCore.Controllers
{
    public class AuctionsController : BaseController
    {
        private readonly AuctionService _auctions;
        private readonly CategoryDetailsService _categories;

        public AuctionsController()
        {
            _auctions = new AuctionService();
            _categories = new CategoryDetailsService();
        }

        [HttpGet("/Auctions")]
        public IActionResult Auctions(string category, string search, string orderBy)
        {
            List<Auction> auctions = _auctions.Get();
            ViewData["categories"] = _categories.Get();

            if (category != null)
            {
                string[] categorySplit = category.Split(".");

                if (categorySplit.Length == 1)
                {
                    auctions = new List<Auction>(
                        from auction in auctions
                        where auction.Item.Category.Main.StripNonLatin().ToLower() == categorySplit[0].StripNonLatin().ToLower()
                        select auction
                        );
                }
                else if (categorySplit.Length == 2)
                {
                    auctions = new List<Auction>(
                        from auction in auctions
                        where auction.Item.Category.Main.StripNonLatin().ToLower() == categorySplit[0].StripNonLatin().ToLower() &&
                        auction.Item.Category.Sub.StripNonLatin().ToLower() == categorySplit[1].StripNonLatin().ToLower()
                        select auction
                        );
                }
            }

            if (search != null)
            {
                auctions = new List<Auction>(
                    from auction in auctions
                    where auction.Item.Name.ToLower() == search.ToLower()
                    select auction
                    );
            }

            if (orderBy != null)
            {
                string[] orderBySplit = orderBy.Split(".");
                bool asc = true;

                if (orderBySplit.Length == 2 && orderBySplit[1].ToUpper() == "DESC") asc = false;

                auctions = AuctionQueries.OrderBy(auctions, orderBySplit[0].ToLower(), asc);
            }

            return View(auctions);
        }


        [HttpGet("/Create")]
        public IActionResult Create()
        {
            ViewData["categories"] = _categories.Get();

            return View();
        }

        [HttpPost("/Create")]
        public IActionResult Create(Auction auction, string category)
        {
            if (ModelState.IsValid)
            {

            }

            return View(auction);
        }
    }
}