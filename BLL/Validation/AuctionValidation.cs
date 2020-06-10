using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuctionCore.Models.AuctionModel; 

namespace AuctionCore.BLL.Validation
{
    public static class AuctionValidation
    {
        public static bool ValidateBid(Bid bid, Auction auction)
        {
            if (auction.BuyoutPrice != null)
                return bid.Amount >= auction.StartingPrice && bid.Amount < auction.BuyoutPrice &&
                bid.Amount > auction.Bids.Max().Amount && bid.Bidder != null;
            else
                return bid.Amount >= auction.StartingPrice && bid.Amount > auction.Bids.Max().Amount && bid.Bidder != null;
        }
    }
}
