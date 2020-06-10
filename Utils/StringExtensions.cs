using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionCore.Utils
{
    public static class StringExtensions
    {
        private static readonly List<char> legal = new List<char>
        {
            'A', 'a', 'B', 'b', 'C', 'c', 'D', 'd', 'E', 'e', 'F', 'f', 'G', 'g', 'H', 'h', 'I', 'i', 'J', 'j',
            'K', 'k', 'L', 'l', 'M', 'm', 'N', 'n', 'O', 'o', 'P', 'p', 'Q', 'q', 'R', 'r', 'S', 's', 'T', 't',
            'U', 'u', 'V', 'v', 'W', 'w', 'X', 'x', 'Y', 'y', 'Z', 'z'
        };

        public static string StripNonLatin(this string str)
        {
            StringBuilder builder = new StringBuilder();

            foreach (char c in str)
            {
                if (legal.Contains(c))
                    builder.Append(c); 
            }
            return builder.ToString();
        }
    }
}
