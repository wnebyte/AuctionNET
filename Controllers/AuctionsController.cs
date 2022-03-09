using System.IO;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using AuctionCore.Models.Auction;
using AuctionCore.Models.Session;
using AuctionCore.Data.Services;
using AuctionCore.Utils.Extensions;

namespace AuctionCore.Controllers
{
    public class AuctionsController : Controller
    {
        private readonly IAuctionService _auctions;

        private readonly ICategoryService _categories;

        private readonly ISessionService _sessions;

        public AuctionsController(
            IAuctionService auctions, ICategoryService categories, ISessionService sessions)
        {
            _auctions = auctions;
            _categories = categories;
            _sessions = sessions;
        }

        [HttpGet("/Auctions")]
        public IActionResult Auctions(
            string category, string range, string search, string orderBy)
        {
            List<Auction> auctions = _auctions.GetAll();
            ViewData["categories"] = _categories.GetAll();

            if (category != null)
            {
                string[] categories = category.ToLower().Split(".");

                // only primary category has been specified
                if (categories.Length == 1)
                {
                    auctions = auctions.FindAll(a => a.Item.Category.Primary.ToLower().StripNonLetters() == categories[0]);
                }
                // primary and secondary category has been specified
                else if (categories.Length == 2)
                {
                    auctions = auctions.FindAll(a => a.Item.Category.Primary.ToLower().StripNonLetters() == categories[0] && 
                    a.Item.Category.Secondary.ToLower().StripNonLetters() == categories[1]);
                }
            }

            if (range != null)
            {
                string[] split = range.Split('-');

                if (split.Length == 2 || split.Length == 3)
                {
                    split[split.Length - 1] = split[split.Length - 1] == "&#8734;" ? double.MaxValue.ToString() : split[split.Length - 1];

                    if (double.TryParse(split.Length == 2 ? split[0] : split[1], out double lower) &&
                        double.TryParse(split.Length == 2 ? split[1] : split[2], out double upper))
                    {
                        auctions = auctions.MyRange(lower, upper, split.Length == 2 ? "startingprice" : split[0]).ToList();
                    }
                }
            }

            if (search != null)
            {
                auctions = auctions.FindAll(auc => auc.Item.Name.ToLower() == search.ToLower());
            }

            if (orderBy != null)
            {
                string[] order = orderBy.ToLower().Split(".");
                auctions = auctions.MyOrderBy(order[0], order.Length == 2 && order[1] == "desc" ? false : true).ToList();
            }

            return View(auctions);
        }

        [HttpGet("/Create")]
        public IActionResult Create(string orderBy)
        {
            if (_sessions.Exists(HttpContext, "session:id", out Session session) && session.Username != null)
            {
                ViewData["categories"] = _categories.GetAll();
                ViewData["auctions"]   = _auctions.GetAll().FindAll(auc => auc.Auctioneer == session.Username);

                if (orderBy != null)
                {
                    string[] order = orderBy.ToLower().Split(".");
                    bool asc = order.Length == 2 && order[1] == "desc" ? false : true;

                    ViewData["auctions"] = ((List<Auction>)ViewData["auctions"]).MyOrderBy(order[0], asc);
                }
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        [ValidateAntiForgeryToken]
        [HttpPost("/Create")]
        public IActionResult Create(Auction auction)
        {
            if (_sessions.Exists(HttpContext, "session:id", out Session session) && session.Username != null)
            {
                ViewData["categories"] = _categories.GetAll();
                ViewData["auctions"]   = _auctions.GetAll().FindAll(auc => auc.Auctioneer == session.Username);

                if (ModelState.IsValid)
                {
                    if (auction.Item.ImageFiles != null)
                    {
                        foreach (IFormFile file in auction.Item.ImageFiles)
                        {
                            var binaryReader = new BinaryReader(file.OpenReadStream());
                            byte[] data = binaryReader.ReadBytes((int)file.Length);
                            binaryReader.Close();
                            auction.Item.Images.Add(new Image { Data = data, ContentType = file.ContentType });
                        }
                    }
                    auction.Auctioneer = session.Username;
                    _auctions.Insert(auction);
					_categories.IncrementCount(auction.Item.Category.Primary, auction.Item.Category.Secondary);
                    return RedirectToAction("Create");
                }
                return View(auction);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}