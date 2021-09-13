using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AuctionCore.Models.Ajax
{
    public class Response
    {
        public string Status { get; set; }

        public int StatusCode { get; set; }

        public object Data { get; set; }

        public override string ToString() =>
            JsonConvert.SerializeObject(this, Formatting.Indented);

        public string ToJSON() =>
            ToString();
    }
}
