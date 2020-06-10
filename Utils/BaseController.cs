using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AuctionCore.BLL.Services;
using AuctionCore.Models;
using Microsoft.AspNetCore.Http;

namespace AuctionCore.Utils
{
    public class BaseController : Controller
    {
        private readonly SessionService _service = new SessionService();

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            if (Request.Cookies.ContainsKey("session:id"))

                if (_service.Exists(Request.Cookies["session:id"] as string, out Session session))
                    _service.Set(ProcessOnActionExecuting(session));
                else
                    _service.Set(ProcessOnActionExecuting(_service.Init(Request.Cookies["session:id"] as string)));
            else
            {
                Response.Cookies
                    .Append("session:id", _service.Set(ProcessOnActionExecuting(_service.Init(Session.GenerateGUID()))).Key);
            }
        }
        private Session ProcessOnActionExecuting(Session session)
        {
            session.Active += _service.GetExpires() - _service.GetTTL(session.Key);
            if (session.CurrentPage != HttpContext.Request.Path.Value.ToString())
                session.PrevPage = session.CurrentPage;
            if (Request.Method == HttpMethods.Get)
                session.CurrentPage = HttpContext.Request.Path.Value.ToString();
            return session;
        }
    }
}
