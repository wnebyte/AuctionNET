using System;
using Newtonsoft.Json;

namespace AuctionCore.Models.Session
{
    public class Session
    {
        public static string GenerateGUID()
        {
            return "session:" + Guid.NewGuid().ToString();
        }

        public string Key { get; set; }

        public string Username { get; set; }

        public int Active { get; set; }

        public string CurrentPage { get; set; }

        public string PrevPage { get; set; }

        public string Toggled { get; set; }

        public string Selected { get; set; }

        public string ToJSON() =>
            JsonConvert.SerializeObject(this, Formatting.Indented);

        public Session SetUsername(string username)
        {
            Username = username;
            return this;
        }
    }
}
