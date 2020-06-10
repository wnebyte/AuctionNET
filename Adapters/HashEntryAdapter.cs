using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StackExchange.Redis;
using System.Reflection;

namespace AuctionCore.Adapters
{
    public static class HashEntryAdapter
    {
        // Serialize to Redis format
        public static HashEntry[] ToHashEntry(this object obj)
        {
            PropertyInfo[] properties = obj.GetType().GetProperties();
            List<HashEntry> list = new List<HashEntry>();

            foreach (var property in properties)
            {
                if (property.GetValue(obj) != null)
                {
                    list.Add(new HashEntry(property.Name, property.GetValue(obj).ToString()));
                }
            }
            return list.ToArray<HashEntry>();

         //   return properties.Select(property => new HashEntry(property.Name, property.GetValue(obj).ToString())).ToArray();
        }

        // Deserialize from Redit format
        public static T FromHashEntry<T>(this HashEntry[] hashEntries)
        {
            PropertyInfo[] properties = typeof(T).GetProperties();
            var obj = Activator.CreateInstance(typeof(T));
            foreach (var property in properties)
            {
                HashEntry entry = hashEntries.FirstOrDefault(g => g.Name.ToString().Equals(property.Name));
                if (entry.Equals(new HashEntry())) continue;
                property.SetValue(obj, Convert.ChangeType(entry.Value.ToString(), property.PropertyType));
            }
            return (T)obj;
        }
    }
}
