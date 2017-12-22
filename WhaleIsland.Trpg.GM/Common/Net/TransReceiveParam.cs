using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhaleIsland.Trpg.GM.Common.Model;

namespace WhaleIsland.Trpg.GM.Common.Net
{
    /// <summary>
    /// 转发器接收参数
    /// </summary>
    public class TransReceiveParam
    {
        ///<summary>
        ///</summary>
        ///<param name="redisKey"></param>
        public TransReceiveParam(string redisKey)
        {
            RedisKey = redisKey;
        }

        ///<summary>
        ///</summary>
        ///<param name="redisKey"></param>
        ///<param name="schema"></param>
        ///<param name="dbFilter"></param>
        public TransReceiveParam(string redisKey, SchemaTable schema, DbDataFilter dbFilter)
            : this(redisKey)
        {
            Schema = schema;
            DbFilter = dbFilter;
        }

        ///<summary>
        ///</summary>
        public string RedisKey { get; set; }

        ///<summary>
        ///</summary>
        public SchemaTable Schema { get; set; }

        /////<summary>
        /////</summary>
        //public int Capacity { get; set; }

        ///<summary>
        ///</summary>
        public DbDataFilter DbFilter { get; set; }
    }

}
