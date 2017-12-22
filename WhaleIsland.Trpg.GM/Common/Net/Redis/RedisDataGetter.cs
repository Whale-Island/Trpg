using System.Collections.Generic;
using WhaleIsland.Trpg.GM.Common.Model;
using WhaleIsland.Trpg.GM.Common.Redis;

namespace WhaleIsland.Trpg.GM.Common.Net.Redis
{
    internal class RedisDataGetter : IDataReceiver
    {
        private string _redisKey;
        private readonly SchemaTable _table;

        public RedisDataGetter(string redisKey, SchemaTable table)
        {
            _redisKey = redisKey;
            _table = table;
        }

        #region IDataReceiver 成员

        public bool TryReceive<T>(out List<T> dataList) where T : ISqlEntity, new()
        {
            return RedisConnectionPool.TryGetEntity(_redisKey, _table, out dataList);
        }

        public void Dispose()
        {

        }

        #endregion IDataReceiver 成员
    }
}
