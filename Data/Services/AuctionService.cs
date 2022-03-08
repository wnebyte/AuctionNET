using System;
using System.Collections.Generic;
using AuctionCore.Models.Auction;
using AuctionCore.Data.Repositories;

namespace AuctionCore.Data.Services
{
    public class AuctionService : IAuctionService
    {
        private readonly AuctionRepository _repo;

        public AuctionService(AuctionRepository repo)
        {
            _repo = repo;
        }

        public List<Auction> GetAll() =>
            _repo.Get().FindAll(auction => (auction.Expires > DateTime.UtcNow) && !auction.Bids.Exists(bid => bid.IsBuyout));

        public Auction Get(string id)
        {
            Auction auction = _repo.Get(id);
            if ((auction.Expires > DateTime.UtcNow) && (auction.Bids.Find(b => b.IsBuyout) == null))
            {
                return auction;
            }
            else
            {
                return null;
            }
        }

        public bool Insert(Auction auction)
        {
            _repo.Insert(auction);
            return true;
        }

        public bool Update(Auction auction)
        {
            _repo.Update(auction.Id, auction);
            return true;
        }

        public bool Update(string id, Auction auction) 
        {
            _repo.Update(id, auction);
            return true;
        }
    
        public bool Exists(string id, out Auction auction)
        {
            auction = Get(id);
            return auction != null;   
        }

        public bool Delete(Auction auction)
        {
            _repo.Delete(auction);
            return true;
        }

        public Auction Delete(string id)
        {
            Auction auction = _repo.Get(id);
            _repo.Delete(id);
            return auction;
        }
    }
}
