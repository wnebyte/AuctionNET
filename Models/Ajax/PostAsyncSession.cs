using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionCore.Models.Ajax
{
    public class PostAsyncSession
    {
        public List<string> Toggled { get; set; }

        public string Selected { get; set; }

        public string ToggledToString() {
            StringBuilder builder = new StringBuilder();
            Toggled.ForEach(str => builder.Append(str + ";"));
            return builder.ToString();
        }
    }
}
