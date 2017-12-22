using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace WhaleIsland.Trpg.GM.Common.Collection.Generic
{
    /// <summary>
    /// 分组List
    /// </summary>
    /// <typeparam name="K"></typeparam>
    /// <typeparam name="V"></typeparam>
    public class GroupList<K, V>
        where V : new()
    {
        private readonly ConcurrentDictionary<K, List<V>> _group = new ConcurrentDictionary<K, List<V>>();

        /// <summary>
        ///
        /// </summary>
        public ICollection<K> Keys
        {
            get
            {
                return _group.Keys;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool TryAdd(K key, V value)
        {

            Func<K, List<V>> func = (k) => new List<V>();
            var list = _group.GetOrAdd(key, func);
            list.Add(value);
            return true;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="key"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool TryGetValue(K key, out List<V> list)
        {
            return _group.TryGetValue(key, out list);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="key"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool TryRemove(K key, out List<V> list)
        {
            return _group.TryRemove(key, out list);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public KeyValuePair<K, List<V>>[] ToArray()
        {
            return _group.ToArray();
        }
    }
}
