using AuctionCore.Models.Auction;
using System.Collections.Generic;
using System.Linq;

namespace AuctionCore.Utils.Extensions
{
    public static class AuctionEnumerableExtensions
    {
        public static IEnumerable<Auction> MyOrderBy(
            this IEnumerable<Auction> auctions, string propertyName, bool asc = true)
        {
            if (asc)
            {
                switch (propertyName.ToLower())
                {
                    case "name":
                        return auctions.OrderBy(a => a.Item.Name).ToList();
                    case "expires":
                        return auctions.OrderBy(a => a.Expires).ToList();
                    case "startingprice":
                        return auctions.OrderBy(a => a.StartingPrice).ToList();
                    case "currentbid":
                        return auctions.OrderBy(a => a.Bids.Max(bid => (decimal?)bid.Amount)).ToList();
                    case "buyoutprice":
                        return auctions.OrderBy(a => a.BuyoutPrice).ToList();
                    case "soldby":
                        return auctions.OrderBy(a => a.Auctioneer).ToList();
                }
            }
            else
            {
                switch (propertyName.ToLower())
                {
                    case "name":
                        return auctions.OrderByDescending(a => a.Item.Name).ToList();
                    case "expires":
                        return auctions.OrderByDescending(a => a.Expires).ToList();
                    case "startingprice":
                        return auctions.OrderByDescending(a => a.StartingPrice).ToList();
                    case "currentbid":
                        return auctions.OrderByDescending(a => a.Bids.Max(bid => (decimal?)bid.Amount)).ToList();
                    case "buyoutprice":
                        return auctions.OrderByDescending(a => a.BuyoutPrice).ToList();
                    case "soldby":
                        return auctions.OrderByDescending(a => a.Auctioneer).ToList();
                }
            }
            return auctions;
        }

        public static IEnumerable<Auction> MyRange(this List<Auction> auctions, double lower, double upper, string rangeBy)
        {
            switch (rangeBy.ToLower())
            {
                case "startingprice":
                    return auctions.FindAll(a => a.StartingPrice >= new decimal(lower) && a.StartingPrice <= new decimal(upper));
                case "currentbid":
                    return auctions.FindAll(a => a.Bids.Max(bid => (decimal?)bid.Amount) >= new decimal(lower) && a.Bids.Max(bid => (decimal?)bid.Amount) <= new decimal(upper));
                case "buyoutprice":
                    return auctions.FindAll(a => a.BuyoutPrice >= new decimal(lower) && a.BuyoutPrice <= new decimal(upper));
            }

            return auctions;
        }
    }
}
