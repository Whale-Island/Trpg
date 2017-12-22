using System.Collections.Generic;
using WhaleIsland.Trpg.GM.Common.Model;

namespace WhaleIsland.Trpg.GM.Common.Net
{
    /// <summary>
    /// 数据转发器
    /// </summary>
    public interface ITransponder
    {
        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="receiveParam"></param>
        /// <param name="dataList"></param>
        /// <returns></returns>
        bool TryReceiveData<T>(TransReceiveParam receiveParam, out List<T> dataList) where T : AbstractEntity, new();

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataList"></param>
        /// <param name="sendParam"></param>
        bool SendData<T>(T[] dataList, TransSendParam sendParam) where T : AbstractEntity, new();
    }
}
