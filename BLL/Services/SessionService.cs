using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuctionCore.Adapters;
using AuctionCore.DAL.Repositories;
using AuctionCore.Models;
using StackExchange.Redis;
using AuctionCore.DAL.Settings;
using Microsoft.AspNetCore.Http;

namespace AuctionCore.BLL.Services
{
    public class SessionService
    {
        private static readonly ISessionDatabaseSettings settings = new SessionDatabaseSettings
        {
            Hostname = "192.168.99.100",
            Port = 6379,
            Expires = TimeSpan.FromSeconds(1800)
        };
        private static readonly SessionRepository _repo = new SessionRepository(settings);

        public Session Get(string key) =>
            _repo.Get(key).FromHashEntry<Session>();

        public Session Set(string key, Session session) =>
            _repo.Set(key, session.ToHashEntry()).FromHashEntry<Session>();

        public Session Set(Session session) =>
            _repo.Set(session.Key, session.ToHashEntry()).FromHashEntry<Session>();

        public bool Del(string key, string field) =>
            _repo.Del(key, new RedisValue(field));

        public bool Del(string key) =>
            _repo.Del(key);

        public bool Del(Session session) =>
            _repo.Del(session.Key);

        public bool Exists(string key) =>
            _repo.Exists(key);

        public bool Exists(string key, out Session outVar)
        {
            if (_repo.Exists(key))
            {
                outVar = _repo.Get(key).FromHashEntry<Session>();
                return true;
            }
            outVar = null;
            return false;
        }

        public bool Exists(HttpContext context, string cookieName, out Session outVar)
        {
            if (context.Request.Cookies.ContainsKey(cookieName) &&
                _repo.Exists(context.Request.Cookies[cookieName] as string))
            {
                outVar = _repo.Get(context.Request.Cookies[cookieName] as string).FromHashEntry<Session>();
                return true;
            }
            outVar = null;
            return false;
        }

        public int GetTTL(string key) =>
            (int)Math.Round(_repo.GetTimeToLive(key).Value.TotalSeconds);

        public int GetExpires() =>
            (int)Math.Round(settings.Expires.TotalSeconds);

        public void Flush() =>
            _repo.Flush();

        public Session Init(string key) =>
            _repo.Set(key, new Session { Key = key }.ToHashEntry()).FromHashEntry<Session>();

    }
}
