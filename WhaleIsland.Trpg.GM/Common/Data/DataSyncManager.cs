using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhaleIsland.Trpg.GM.Common.Model;
using WhaleIsland.Trpg.GM.Common.Net;
using WhaleIsland.Trpg.GM.Common.Net.Redis;
using WhaleIsland.Trpg.GM.Common.Net.Sql;
using WhaleIsland.Trpg.GM.Net;

namespace WhaleIsland.Trpg.GM.Common.Data
{
    /// <summary>
    /// 数据同步管理类
    /// </summary>
    public static class DataSyncManager
    {
        #region SQL

        /// <summary>
        /// 获取操作数据的Sql发送者接口
        /// </summary>
        /// <param name="isChange">是否包含修改的</param>
        /// <param name="connectKey">指定配置文件中的连接键</param>
        /// <returns></returns>
        public static IDataSender GetDataSender(bool isChange = true, string connectKey = null)
        {
            return new SqlDataSender(isChange, connectKey);
        }

        /// <summary>
        /// 获取操作数据的Sql接收者接口
        /// </summary>
        /// <param name="schema">指定的表结构对象</param>
        /// <param name="filter">操作数据的过滤器</param>
        /// <returns></returns>
        public static IDataReceiver GetDataGetter(SchemaTable schema, DbDataFilter filter)
        {
            return new SqlDataReceiver(schema, filter);
        }

        /// <summary>
        /// 尝试执行查询Sql数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="schema">指定的表结构对象</param>
        /// <param name="filter">操作数据的过滤器</param>
        /// <param name="setFunc">指定数据库字段转换成对象属性方法</param>
        /// <param name="dataList">返回的结果</param>
        /// <returns></returns>
        public static bool TryReceiveSql<T>(SchemaTable schema, DbDataFilter filter, EntityPropertySetFunc<T> setFunc, out List<T> dataList)
            where T : new()
        {
            return new SqlDataReceiver(schema, filter).TryReceive(setFunc, out dataList);
        }

        /// <summary>
        /// 尝试执行查询Sql数据，并使用反射转换成对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="schema">指定的表结构对象</param>
        /// <param name="filter">操作数据的过滤器</param>
        /// <param name="dataList">返回的结果</param>
        /// <returns></returns>
        public static bool TryReceiveSql<T>(SchemaTable schema, DbDataFilter filter, out List<T> dataList)
            where T : ISqlEntity, new()
        {
            return new SqlDataReceiver(schema, filter).TryReceive(out dataList);
        }

        /// <summary>
        /// 发送Sql数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataList">需要更新的对象</param>
        /// <param name="isChange">是否只更新变化的列</param>
        /// <param name="synchronous">是否使用同步的方式更新，而不使用队列</param>
        /// <returns></returns>
        public static bool SendSql<T>(IEnumerable<T> dataList, bool isChange = false, bool synchronous = false)
            where T : ISqlEntity
        {
            return SendSql(dataList, isChange, null, null, synchronous);
        }

        /// <summary>
        /// 发送Sql数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataList">需要更新的对象</param>
        /// <param name="isChange">是否只更新变化的列</param>
        /// <param name="getPropertyFunc">指定对象属性转换成数据库字段方法</param>
        /// <param name="postColumnFunc">指定过滤更新的列方法</param>
        /// <param name="synchronous">是否使用同步的方式更新，而不使用队列</param>
        /// <returns></returns>
        public static bool SendSql<T>(IEnumerable<T> dataList, bool isChange, EntityPropertyGetFunc<T> getPropertyFunc, EnttiyPostColumnFunc<T> postColumnFunc = null, bool synchronous = false)
            where T : ISqlEntity
        {
            return new SqlDataSender(isChange).Send(dataList, getPropertyFunc, postColumnFunc, synchronous);
        }

        #endregion SQL

        #region Redis

        /// <summary>
        /// 获取操作数据的Redis发送者接口
        /// </summary>
        /// <param name="schema">指定的表结构对象</param>
        /// <param name="key">Redis的键值</param>
        /// <returns></returns>
        public static IDataSender GetRedisSender(SchemaTable schema, string key)
        {
            return new RedisDataSender(new TransSendParam(key) { Schema = schema });
        }

        /// <summary>
        /// 获取操作数据的Redis接收者接口
        /// </summary>
        /// <param name="schema">指定的表结构对象</param>
        /// <param name="redisKey">Redis的键值</param>
        /// <returns></returns>
        public static IDataReceiver GetRedisGetter(SchemaTable schema, string redisKey)
        {
            return new RedisDataGetter(redisKey, schema);
        }

        #endregion Redis
    }
}
