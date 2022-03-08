using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using AuctionCore.Models.Auction;
using AuctionCore.Data.Settings;

namespace AuctionCore.Data.Repositories
{
    public class AuctionRepository : IDisposable
    {
        private readonly IMongoCollection<Auction> _collection;

        public AuctionRepository(IAuctionDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _collection = database.GetCollection<Auction>(settings.CollectionName);
        }

        public List<Auction> Get() =>
            _collection.Find(auction => true).ToList();

        public Auction Get(string id) =>
            _collection.Find(auction => auction.Id == id).FirstOrDefault();

        public Auction Insert(Auction auction)
        {
            _collection.InsertOne(auction);
            return auction;
        }

        public void Update(string id, Auction auctionIn) =>
            _collection.ReplaceOne(auction => auction.Id == id, auctionIn);

        public void Delete(Auction auctionOut) =>
            _collection.DeleteOne(auction => auction.Id == auctionOut.Id);

        public void Delete(string id) =>
            _collection.DeleteOne(auction => auction.Id == id);

        public void Dispose()
        {
            
        }
    }
}
