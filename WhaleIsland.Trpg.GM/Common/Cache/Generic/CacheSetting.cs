using WhaleIsland.Trpg.GM.Common.Config;
using WhaleIsland.Trpg.GM.Common.Configuration;
using WhaleIsland.Trpg.GM.Common.Event;
using WhaleIsland.Trpg.GM.Common.Model;

namespace WhaleIsland.Trpg.GM.Common.Cache.Generic
{
    /// <summary>
    /// The cache setting info.
    /// </summary>
    public class CacheSetting
    {

        private CacheSection _cacheConfig;

        /// <summary>
        /// The cache setting init.
        /// </summary>
        public CacheSetting()
        {
            AutoRunEvent = true;
            _cacheConfig = ConfigManager.Configger.GetFirstOrAddConfig<CacheSection>();
        }

        /// <summary>
        /// is auto run listen event.
        /// </summary>
        public bool AutoRunEvent { get; set; }

        /// <summary>
        /// The cache expiry interval.
        /// </summary>
        public int ExpiredInterval
        {
            get { return _cacheConfig.ExpiredInterval; }
            set { _cacheConfig.ExpiredInterval = value; }
        }

        /// <summary>
        /// The cache update interval.
        /// </summary>
        public int UpdateInterval
        {
            get { return _cacheConfig.UpdateInterval; }
            set { _cacheConfig.UpdateInterval = value; }
        }

        /// <summary>
        /// Redis data is storage to Db.
        /// </summary>
        public bool IsStorageToDb
        {
            get { return _cacheConfig.IsStorageToDb; }
            set { _cacheConfig.IsStorageToDb = value; }
        }

        /// <summary>
        /// The entity has be changed event notify.
        /// </summary>
        public event EntityChangedNotifyEvent ChangedHandle;

        internal void OnChangedNotify(AbstractEntity sender, CacheItemEventArgs e)
        {
            if (ChangedHandle != null)
            {
                ChangedHandle(sender, e);
            }
        }
    }

}
