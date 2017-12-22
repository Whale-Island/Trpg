using System.Collections.Generic;
using WhaleIsland.Trpg.GM.Common.Model;
using WhaleIsland.Trpg.GM.Common.Net.Redis;
using WhaleIsland.Trpg.GM.Net;

namespace WhaleIsland.Trpg.GM.Common.Net
{
    /// <summary>
    ///
    /// </summary>
    public class RedisTransponder : ITransponder
    {
        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="receiveParam"></param>
        /// <param name="dataList"></param>
        /// <returns></returns>
        public bool TryReceiveData<T>(TransReceiveParam receiveParam, out List<T> dataList) where T : AbstractEntity, new()
        {

            using (IDataReceiver getter = new RedisDataGetter(receiveParam.RedisKey, receiveParam.Schema))
            {
                return getter.TryReceive<T>(out dataList);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataList"></param>
        /// <param name="sendParam"></param>
        public bool SendData<T>(T[] dataList, TransSendParam sendParam) where T : AbstractEntity, new()
        {
            using (IDataSender sender = new RedisDataSender(sendParam))
            {
                return sender.Send(dataList);
            }
        }
    }
}
