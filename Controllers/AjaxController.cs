using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AuctionCore.Models;
using AuctionCore.Models.UserModel;
using AuctionCore.Models.AuctionModel;
using AuctionCore.Models.Ajax;
using AuctionCore.BLL.Services;
using Microsoft.AspNetCore.Http;

namespace AuctionCore.Controllers
{
    public class AjaxController : Controller
    {
        private readonly SessionService _sessions;
        private readonly UserService _users;
        private readonly AuctionService _auctions;

        public AjaxController()
        {
            _sessions = new SessionService();
            _users = new UserService();
            _auctions = new AuctionService();
        }

        [ValidateAntiForgeryToken]
        [HttpPost("/Ajax/Bid")]
        public ActionResult Bid([FromBody] AsyncBid asyncBid)
        {
            if (_sessions.Exists(this.HttpContext, "session:id", out Session session))
            {
                if (session.Username != null)
                {
                    if (_auctions.Exists(asyncBid.Id, out Auction auction))
                    {
                        if (auction.Bids.Max(b => b.Amount) < new decimal(asyncBid.Val))
                        {
                            auction.Bids.Add(new Bid
                            {
                                Amount = new decimal(asyncBid.Val),
                                Bidder = session.Username
                            });

                            _auctions.Update(auction.Id, auction);

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
        public ActionResult Buyout([FromBody] PostAsyncBuyout asyncBuyout)
        {
            if (_sessions.Exists(this.HttpContext, "session:id", out Session session))
            {
                if (session.Username != null)
                {
                    if (_auctions.Exists(asyncBuyout.Id, out Auction auction))
                    {
                        auction.Bids.Add(new Bid
                        {
                            Amount = auction.BuyoutPrice.Value, 
                            Bidder = session.Username,
                            IsBuyout = true
                        });

                        _auctions.Update(auction.Id, auction);

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
            if (_sessions.Exists(this.HttpContext, "session:id", out Session session))
            {
                var user = _users.Get().Find(u => u.EmailAddress == userLogin.EmailAddress);

                if (user == null)
                    return StatusCode(417);

                if (userLogin.Password != user.Password)
                    return StatusCode(423);

                _sessions.Set(session.SetUsername(user.Username));

                return Ok();
            }

            return StatusCode(412);
        }

        [ValidateAntiForgeryToken]
        [HttpPost("/Ajax/Logout")]
        public ActionResult Logout()
        {
            if (_sessions.Exists(this.HttpContext, "session:id", out Session session))
            {
                _sessions.Del(session.Key, "Username");
                return Ok();
            }

            return StatusCode(412); 
        }

        [HttpGet("/Ajax/GetSession")]
        public ActionResult GetSession()
        {
            if (_sessions.Exists(this.HttpContext, "session:id", out Session session))
                return Content(session.ToJSON());

            return StatusCode(412);
        }

        [ValidateAntiForgeryToken]
        [HttpPost("/Ajax/PostSession")]
        public ActionResult PostSession([FromBody] PostAsyncSession asyncSession)
        {
            if (_sessions.Exists(this.HttpContext, "session:id", out Session session))
            {
                session.Toggled = asyncSession.ToggledToString();
                session.Selected = asyncSession.Selected;
                _sessions.Set(session);
                return Ok();
            }

            return StatusCode(412);
        }
    }
}