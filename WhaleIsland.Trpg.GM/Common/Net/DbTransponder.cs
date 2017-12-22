using System.Collections.Generic;
using WhaleIsland.Trpg.GM.Common.Model;
using WhaleIsland.Trpg.GM.Common.Net.Sql;

namespace WhaleIsland.Trpg.GM.Common.Net
{
    /// <summary>
    /// 数据转发器
    /// </summary>
    public class DbTransponder : ITransponder
    {
        /// <summary>
        ///
        /// </summary>
        public DbTransponder()
        {
        }

        /// <summary>
        /// 尝试接收数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="receiveParam"></param>
        /// <param name="dataList"></param>
        /// <returns></returns>
        public bool TryReceiveData<T>(TransReceiveParam receiveParam, out List<T> dataList) where T : AbstractEntity, new()
        {
            using (IDataReceiver getter = new SqlDataReceiver(receiveParam.Schema, receiveParam.DbFilter))
            {
                return getter.TryReceive<T>(out dataList);
            }
        }

        /// <summary>
        /// 发送更新数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataList"></param>
        /// <param name="sendParam"></param>
        public bool SendData<T>(T[] dataList, TransSendParam sendParam) where T : AbstractEntity, new()
        {
            using (var sender = new SqlDataSender(sendParam.IsChange, sendParam.Schema.ConnectKey))
            {
                return sender.Send(dataList);
            }
        }
    }

}
