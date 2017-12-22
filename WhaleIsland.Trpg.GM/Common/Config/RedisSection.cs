
using WhaleIsland.Trpg.GM.Common.Configuration;
using WhaleIsland.Trpg.GM.Common.Log;
using WhaleIsland.Trpg.GM.Common.Redis;

namespace WhaleIsland.Trpg.GM.Common.Config
{
    /// <summary>
    ///
    /// </summary>
    public class RedisSection : ConfigSection
    {
        /// <summary>
        ///
        /// </summary>
        public RedisSection()
            : this(true)
        {
        }

        /// <summary>
        ///
        /// </summary>
        public RedisSection(bool useConfig)
        {
            TraceLog.WriteInfo("RedisSection 初始化");
            if (useConfig)
            {
                Host = ConfigUtils.GetSetting("Redis.Host", "localhost");
                MaxWritePoolSize = ConfigUtils.GetSetting("Redis.MaxWritePoolSize", 100);
                MaxReadPoolSize = ConfigUtils.GetSetting("Redis.MaxReadPoolSize", 100);
                ConnectTimeout = ConfigUtils.GetSetting("Redis.ConnectTimeout", 0);
                PoolTimeOut = ConfigUtils.GetSetting("Redis.PoolTimeOut", 300);
                DbIndex = ConfigUtils.GetSetting("Redis.Db", 0);
                ReadOnlyHost = ConfigUtils.GetSetting("Redis.ReadHost", Host);
                Password = ConfigUtils.GetSetting("Redis.Password", string.Empty);
                Port = ConfigUtils.GetSetting("Redis.Port", 6379);
                ClientVersion = ConfigUtils.GetSetting("Redis.ClientVersion", (int)RedisStorageVersion.Hash).ToEnum<RedisStorageVersion>();
            }
            else
            {
                ClientVersion = RedisStorageVersion.Hash;
            }
        }

        /// <summary>
        /// Host, format:password@ip:port
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// ReadOnlyHost
        /// </summary>
        public string ReadOnlyHost { get; set; }

        /// <summary>
        /// MaxWritePoolSize
        /// </summary>
        public int MaxWritePoolSize { get; set; }

        /// <summary>
        /// MaxReadPoolSize
        /// </summary>
        public int MaxReadPoolSize { get; set; }

        /// <summary>
        /// ConnectTimeout(ms)
        /// </summary>
        public int ConnectTimeout { get; set; }

        /// <summary>
        /// Pool timeout release time(s), default 300s
        /// </summary>
        public int PoolTimeOut { get; set; }

        /// <summary>
        /// DbIndex
        /// </summary>
        public int DbIndex { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// ver: 0 is old versin
        /// </summary>
        public RedisStorageVersion ClientVersion { get; set; }
    }
}
