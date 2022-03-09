using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using StackExchange.Redis;

namespace AuctionCore.Adapters
{
    public static class HashEntryAdapter
    {
        public static HashEntry[] ToHashEntry(this object obj)
        {
            PropertyInfo[] properties = obj.GetType().GetProperties();
            var list = new List<HashEntry>();

            foreach (PropertyInfo property in properties)
            {
                if (property.GetValue(obj) != null)
                {
                    list.Add(new HashEntry(property.Name, property.GetValue(obj).ToString()));
                }
            }

            return list.ToArray<HashEntry>();

         //   return properties.Select(property => new HashEntry(property.Name, property.GetValue(obj).ToString())).ToArray();
        }

        public static T FromHashEntry<T>(this HashEntry[] hashEntries)
        {
            PropertyInfo[] properties = typeof(T).GetProperties();
            object obj = Activator.CreateInstance(typeof(T));

            foreach (PropertyInfo property in properties)
            {
                HashEntry hashEntry = hashEntries.FirstOrDefault(entry => entry.Name.ToString().Equals(property.Name));
				if (hashEntry.Equals(new HashEntry()))
				{
					continue;
				}
                property.SetValue(obj, Convert.ChangeType(hashEntry.Value.ToString(), property.PropertyType));
            }

            return (T)obj;
        }
    }
}
