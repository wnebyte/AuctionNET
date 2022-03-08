using System;
using System.Threading.Tasks;
using StackExchange.Redis;
using AuctionCore.Data.Settings;

namespace AuctionCore.Data.Repositories
{
    public class SessionRepository : IDisposable
    {
        private readonly ISessionDatabaseSettings _settings;

        private readonly ConnectionMultiplexer _redis;

        private readonly IDatabase _db;

        public SessionRepository(ISessionDatabaseSettings settings)
        {
            _settings = settings;
            _redis = ConnectionMultiplexer.Connect(settings.Hostname + ":" + settings.Port);
            _db = _redis.GetDatabase();
        }

        public HashEntry[] Get(string id) =>
            _db.HashGetAll(id);

        public async Task<HashEntry[]> GetAsync(string id) =>
            await _db.HashGetAllAsync(id);

        public HashEntry[] Set(string id, HashEntry[] hashEntry)
        {
            _db.HashSet(id, hashEntry);
            _db.KeyExpire(id, _settings.Expires);
            return hashEntry;
        }

        public bool Del(string key, string field) =>
            _db.HashDelete(key, new RedisValue(field));

        public bool Del(string id) =>
            _db.KeyDelete(id);

        public bool Exists(string key) =>
            _db.KeyExists(key);

        public int GetTTL(string key) =>
             (int)Math.Round(_db.KeyTimeToLive(key).Value.TotalSeconds);

        public int GetExpires() =>
            (int)Math.Round(_settings.Expires.TotalSeconds);

        public void Flush() =>
            _db.Execute("FLUSHALL");

        public void Dispose()
        {
            _redis.Close();
        }
    }
}