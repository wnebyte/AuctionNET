using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using AuctionCore.Models.AuctionModel;
using AuctionCore.DAL.Settings;

namespace AuctionCore.DAL.Repositories
{
    public class AuctionRepository
    {
        private readonly IMongoCollection<Auction> _auctions;
        private readonly IAuctionDatabaseSettings settings = new AuctionDatabaseSettings
        {
            ConnectionString = "mongodb://192.168.99.100:27017",
            DatabaseName = "auctionDb",
            CollectionName = "auctions"
        };

        public AuctionRepository()
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _auctions = database.GetCollection<Auction>(settings.CollectionName);
        }

        public List<Auction> Get() =>
            _auctions.Find<Auction>(auction => true).ToList();

        public Auction Get(string id) =>
            _auctions.Find<Auction>(auction => auction.Id == id).FirstOrDefault();

        public Auction Create(Auction auction)
        {
            _auctions.InsertOne(auction);
            return auction;
        }

        public void Update(string id, Auction auctionIn) =>
            _auctions.ReplaceOne(auction => auction.Id == id, auctionIn);

        public void Delete(Auction auctionOut) =>
            _auctions.DeleteOne(auction => auction.Id == auctionOut.Id);

        public void Delete(string id) =>
            _auctions.DeleteOne(auction => auction.Id == id);

    }
}
