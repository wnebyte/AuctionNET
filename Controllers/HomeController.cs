using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AuctionCore.Models;
using AuctionCore.Models.AuctionModel;
using AuctionCore.BLL.Services;
using AuctionCore.Utils;

namespace AuctionCore.Controllers
{
    public class HomeController : BaseController
    {

        private readonly AuctionService service = new AuctionService();

        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Auction auction)
        {
            if (ModelState.IsValid)
            {
                auction.Auctioneer = "wne";
                auction.Expires = auction.Posted.AddDays(7);
                service.Create(auction);
                return RedirectToAction("Index");
            }
            return View(auction);
        }

   

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
