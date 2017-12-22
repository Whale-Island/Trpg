using WhaleIsland.Trpg.GM.Common.Cache.Generic;
using WhaleIsland.Trpg.GM.Common.Model;
using WhaleIsland.Trpg.GM.Common.Timing;

namespace WhaleIsland.Trpg.GM.Common.Cache
{
    /// <summary>
    /// 上下文缓存集
    /// </summary>
    public class ContextCacheSet<T> : BaseDisposable where T : CacheItem, new()
    {
        private string _cacheKey;
        private EntityContainer<T> _container;

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="cacheKey">Cache key.</param>
        public ContextCacheSet(string cacheKey)
        {
            _container = CacheFactory.GetOrCreate<T>();
            _cacheKey = cacheKey;
        }

        /// <summary>
        /// Determines whether this instance is exist key the specified keys.
        /// </summary>
        /// <returns><c>true</c> if this instance is exist key the specified keys; otherwise, <c>false</c>.</returns>
        /// <param name="keys">Keys.</param>
        public bool IsExistKey(params object[] keys)
        {
            BaseCollection collection;
            LoadingStatus loadStatus;
            if (_container.TryGetGroup(_cacheKey, out collection, out loadStatus))
            {
                T data;
                return collection.TryGetValue(BaseEntity.CreateKeyCode(keys), out data);
            }
            return false;
        }

        /// <summary>
        /// Tries the add.
        /// </summary>
        /// <returns><c>true</c>, if add was tryed, <c>false</c> otherwise.</returns>
        /// <param name="key">Key.</param>
        /// <param name="t">T.</param>
        public bool TryAdd(string key, T t)
        {
            BaseCollection collection;
            LoadingStatus loadStatus;
            if (_container.TryGetGroup(_cacheKey, out collection, out loadStatus))
            {
                return collection.TryAdd(key, t);
            }
            return false;
        }

        /// <summary>
        /// Gets the with the specified key.
        /// </summary>
        /// <param name="key">Key.</param>
        public T this[string key]
        {
            get
            {
                BaseCollection collection;
                LoadingStatus loadStatus;
                if (_container.TryGetGroup(_cacheKey, out collection, out loadStatus))
                {
                    T data;
                    collection.TryGetValue(key, out data);
                    return data;
                }
                return default(T);
            }
        }

        /// <summary>
        /// Clear this instance.
        /// </summary>
        public void Clear()
        {
            _container.TryRemove(_cacheKey, item => true);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _cacheKey = null;
                _container = null;
            }
            base.Dispose(disposing);
        }

    }
}
