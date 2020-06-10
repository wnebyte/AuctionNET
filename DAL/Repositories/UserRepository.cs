using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using AuctionCore.Models.UserModel;
using AuctionCore.DAL.Settings;

namespace AuctionCore.DAL.Repositories
{
    public class UserRepository
    {
        private readonly IMongoCollection<User> _users;
        private readonly IUserDatabaseSettings settings = new UserDatabaseSettings
        {
            ConnectionString = "mongodb://192.168.99.100:27017",
            DatabaseName = "auctionDb",
            CollectionName = "users"
        };

        public UserRepository()
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _users = database.GetCollection<User>(settings.CollectionName);
        }

        public List<User> Get() =>
            _users.Find<User>(user => true).ToList();

        public User Get(string username) =>
            _users.Find<User>(user => user.Username == username).FirstOrDefault();

        public void Insert(User user) =>
            _users.InsertOne(user);

        public void Update(string username, User userIn) =>
            _users.ReplaceOne(user => user.Username == username, userIn);

        public void Delete(string username) =>
            _users.DeleteOne(user => user.Username == username);

        public void Delete(User userOut) =>
            _users.DeleteOne(user => user.Username == userOut.Username);

        public bool Exists(string username) =>
            _users.Find<User>(user => user.Username == username) != null;

        public bool Exists(User user) =>
            _users.Find<User>(u => u.Username == user.Username) != null;

    }
}
