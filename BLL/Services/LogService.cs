using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuctionCore.DAL.Repositories;
using AuctionCore.Models.AuctionModel;

namespace AuctionCore.BLL.Services
{
    public class LogService
    {
        private readonly AuctionRepository _repo;

        public LogService() =>
            _repo = new AuctionRepository();

        public List<Auction> Get() =>
            _repo.Get().FindAll(auction => auction.Expires < DateTime.UtcNow || auction.Bids.Find(b => b.IsBuyout) != null);

        public Auction Get(string id)
        {
            Auction auction = _repo.Get(id);
            if (auction.Expires < DateTime.UtcNow || auction.Bids.Find(b => b.IsBuyout) != null)
                return auction;
            return null; 
        }

        public bool Exists(string id, out Auction auction)
        {
            auction = Get(id);
            return auction != null;
        }
    }
}
