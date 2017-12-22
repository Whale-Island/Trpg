using WhaleIsland.Trpg.GM.Common.Model;
using WhaleIsland.Trpg.GM.Common.Redis;
using WhaleIsland.Trpg.GM.Net;

namespace WhaleIsland.Trpg.GM.Common.Net.Redis
{
    /// <summary>
    /// 储存格式：
    /// </summary>
    internal class RedisDataSender : IDataSender
    {
        private readonly TransSendParam _sendParam;

        public RedisDataSender(TransSendParam sendParam)
        {
            _sendParam = sendParam;
        }

        #region IDataSender 成员

        public bool Send<T>(params T[] dataList) where T : AbstractEntity
        {
            if (_sendParam.Schema.CacheType == CacheType.Rank)
            {
                return RedisConnectionPool.TryUpdateRankEntity(_sendParam.Key, dataList);
            }
            return RedisConnectionPool.TryUpdateEntity(dataList);
        }

        public void Dispose()
        {
        }

        #endregion IDataSender 成员

    }
}
