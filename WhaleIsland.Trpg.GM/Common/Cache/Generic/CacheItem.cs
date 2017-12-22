using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhaleIsland.Trpg.GM.Common.Event;

namespace WhaleIsland.Trpg.GM.Common.Cache.Generic
{
    /// <summary>
    /// 缓存项对象
    /// </summary>
    public class CacheItem : EntityChangeEvent, IDataExpired
    {
        /// <summary>
        ///
        /// </summary>
        public CacheItem()
            : this(null, false)
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="item"></param>
        /// <param name="isReadOnly"></param>
        public CacheItem(object item, bool isReadOnly)
            : base(isReadOnly)
        {
            Item = item;
        }

        /// <summary>
        /// 移除过期键
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool RemoveExpired(string key)
        {
            return false;
        }

        /// <summary>
        /// 缓存项
        /// </summary>
        public object Item
        {
            get;
            set;
        }
    }

}
