using WhaleIsland.Trpg.GM.Common.Configuration;

namespace WhaleIsland.Trpg.GM.Common.Config
{
    /// <summary>
    ///
    /// </summary>
    public class MessageQueueSection : ConfigSection
    {
        /// <summary>
        ///
        /// </summary>
        public MessageQueueSection()
        {
            SlaveMessageQueue = ConfigUtils.GetSetting("Slave.MessageQueue", "");
            EnableRedisQueue = ConfigUtils.GetSetting("Cache.enable.redisqueue", true);
            EnableWriteToDb = ConfigUtils.GetSetting("Cache.enable.writetoDb", true);
            DataSyncQueueNum = ConfigUtils.GetSetting("DataSyncQueueNum", 2);
            SqlWaitSyncQueueNum = ConfigUtils.GetSetting("SqlWaitSyncQueueNum", 2);
            SqlSyncInterval = ConfigUtils.GetSetting("Game.Cache.UpdateDbInterval", 60000);//1 min
            SqlSyncQueueNum = ConfigUtils.GetSetting("SqlSyncQueueNum", 1);
        }

        /// <summary>
        /// Slave message queue name
        /// </summary>
        public string SlaveMessageQueue { get; set; }

        /// <summary>
        /// Enable redis queue
        /// </summary>
        public bool EnableRedisQueue { get; set; }

        /// <summary>
        /// Enable write to Db.
        /// </summary>
        public bool EnableWriteToDb { get; set; }

        /// <summary>
        ///
        /// </summary>
        public int DataSyncQueueNum { get; set; }

        /// <summary>
        ///
        /// </summary>
        public int SqlWaitSyncQueueNum { get; set; }

        /// <summary>
        /// default 5min
        /// </summary>
        public int SqlSyncInterval { get; set; }

        /// <summary>
        ///
        /// </summary>
        public int SqlSyncQueueNum { get; set; }
    }

}
