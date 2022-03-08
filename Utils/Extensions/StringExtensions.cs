using System.Text;

namespace AuctionCore.Utils.Extensions
{
    public static class StringExtensions
    {
        public static string StripNonLetters(this string s)
        {
            StringBuilder builder = new StringBuilder();

            foreach (char c in s)
            {
                if (char.IsLetter(c))
                {
                    builder.Append(c);
                }
            }

            return builder.ToString();
        }
    }
}
