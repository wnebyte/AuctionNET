using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuctionCore.DAL.Repositories;
using AuctionCore.Models.AuctionModel;

namespace AuctionCore.BLL.Services
{
    public class AuctionService
    {
        private readonly AuctionRepository _repo;

        public AuctionService() =>
            _repo = new AuctionRepository();

        public void Insert(Auction auction) =>
            _repo.Insert(auction);

        public List<Auction> Get() =>
            _repo.Get().FindAll(auction => auction.Expires > DateTime.UtcNow && !auction.Bids.Exists(bid => bid.IsBuyout));

        public Auction Get(string id)
        {
            Auction auction = _repo.Get(id);
            if (auction.Expires > DateTime.UtcNow && auction.Bids.Find(b => b.IsBuyout) == null)
                return auction;
            return null; 
        }

        public void Update(string id, Auction auctionIn) =>
            _repo.Update(id, auctionIn);

        public bool Exists(string id, out Auction auction)
        {
            auction = Get(id);
            return auction != null;   
        }

        public Auction Delete(Auction auction)
        {
            _repo.Delete(auction);
            return auction; 
        }

        public Auction Delete(string id)
        {
            Auction auction = _repo.Get(id);
            _repo.Delete(id);
            return auction; 
        }
    }
}
