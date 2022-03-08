using System.Linq;
using Microsoft.AspNetCore.Mvc;
using AuctionCore.Models;
using AuctionCore.Models.Auction;
using AuctionCore.Models.Session;
using AuctionCore.Models.Ajax;
using AuctionCore.Data.Services;

namespace AuctionCore.Controllers
{
    public class AjaxController : Controller
    {
        private readonly ISessionService _sessions;

        private readonly IUserService _users;

        private readonly IAuctionService _auctions;

        public AjaxController(
            ISessionService sessions, IUserService users, IAuctionService auctions)
        {
            _sessions = sessions;
            _users = users;
            _auctions = auctions;
        }

        [ValidateAntiForgeryToken]
        [HttpPost("/Ajax/Bid")]
        public ActionResult Bid([FromBody] AsyncBid asyncBid)
        {
            if (_sessions.Exists(HttpContext, "session:id", out Session session))
            {
                if (session.Username != null)
                {
                    if (_auctions.Exists(asyncBid.Id, out Auction auction))
                    {
                        if (!auction.Bids.Any() || auction.Bids.Max(b => b.Amount) < new decimal(asyncBid.Val))
                        {
                            auction.Bids.Add(new Bid
                            {
                                Amount = new decimal(asyncBid.Val),
                                Bidder = session.Username
                            });

                            _auctions.Update(auction);

                            return Content(new AsyncBidResponse { Status = "success", StatusCode = 200, Data = auction }
                            .ToJSON());
                        }

                        return Content(new AsyncBidResponse { Status = "error", StatusCode = 400, Data = auction }
                        .ToJSON());
                    }

                    return Content(new AsyncBidResponse { Status = "error", StatusCode = 404 }
                    .ToJSON());
                }

                return Content(new AsyncBidResponse { Status = "error", StatusCode = 408 }
                .ToJSON()); 
            }

            // return StatusCode(session has expired)
            return StatusCode(412);
        }

        [ValidateAntiForgeryToken]
        [HttpPost("/Ajax/Buyout")]
        public ActionResult Buyout([FromBody] PostAsyncBuyout buyout)
        {
            if (_sessions.Exists(HttpContext, "session:id", out Session session))
            {
                if (session.Username != null)
                {
                    if (_auctions.Exists(buyout.Id, out Auction auction))
                    {
                        auction.Bids.Add(new Bid
                        {
                            Amount = auction.BuyoutPrice.Value, 
                            Bidder = session.Username,
                            IsBuyout = true
                        });

                        //test
                     //   _auctions.Update(auction.Id, auction);

                        return Content(new ResponseAsyncBuyout { Status = "success", StatusCode = 200 }
                        .ToJSON());
                    }

                    return Content(new ResponseAsyncBuyout { Status = "error", StatusCode = 404 }
                    .ToJSON());
                }

                return Content(new ResponseAsyncBuyout { Status = "error", StatusCode = 408 }
                .ToJSON());
            }

            return StatusCode(412); 
        }

        [ValidateAntiForgeryToken]
        [HttpPost("/Ajax/Login")]
        public ActionResult Login([FromBody] UserLogin userLogin)
        {
            if (_sessions.Exists(HttpContext, "session:id", out Session session))
            {
                var user = _users.GetAll().Find(u => u.EmailAddress == userLogin.EmailAddress);

                if (user == null)
                    return StatusCode(417);

                if (userLogin.Password != user.Password)
                    return StatusCode(423);

                _sessions.Update(session.SetUsername(user.Username));

                return Ok();
            }
            return StatusCode(412);
        }

        [ValidateAntiForgeryToken]
        [HttpPost("/Ajax/Logout")]
        public ActionResult Logout()
        {
            if (_sessions.Exists(HttpContext, "session:id", out Session session))
            {
                _sessions.Delete(session.Key, "Username");
                return Ok();
            }

            return StatusCode(412); 
        }

        [HttpGet("/Ajax/GetSession")]
        public ActionResult GetSession()
        {
            if (_sessions.Exists(HttpContext, "session:id", out Session session))
                return Content(session.ToJSON());

            return StatusCode(412);
        }

        [ValidateAntiForgeryToken]
        [HttpPost("/Ajax/PostSession")]
        public ActionResult PostSession([FromBody] PostAsyncSession data)
        {
            if (_sessions.Exists(HttpContext, "session:id", out Session session))
            {
                session.Toggled = data.ToggledToString();
                session.Selected = data.Selected;
                _sessions.Update(session);
                return Ok();
            }

            return StatusCode(412);
        }

        [ValidateAntiForgeryToken]
        [HttpPost("/Ajax/Delete")]
        public IActionResult Delete([FromBody] AsyncDelete asyncDelete)
        {
            /*
            if (_sessions.Exists(this.HttpContext, "session:id", out var session) && session.Username != null)
            {
                var auction = _auctions.Get().Find(auc => auc.Auctioneer == session.Username && auc.Id == id);
                _auctions.Delete(auction);

                return Content(new Response { Status = "success", StatusCode = 200 }.ToJSON());
            }
            return Content(new Response { Status = "error", StatusCode = 412 }.ToJSON());
            */
            return Content(new Response { Status = asyncDelete.Id, StatusCode = 1 }.ToJSON());
        }
    }
}