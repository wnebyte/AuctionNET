using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using AuctionCore.Models.User;
using AuctionCore.Data.Settings;

namespace AuctionCore.Data.Repositories
{
    public class UserRepository : IDisposable
    {
        private readonly IMongoCollection<User> _users;

        public UserRepository(IUserDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _users = database.GetCollection<User>(settings.CollectionName);
        }

        public List<User> Get() =>
            _users.Find(user => true).ToList();

        public User Get(string username) =>
            _users.Find(user => user.Username == username).FirstOrDefault();

        public void Insert(User user) =>
            _users.InsertOne(user);

        public void Update(string username, User userIn) =>
            _users.ReplaceOne(user => user.Username == username, userIn);

        public void Delete(string username) =>
            _users.DeleteOne(user => user.Username == username);

        public void Delete(User userOut) =>
            _users.DeleteOne(user => user.Username == userOut.Username);

        public bool Exists(string username) =>
            _users.Find(user => user.Username == username) != null;

        public bool Exists(User user) =>
            _users.Find(u => u.Username == user.Username) != null;

        public void Dispose()
        {
            
        }
    }
}
