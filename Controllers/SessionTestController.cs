using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AuctionCore.Adapters;
using AuctionCore.Models;
using AuctionCore.Utils;
using AuctionCore.BLL.Services;
using System.Text;
using AuctionCore.Models.CategoryDetailsModel;
using Microsoft.AspNetCore.Http;


namespace AuctionCore.Controllers
{
    public class SessionTestController : BaseController
    {
        private readonly SessionService service = new SessionService();

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("/Test/Get/{id:length(44)}", Name = "GetSession")]
        public ActionResult<Session> GetSession(string id)
        {
            Session session = null;

            if (session == null)
            {
                return NotFound();
            }

            return session;
        }

        [HttpGet("/Test/Create")]
        public ActionResult<Session> Create()
        {
            string sessionId = Session.GenerateGUID();
            var session = new Session
            { 
                Active = 0, 
                CurrentPage = "/TestSession"
            };

            service.Set(sessionId, session);

            return CreatedAtRoute("GetSession", new { id = sessionId }, session);
        }
    }
}