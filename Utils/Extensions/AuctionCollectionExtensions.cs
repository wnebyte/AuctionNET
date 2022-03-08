using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuctionCore.Models.Auction;
using static AuctionCore.Utils.StringExtensions;

namespace AuctionCore.Utils.Extensions
{
    public static class AuctionCollectionExtensions
    {
        public static List<Auction> MyOrderBy(
            this List<Auction> auctions, string propertyName, bool asc)
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

        public static List<Auction> MyRange(this List<Auction> auctions, double lower, double upper, string rangeBy)
        {
            switch (rangeBy.ToLower())
            {
                case "startingprice":
                    return auctions.FindAll(auc => auc.StartingPrice >= new decimal(lower) && auc.StartingPrice <= new decimal(upper));
                case "currentbid":
                    return auctions.FindAll(auc => auc.Bids.Max(bid => (decimal?)bid.Amount) >= new decimal(lower) && auc.Bids.Max(bid => (decimal?)bid.Amount) <= new decimal(upper));
                case "buyoutprice":
                    return auctions.FindAll(auc => auc.BuyoutPrice >= new decimal(lower) && auc.BuyoutPrice <= new decimal(upper));
            }

            return auctions;
        }
    }
}
