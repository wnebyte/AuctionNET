using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuctionCore.Models.AuctionModel;
using Newtonsoft.Json;

namespace AuctionCore.Models.Ajax
{
    public class ResponseAsyncBuyout
    {
        public string Status { get; set; }

        public int StatusCode { get; set; }

        public string StatusText { get; set; }

        public override string ToString() =>
            JsonConvert.SerializeObject(this, Formatting.Indented);

        public string ToJSON() =>
            ToString();
    }
}
