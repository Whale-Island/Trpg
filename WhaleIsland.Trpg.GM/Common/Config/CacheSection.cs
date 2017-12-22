using WhaleIsland.Trpg.GM.Common.Configuration;

namespace WhaleIsland.Trpg.GM.Common.Config
{
    /// <summary>
    ///
    /// </summary>
    public class CacheSection : ConfigSection
    {
        /// <summary>
        ///
        /// </summary>
        public CacheSection()
        {
            UpdateInterval = ConfigUtils.GetSetting("Cache.update.interval", 600); //10 Minute
            ExpiredInterval = ConfigUtils.GetSetting("Cache.expired.interval", 600);
            IsStorageToDb = ConfigUtils.GetSetting("Cache.IsStorageToDb", false);
            SerializerType = ConfigUtils.GetSetting("Cache.Serializer", "Protobuf");
            ShareExpirePeriod = ConfigUtils.GetSetting("Cache.global.period", 3 * 86400); //72 hour
            PersonalExpirePeriod = ConfigUtils.GetSetting("Cache.user.period", 86400); //24 hour
        }

        /// <summary>
        /// The cache expiry interval.
        /// </summary>
        public int ExpiredInterval { get; set; }

        /// <summary>
        /// The cache update interval.
        /// </summary>
        public int UpdateInterval { get; set; }

        /// <summary>
        /// Redis data is storage to Db.
        /// </summary>
        public bool IsStorageToDb { get; set; }

        /// <summary>
        /// cache serialize to redis's type, protobuf or json
        /// </summary>
        public string SerializerType { get; set; }

        /// <summary>
        /// Personal cache expire period, default 24h
        /// </summary>
        public int PersonalExpirePeriod { get; set; }

        /// <summary>
        /// cache expire period
        /// </summary>
        public int ShareExpirePeriod { get; set; }

    }

}
