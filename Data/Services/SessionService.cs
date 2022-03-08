using Microsoft.AspNetCore.Http;
using AuctionCore.Models.Session;
using AuctionCore.Data.Repositories;
using AuctionCore.Adapters;

namespace AuctionCore.Data.Services
{
    public class SessionService : ISessionService
    {
        private readonly SessionRepository _repo;

        public SessionService(SessionRepository repo)
        {
            _repo = repo;
        }

        public Session Get(string key) =>
            _repo.Get(key).FromHashEntry<Session>();

        public Session Insert(string key) =>
            _repo.Set(key, new Session { Key = key }.ToHashEntry()).FromHashEntry<Session>();

        public Session Insert(Session session) =>
            _repo.Set(session.Key, session.ToHashEntry()).FromHashEntry<Session>();

        public Session Update(string key, Session session) =>
            _repo.Set(key, session.ToHashEntry()).FromHashEntry<Session>();

        public Session Update(Session session) =>
            _repo.Set(session.Key, session.ToHashEntry()).FromHashEntry<Session>();

        public bool Delete(string key, string field) =>
            _repo.Del(key, field);

        public bool Delete(string key) =>
            _repo.Del(key);

        public bool Delete(Session session) =>
            _repo.Del(session.Key);

        public void DeleteAll() =>
            _repo.Flush();

        public bool Exists(string key) =>
            _repo.Exists(key);

        public bool Exists(string key, out Session session)
        {
            if (_repo.Exists(key))
            {
                session = _repo.Get(key).FromHashEntry<Session>();
                return true;
            }
            else
            {
                session = null;
                return false;
            }
        }

        public bool Exists(HttpContext context, string cookie, out Session session)
        {
            if (context.Request.Cookies.ContainsKey(cookie) &&
                _repo.Exists(context.Request.Cookies[cookie]))
            {
                session = _repo.Get(context.Request.Cookies[cookie]).FromHashEntry<Session>();
                return true;
            }
            else
            {
                session = null;
                return false;
            }
        }

        public int GetTTL(string key) =>
            _repo.GetTTL(key);

        public int GetExpires() =>
            _repo.GetExpires();

    }
}
