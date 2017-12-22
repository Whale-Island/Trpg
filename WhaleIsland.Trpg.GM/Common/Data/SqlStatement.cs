using ProtoBuf;
using System;
using System.Data;
using System.Text;

namespace WhaleIsland.Trpg.GM.Common.Data
{
    /// <summary>
    /// Sql语句对象
    /// </summary>
    [ProtoContract, Serializable]
    public class SqlStatement
    {
        /// <summary>
        /// init
        /// </summary>
        public SqlStatement()
        {

        }

        /// <summary>
        /// 标识ID
        /// </summary>
        [ProtoMember(1)]
        public long IdentityID { get; set; }

        /// <summary>
        /// 数据库连接串设置
        /// </summary>
        [ProtoMember(2)]
        public string ConnectionString { get; set; }

        /// <summary>
        /// 数据驱动连接提供者类型
        /// </summary>
        [ProtoMember(3)]
        public string ProviderType { get; set; }

        /// <summary>
        /// 命令类型
        /// </summary>
        [ProtoMember(4)]
        public CommandType CommandType { get; set; }

        /// <summary>
        /// 语句
        /// </summary>
        [ProtoMember(5)]
        public string CommandText { get; set; }

        /// <summary>
        /// 参数
        /// </summary>
        [ProtoMember(6)]
        public SqlParam[] Params { get; set; }

        /// <summary>
        /// 表名
        /// </summary>
        [ProtoMember(7)]
        public string Table { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var sql = new StringBuilder();
            sql.AppendLine(CommandText);
            foreach (var sqlParam in Params)
            {
                sql.AppendLine(string.Format("{0}:{1}", sqlParam.ParamName, sqlParam.Value));
            }
            return sql.ToString();
        }
    }
}
