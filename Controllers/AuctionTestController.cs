using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AuctionCore.BLL.Services;
using AuctionCore.Models.AuctionModel;
using AuctionCore.DAL.Repositories;
using AuctionCore.Utils;

namespace AuctionCore.Controllers
{
    public class AuctionTestController : BaseController
    {
        private readonly AuctionRepository repo = new AuctionRepository();

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("/Test/Auction/Get")]
        public ActionResult<List<Auction>> Get()
        {

            return Content("Not Authorized");
        }

        [HttpGet("id:length(24)", Name = "GetAuction")]
        public ActionResult<Auction> GetAuction(string id)
        {
            var auction = repo.Get(id);

            if (auction == null)
            {
                return NotFound();
            }

            return auction;
        }

        [HttpGet]
        public ActionResult<Auction> Create()
        {
            Auction auction = CreateTestAuction();

            repo.Create(auction);

            return CreatedAtRoute("GetAuction", new { id = auction.Id.ToString() }, auction);
        }

        private Auction CreateTestAuction()
        {
            return new Auction
            {
                Item = new Item
                {
                    Name = "Added Auction Name",
                    Category = new Category
                    {
                        Main = "Auction Main Category"
                    },
                    Description = "This Auction Yada, Yada, Yada..."
                },
                StartingPrice = new Decimal(125.5),
                Posted = DateTime.Now,
                Expires = DateTime.Now.AddDays(7),
                Auctioneer = "wne"
            };
        }

        public ActionResult<Auction> Push()
        {
            string id = "5eb063058b39e12cccbe8fb7";
            Auction auction = repo.Get(id);

            auction.Bids.Add(new Bid
            {
                Amount = new decimal(175.5), 
                Timestamp = DateTime.Now.AddMinutes(5), 
                Bidder = "thisUser"
            }); ;

            repo.Update(id, auction);
            return CreatedAtRoute("GetAuction", new { id = auction.Id.ToString() }, auction);
        }
    }
}