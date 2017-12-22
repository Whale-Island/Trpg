using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhaleIsland.Trpg.GM.Common.Log;

namespace WhaleIsland.Trpg.GM.Common.Serialization
{
    /// <summary>
    /// Json序列化反序列化器
    /// </summary>
    public static class JsonUtils
    {
        /// <summary>
        /// 序列化成中国标准时间,精确值到秒
        /// </summary>
        /// <param name="entity">待序列化的对象</param>
        /// <returns></returns>
        public static string SerializeCustom(object entity)
        {
            return SerializeCustom(entity, "yyyy'-'MM'-'dd' 'HH':'mm':'ss");
        }

        /// <summary>
        /// 序列化成中国标准时间,精确值到秒
        /// </summary>
        /// <param name="entity">待序列化的对象</param>
        /// <param name="formatDate">指定日期格式</param>
        /// <returns></returns>
        public static string SerializeCustom(object entity, string formatDate)
        {
            return Serialize(entity, new IsoDateTimeConverter() { DateTimeFormat = formatDate });
        }

        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="entity">待序列化的对象</param>
        /// <param name="converters">自定转换的处理</param>
        /// <returns></returns>
        public static string Serialize(object entity, params JsonConverter[] converters)
        {
            if (entity == null)
            {
                return string.Empty;
            }
            try
            {
                return JsonConvert.SerializeObject(entity, converters);
            }
            catch (Exception ex)
            {
                TraceLog.WriteError("Serialize object:{0},Error:{1}", entity.GetType().FullName, ex);
                return string.Empty;
            }
        }

        /// <summary>
        /// 自定义日期反序列化
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="entity">待反序列化的对象</param>
        /// <param name="formatDate">指定日期格式</param>
        /// <returns></returns>
        public static T DeserializeCustom<T>(string entity, string formatDate = "yyyy'-'MM'-'dd' 'HH':'mm':'ss")
        {
            return Deserialize<T>(entity, new IsoDateTimeConverter() { DateTimeFormat = formatDate });
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="entity">待反序列化的对象</param>
        /// <param name="converters">自定转换的处理</param>
        /// <returns></returns>
        public static T Deserialize<T>(string entity, params JsonConverter[] converters)
        {
            if (string.IsNullOrEmpty(entity))
            {
                return default(T);
            }
            return JsonConvert.DeserializeObject<T>(entity, converters);
        }

        /// <summary>
        /// 自定义日期反序列化
        /// </summary>
        /// <param name="entity">待反序列化的对象</param>
        /// <param name="type">对象类型</param>
        /// <param name="formatDate">指定日期格式</param>
        /// <returns></returns>
        public static object DeserializeCustom(string entity, Type type, string formatDate = "yyyy'-'MM'-'dd' 'HH':'mm':'ss")
        {
            return Deserialize(entity, type, new IsoDateTimeConverter() { DateTimeFormat = formatDate });
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="entity">待反序列化的对象</param>
        /// <param name="type">对象类型</param>
        /// <param name="converters">自定转换的处理</param>
        /// <returns></returns>
        public static object Deserialize(string entity, Type type, params JsonConverter[] converters)
        {
            if (type == null || string.IsNullOrEmpty(entity))
            {
                return null;
            }
            return JsonConvert.DeserializeObject(entity, type, converters);
        }
    }
}
