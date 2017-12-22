using System;
using System.Collections.Generic;

namespace WhaleIsland.Trpg.GM.Common.Redis
{
    /// <summary>
    /// Server redis info
    /// </summary>
    public class RedisInfo
    {
        /// <summary>
        ///
        /// </summary>
        public RedisInfo()
        {
            ClientVersion = RedisStorageVersion.Hash;
            SlaveSet = new Dictionary<string, RedisInfo>();
        }

        /// <summary>
        /// Server info hash
        /// </summary>
        public string HashCode { get; set; }

        /// <summary>
        /// Redis client version
        /// </summary>
        public RedisStorageVersion ClientVersion { get; set; }

        /// <summary>
        /// 使用Redis的服务器主机
        /// </summary>
        public string ServerHost { get; set; }

        /// <summary>
        /// 使用Redis的服务器所在物理路径
        /// </summary>
        public string ServerPath { get; set; }

        /// <summary>
        /// 使用Redis的数据序列化类型
        /// </summary>
        public string SerializerType { get; set; }

        /// <summary>
        /// 使用Redis的服务器启动时间
        /// </summary>
        public DateTime StarTime { get; set; }

        /// <summary>
        /// 使用Redis的服务器的子群集服务器
        /// </summary>
        public Dictionary<string, RedisInfo> SlaveSet { get; set; }

    }
}
