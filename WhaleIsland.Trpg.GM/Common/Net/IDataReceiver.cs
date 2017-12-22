using System;
using System.Collections.Generic;
using WhaleIsland.Trpg.GM.Common.Model;

namespace WhaleIsland.Trpg.GM.Common.Net
{
    /// <summary>
    /// 设置对象属性方法委托
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="column"></param>
    /// <param name="fieldValue"></param>
    public delegate void EntityPropertySetFunc<T>(T entity, SchemaColumn column, object fieldValue) where T : new();

    /// <summary>
    /// 数据接收处理接口
    /// </summary>
    public interface IDataReceiver : IDisposable
    {
        /// <summary>
        /// 尝试接收数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>返回数据结果</returns>
        bool TryReceive<T>(out List<T> dataList) where T : ISqlEntity, new();
    }
}
