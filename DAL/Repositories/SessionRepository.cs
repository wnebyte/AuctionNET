using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StackExchange.Redis;
using AuctionCore.DAL.Settings;
using AuctionCore.Models;

namespace AuctionCore.DAL.Repositories
{
    public class SessionRepository
    {
        private readonly IDatabase _db;
        private readonly ISessionDatabaseSettings settings;

        public SessionRepository(ISessionDatabaseSettings settings)
        {
            this.settings = settings;
            var redis = ConnectionMultiplexer.Connect(settings.Hostname + ":" + settings.Port);
            _db = redis.GetDatabase();
        }

        public HashEntry[] Get(string id) =>
            _db.HashGetAll(id);

        public async Task<HashEntry[]> GetAsync(string id) =>
            await _db.HashGetAllAsync(id);

        public HashEntry[] Set(string id, HashEntry[] hashEntry)
        {
            _db.HashSet(id, hashEntry);
            _db.KeyExpire(id, settings.Expires);
            return hashEntry;
        }

        public bool Del(string key, RedisValue redisValue) =>
            _db.HashDelete(key, redisValue);

        public bool Del(string id) =>
            _db.KeyDelete(id);

        public bool Exists(string key) =>
            _db.KeyExists(key);

        public TimeSpan? GetTimeToLive(string key) =>
            _db.KeyTimeToLive(key);

        public void Flush() =>
            _db.Execute("FLUSHALL");

    }
}