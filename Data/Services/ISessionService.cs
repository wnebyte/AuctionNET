using Microsoft.AspNetCore.Http;
using AuctionCore.Models.Session;

namespace AuctionCore.Data.Services
{
    public interface ISessionService
    {
        Session Get(string key);

        Session Insert(string key);

        Session Insert(Session session);

        Session Update(string key, Session session);

        Session Update(Session session);

        bool Delete(string key, string field);

        bool Delete(string key);

        bool Delete(Session session);

        void DeleteAll();

        bool Exists(string key);

        bool Exists(string key, out Session session);

        bool Exists(HttpContext context, string cookie, out Session session);

        int GetTTL(string key);

        int GetExpires();
    }
}
