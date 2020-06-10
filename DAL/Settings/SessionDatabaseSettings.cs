using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionCore.DAL.Settings
{
    public class SessionDatabaseSettings : ISessionDatabaseSettings
    {
        public string Hostname { get; set; }

        public int Port { get; set; }

        public TimeSpan Expires { get; set; } 
    }

    public interface ISessionDatabaseSettings
    {
        string Hostname { get; set; }

        int Port { get; set; }

        TimeSpan Expires { get; set; }
    }
}
