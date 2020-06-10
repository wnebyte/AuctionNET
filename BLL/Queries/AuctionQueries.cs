using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuctionCore.Models.AuctionModel;

namespace AuctionCore.BLL.Queries
{
    public static class AuctionQueries
    {

        public static List<Auction> OrderBy(List<Auction> auctions, string propertyName, bool ASC)
        {

            if (ASC)
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

        public static IEnumerable<Auction> SelectCategory(this IEnumerable<Auction> auctions, string category)
        {
            var query = from item in auctions
                        where item.Item.Category.Main == category
                        select item;
            return query;
        }
    }
}
