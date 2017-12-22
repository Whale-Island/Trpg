using System;
using System.Collections.Generic;
using System.Data;

namespace WhaleIsland.Trpg.GM.Common.Data
{
    /// <summary>
    /// DB connection exception
    /// </summary>
    public class DbConnectionException : Exception
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="message"></param>
        public DbConnectionException(string message)
            : base(message)
        {

        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="message"></param>
        /// <param name="error"></param>
        public DbConnectionException(string message, Exception error)
            : base(message, error)
        {

        }

    }

    internal static class DataParameterExtend
    {
        /// <summary>
        /// 获得参数的字段名，不带前缀字符
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public static string GetFieldName(this IDataParameter parameter)
        {
            string fieldName = parameter.ParameterName ?? "";
            if (fieldName.Length > 0)
            {
                return fieldName.Substring(1);
            }
            return fieldName;
        }
    }

    /// <summary>
    /// 提供数据访问基类
    /// </summary>
    public abstract class DbBaseProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DbBaseProvider"/> class.
        /// </summary>
        /// <param name="connectionSetting"></param>
        protected DbBaseProvider(ConnectionSetting connectionSetting)
        {
            this.ConnectionSetting = connectionSetting;
        }

        /// <summary>
        /// 提供者
        /// </summary>
        public string ProviderTypeName
        {
            get { return ConnectionSetting.ProviderTypeName; }
        }

        /// <summary>
        /// 连接数据库字符串
        /// </summary>
        public string ConnectionString
        {
            get
            {
                return ConnectionSetting.ConnectionString;
            }
        }

        /// <summary>
        /// Gets or sets the connection setting.
        /// </summary>
        /// <value>The connection setting.</value>
        public ConnectionSetting ConnectionSetting
        {
            get;
            private set;
        }

        /// <summary>
        /// clear connection pools
        /// </summary>
        public abstract void ClearAllPools();

        /// <summary>
        /// Check connect
        /// </summary>
        /// <returns></returns>
        public abstract void CheckConnect();

        /// <summary>
        /// 执行Sql语句
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public IDataReader ExecuteReader(CommandStruct command)
        {
            command.Parser();
            return ExecuteReader(command.CommandType, command.Sql, command.Parameters);
        }

        /// <summary>
        /// 执行带返回值的sql语句，需要手动关闭reader
        /// </summary>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">sql字符串语句</param>
        /// <param name="parameters">sql的参数</param>
        /// <returns></returns>
        public IDataReader ExecuteReader(CommandType commandType, string commandText, params IDataParameter[] parameters)
        {
            return ExecuteReader(commandType, null, commandText, parameters);
        }

        /// <summary>
        /// 执行带返回值的sql语句，需要手动关闭reader
        /// </summary>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandTimeout">命令执行超时时间</param>
        /// <param name="commandText">sql字符串语句</param>
        /// <param name="parameters">sql的参数</param>
        /// <returns></returns>
        public abstract IDataReader ExecuteReader(CommandType commandType, int? commandTimeout, string commandText, params IDataParameter[] parameters);

        /// <summary>
        /// 执行只返回第一行第一列值的sql语句
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public object ExecuteScalar(CommandStruct command)
        {
            command.Parser();
            return ExecuteScalar(command.CommandType, command.Sql, command.Parameters);
        }

        /// <summary>
        /// 执行只返回第一行第一列值的sql语句
        /// </summary>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">sql字符串语句</param>
        /// <param name="parameters">sql的参数</param>
        /// <returns></returns>
        public object ExecuteScalar(CommandType commandType, string commandText, params IDataParameter[] parameters)
        {
            return ExecuteScalar(commandType, null, commandText, parameters);
        }

        /// <summary>
        /// 执行Sql语句，返回第一行第一列值
        /// </summary>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandTimeout">命令执行超时时间</param>
        /// <param name="commandText">sql字符串语句</param>
        /// <param name="parameters">sql的参数</param>
        /// <returns></returns>
        public abstract object ExecuteScalar(CommandType commandType, int? commandTimeout, string commandText, params IDataParameter[] parameters);

        /// <summary>
        /// 执行返回影响行数的Sql语句（增，删，改）
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public int ExecuteQuery(CommandStruct command)
        {
            command.Parser();
            return ExecuteQuery(command.CommandType, command.Sql, command.Parameters);
        }

        /// <summary>
        /// 执行返回影响行数的Sql语句（增，删，改）
        /// </summary>
        /// <param name="commands">批量命令</param>
        /// <returns></returns>
        public abstract IEnumerable<int> ExecuteQuery(IEnumerable<CommandStruct> commands);

        /// <summary>
        /// 执行返回影响行数的Sql语句（增，删，改）
        /// </summary>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">sql字符串语句</param>
        /// <param name="parameters">sql的参数</param>
        /// <exception cref="DbConnectionException">抛出连接异常</exception>
        /// <returns></returns>
        public int ExecuteQuery(CommandType commandType, string commandText, params IDataParameter[] parameters)
        {
            return ExecuteQuery(commandType, null, commandText, parameters);
        }

        /// <summary>
        /// 执行返回影响行数的Sql语句（增，删，改）
        /// </summary>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandTimeout">命令执行超时时间</param>
        /// <param name="commandText">sql字符串语句</param>
        /// <param name="parameters">sql的参数</param>
        /// <returns></returns>
        public abstract int ExecuteQuery(CommandType commandType, int? commandTimeout, string commandText, params IDataParameter[] parameters);

        /// <summary>
        /// 提交到消息队列中执行返回影响行数的Sql语句（增，删，改），有延迟时间
        /// </summary>
        /// <param name="identityId">消息队列负载标识ID</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">sql字符串语句</param>
        /// <param name="parameters">sql的参数</param>
        /// <returns></returns>
        public int ExecuteNonQuery(long identityId, CommandType commandType, string commandText, params IDataParameter[] parameters)
        {
            return ExecuteNonQuery(identityId, commandType, null, commandText, parameters);
        }

        /// <summary>
        /// 提交到消息队列中执行返回影响行数的Sql语句（增，删，改），有延迟时间
        /// </summary>
        /// <param name="identityId">消息队列负载标识ID</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="tableName">表名</param>
        /// <param name="commandText">sql字符串语句</param>
        /// <param name="parameters">sql的参数</param>
        /// <returns></returns>
        public abstract int ExecuteNonQuery(long identityId, CommandType commandType, string tableName, string commandText, params IDataParameter[] parameters);

        /// <summary>
        /// 生成Sql语句
        /// </summary>
        /// <param name="identityId">消息队列负载标识ID</param>
        /// <param name="command">命令对象</param>
        /// <returns></returns>
        public abstract SqlStatement GenerateSql(long identityId, CommandStruct command);

        /// <summary>
        /// 检查是否有指定表名
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="columns">取出改变的列</param>
        /// <returns></returns>
        public abstract bool CheckTable(string tableName, out DbColumn[] columns);

        /// <summary>
        /// 创建表
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="columns">列对象集合</param>
        /// <returns></returns>
        public abstract void CreateTable(string tableName, DbColumn[] columns);

        /// <summary>
        /// 创建索引
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="indexs">索引列集, 例:"col1,col2"</param>
        public abstract void CreateIndexs(string tableName, string[] indexs);

        /// <summary>
        /// 创建列
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="columns">增加的列集合</param>
        /// <returns></returns>
        public abstract void CreateColumn(string tableName, DbColumn[] columns);

        /// <summary>
        /// 创建参数
        /// </summary>
        /// <param name="paramName">不带@参数名</param>
        /// <param name="value">参数值</param>
        /// <returns></returns>
        public abstract IDataParameter CreateParameter(string paramName, object value);

        /// <summary>
        /// 创建参数
        /// </summary>
        /// <param name="paramName">不带@参数名</param>
        /// <param name="dbType">字段类型</param>
        /// <param name="size">字段大小</param>
        /// <param name="value">字段值</param>
        /// <returns></returns>
        public abstract IDataParameter CreateParameter(string paramName, int dbType, int size, object value);

        /// <summary>
        /// 创建Guid类型的参数
        /// </summary>
        /// <param name="paramName">不带@参数名</param>
        /// <param name="value">字段值</param>
        /// <returns></returns>
        public abstract IDataParameter CreateParameterByGuid(string paramName, object value);

        /// <summary>
        ///
        /// </summary>
        /// <param name="paramName">不带@参数名</param>
        /// <param name="value">字段值</param>
        /// <returns></returns>
        public abstract IDataParameter CreateParameterByLongText(string paramName, object value);

        /// <summary>
        /// 创建Text类型的参数
        /// </summary>
        /// <param name="paramName">不带@参数名</param>
        /// <param name="value">字段值</param>
        /// <returns></returns>
        public abstract IDataParameter CreateParameterByText(string paramName, object value);

        /// <summary>
        /// 创建长blob类型的参数
        /// </summary>
        /// <param name="paramName">不带@参数名</param>
        /// <param name="value">字段值</param>
        /// <returns></returns>
        public abstract IDataParameter CreateParameterLongBlob(string paramName, object value);

        /// <summary>
        /// 创建blob类型的参数
        /// </summary>
        /// <param name="paramName">不带@参数名</param>
        /// <param name="value">字段值</param>
        /// <returns></returns>
        public abstract IDataParameter CreateParameterByBlob(string paramName, object value);

        /// <summary>
        /// 创建CommandStruct对象
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="editType">命令操作类型</param>
        /// <param name="columns">查询列</param>
        /// <returns></returns>
        public abstract CommandStruct CreateCommandStruct(string tableName, CommandMode editType, string columns = "");

        /// <summary>
        /// 创建CommandFilter对象
        /// </summary>
        /// <returns></returns>
        public abstract CommandFilter CreateCommandFilter();

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parameters"></param>
        /// <returns></returns>
        protected T[] ConvertParam<T>(IDataParameter[] parameters) where T : IDataParameter, new()
        {
            if (parameters == null)
            {
                throw new ArgumentNullException("parameters");
            }
            List<T> list = new List<T>();
            foreach (IDataParameter param in parameters)
            {
                if (param != null)
                {
                    list.Add((T)param);
                }
            }
            return list.ToArray();
        }

        /// <summary>
        /// 格式化条件语句中的参数
        /// </summary>
        /// <param name="fieldName">字段名</param>
        /// <param name="compareChar">比较字符,大于、等于、小于等</param>
        /// <param name="paramName">可定制的参数名，空则取字段名</param>
        /// <returns></returns>
        public abstract string FormatFilterParam(string fieldName, string compareChar = "", string paramName = "");

        /// <summary>
        /// 格式化Select语句中的列名
        /// </summary>
        /// <param name="splitChat">列之间的分隔符</param>
        /// <param name="columns">列名集合</param>
        /// <returns></returns>
        public abstract string FormatQueryColumn(string splitChat, ICollection<string> columns);

        /// <summary>
        /// 格式化关键词
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public abstract string FormatName(string name);

    }
}
