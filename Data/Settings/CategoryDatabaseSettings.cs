﻿namespace AuctionCore.Data.Settings
{
    public class CategoryDatabaseSettings : ICategoryDatabaseSettings
    {
        public string ConnectionString { get; set; }

        public string DatabaseName { get; set; }

        public string CollectionName { get; set; }
    }

    public interface ICategoryDatabaseSettings
    {
        string ConnectionString { get; set; }

        string DatabaseName { get; set; }

        string CollectionName { get; set; }
    }
}

