using System.Collections.Generic;

namespace jsmod2
{
    public class HashMap<K,V>
    {
        private Dictionary<K, V> dic;

        public HashMap()
        {
            dic = new Dictionary<K, V>();
        }

        public void put(K k, V v)
        {
            if (!dic.ContainsKey(k))
            {
                dic.Add(k,v);
            }
        }

        public V get(K k)
        {
            return dic[k];
        }

        public HashMap<V,K> keyToValue()
        {
            HashMap<V,K> map = new HashMap<V, K>();
            foreach (var entry in dic)
            {
                map.put(entry.Value,entry.Key);
            }

            return map;
        }
    }
}