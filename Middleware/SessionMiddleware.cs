using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using AuctionCore.Models.Session;
using AuctionCore.Data.Services;

namespace AuctionCore.Middleware
{
    public class SessionMiddleware
    {
        private readonly RequestDelegate _next;

        public SessionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ISessionService service)
        {
            string cookie;

            if (context.Request.Cookies.ContainsKey("session:id"))
            {
                cookie = context.Request.Cookies["session:id"];

                if (service.Exists(cookie, out Session session))
                {
                    service.Update(
						UpdateSession(
							context, 
							service, 
							session)
						);
                }
                else
                {
                    service.Update(
						UpdateSession(
							context, 
							service, 
							service.Insert(cookie))
						);
                }
            }
            else
            {
                cookie = service.Update(
                    UpdateSession(
                        context, 
                        service, 
						service.Insert(Session.GenerateGUID()))
                    ).Key;

                context.Response.Cookies.Append("session:id", cookie);
            }

            await _next(context);
        }

        private Session UpdateSession(HttpContext context, ISessionService service, Session session)
        {
			session.Active += service.GetExpires() - service.GetTTL(session.Key);

            if (context.Request.Method == HttpMethods.Get)
            {
				string path = context.Request.Path.Value;

				if (session.CurrentPage != path)
				{
					session.PrevPage = session.CurrentPage;
					session.CurrentPage = path;
				}
            }

            return session;
        }
    }
}
