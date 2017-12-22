using System;
using System.Text;

namespace WhaleIsland.Trpg.GM.Common.Serialization
{
    /// <summary>
    ///
    /// </summary>
    public class JsonCacheSerializer : ICacheSerializer
    {
        private readonly Encoding _encoding;

        /// <summary>
        ///
        /// </summary>
        /// <param name="encoding"></param>
        public JsonCacheSerializer(Encoding encoding)
        {
            _encoding = encoding;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public byte[] Serialize(object obj)
        {
            return _encoding.GetBytes(JsonUtils.SerializeCustom(obj));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="data"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public object Deserialize(byte[] data, Type type)
        {
            return JsonUtils.DeserializeCustom(_encoding.GetString(data), type);
        }
    }

}
