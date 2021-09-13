using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using AuctionCore.Models.AuctionModel;
using AuctionCore.BLL.Services;
using AuctionCore.Utils;
using AuctionCore.Utils.Extensions;
using System.IO;

namespace AuctionCore.Controllers
{
    public class AuctionsController : BaseController
    {
        private readonly AuctionService _auctions;
        private readonly SessionService _sessions;
        private readonly CategoryDetailsService _categories;

        public AuctionsController()
        {
            _auctions = new AuctionService();
            _sessions = new SessionService();
            _categories = new CategoryDetailsService();
        }

        [HttpGet("/Auctions")]
        public IActionResult Auctions(string category, string range, string search, string orderBy)
        {
            List<Auction> auctions = _auctions.Get();
            ViewData["categories"] = _categories.Get();

            if (category != null)
            {
                string[] categories = category.ToLower().Split(".");

                if (categories.Length == 1)
                {
                    auctions = auctions.FindAll(auc => auc.Item.Category.Main.ToLower().StripNonLatin() == categories[0]);
                }
                else if (categories.Length == 2)
                {
                    auctions = auctions.FindAll(auc => auc.Item.Category.Main.ToLower().StripNonLatin() == categories[0] && 
                    auc.Item.Category.Sub.ToLower().StripNonLatin() == categories[1]);
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
                        auctions = auctions.MyRange(lower, upper, split.Length == 2 ? "startingprice" : split[0]);
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
                auctions = auctions.MyOrderBy(order[0], order.Length == 2 && order[1] == "desc" ? false : true);
            }

            return View(auctions);
        }


        [HttpGet("/Create")]
        public IActionResult Create(string orderBy)
        {
            if (_sessions.Exists(this.HttpContext, "session:id", out var session) && session.Username != null)
            {
                ViewData["categories"] = _categories.Get();
                ViewData["auctions"]   = _auctions.Get().FindAll(auc => auc.Auctioneer == session.Username);

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
            if (_sessions.Exists(this.HttpContext, "session:id", out var session) && session.Username != null)
            {
                ViewData["categories"] = _categories.Get();
                ViewData["auctions"]   = _auctions.Get().FindAll(auc => auc.Auctioneer == session.Username);

                if (ModelState.IsValid)
                {
                    if (auction.Item.ImageFiles != null)
                    {
                        foreach (var file in auction.Item.ImageFiles)
                        {
                            BinaryReader binaryReader = new BinaryReader(file.OpenReadStream());
                            byte[] content = binaryReader.ReadBytes((int)file.Length);
                            binaryReader.Close();

                            auction.Item.Images.Add(new Image { Bytes = content, ContentType = file.ContentType });
                        }
                    }
                    auction.Auctioneer = session.Username;
                    _auctions.Insert(auction);
                    return RedirectToAction("Create");
                }
                return View(auction);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}