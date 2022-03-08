using System.Collections.Generic;

namespace AuctionCore.Data.Services
{
    public interface IService<K,V> where K : class where V : class
    {
        V Get(K key);

        List<V> GetAll();

        bool Exists(K key, out V entity);

        bool Insert(V entity);

        bool Update(V entity);

        bool Delete(V entity);

    }
}
