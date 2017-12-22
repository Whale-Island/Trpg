using System;
using System.Collections.Generic;
using WhaleIsland.Trpg.GM.Common.Model;

namespace WhaleIsland.Trpg.GM.Net
{
    /// <summary>
    /// 数据处理句柄,跨服战时，可重新设置KEY主键规则
    /// </summary>
    public delegate object EntityPropertyGetFunc<T>(T entity, SchemaColumn column) where T : ISqlEntity;

    /// <summary>
    /// 过滤提交更新列名的方法委托
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="schema"></param>
    /// <param name="isChange"></param>
    /// <returns></returns>
    public delegate IList<string> EnttiyPostColumnFunc<T>(T entity, SchemaTable schema, bool isChange) where T : ISqlEntity;

    /// <summary>
    /// 数据传送操作接口
    /// </summary>
    public interface IDataSender : IDisposable
    {

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataList"></param>
        bool Send<T>(params T[] dataList) where T : AbstractEntity;

    }
}
