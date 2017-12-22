using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using WhaleIsland.Trpg.GM.Common.Config;
using WhaleIsland.Trpg.GM.Common.Configuration;
using WhaleIsland.Trpg.GM.Common.Log;
using WhaleIsland.Trpg.GM.Common.Serialization;

namespace WhaleIsland.Trpg.GM.Common.Runtime
{
    /// <summary>
    /// The environment configuration information.
    /// </summary>
    [Serializable]
    public class EnvironmentSetting
    {

        static EnvironmentSetting()
        {
            bool result;
            try
            {
                result = ConfigManager.Intialize("appServerConfigger");
            }
            catch (Exception)
            {
                result = false;
            }
            if (!result)
            {
                try
                {
                    ConfigManager.GetConfigger<DefaultAppConfigger>();
                }
                catch (Exception ex)
                {
                    TraceLog.WriteError("Configger init error:{0}", ex);
                }
            }
            LoadDecodeFunc();
        }

        private static string GetLocalIp()
        {
            string localIp = "";
            IPAddress[] addressList = Dns.GetHostEntry(Environment.MachineName).AddressList;
            foreach (var ipAddress in addressList)
            {
                if (ipAddress.AddressFamily == AddressFamily.InterNetwork)
                {
                    localIp = ipAddress.ToString();
                    break;
                }
            }
            return localIp;
        }

        /// <summary>
        /// Object Initialization.
        /// </summary>
        public EnvironmentSetting()
        {
            var cacheSection = GetCacheSection();

            CacheGlobalPeriod = cacheSection.ShareExpirePeriod;
            CacheUserPeriod = cacheSection.PersonalExpirePeriod;

            InitSerializer();
            Reset();
        }

        /// <summary>
        ///
        /// </summary>
        public void Reset()
        {
        }

        private void InitSerializer()
        {
            string type = ConfigManager.Configger.GetFirstOrAddConfig<CacheSection>().SerializerType;
            if (string.Equals(type, "json", StringComparison.OrdinalIgnoreCase))
            {
                Serializer = new JsonCacheSerializer(Encoding.UTF8);
            }
            else
            {
                Serializer = new ProtobufCacheSerializer();
            }
        }

        private static CacheSection GetCacheSection()
        {
            return ConfigManager.Configger.GetFirstOrAddConfig<CacheSection>();
        }

        /// <summary>
        ///
        /// </summary>
        public string RedisHost
        {
            get
            {
                var section = ConfigManager.Configger.GetFirstOrAddConfig<RedisSection>();
                return section.Host;
            }
        }

        private static dynamic _scriptDecodeTarget;

        private static void LoadDecodeFunc()
        {
            string decodeFuncTypeName = "";
            try
            {
                if (string.IsNullOrEmpty(decodeFuncTypeName)) return;
                var type = Type.GetType(decodeFuncTypeName, true, true);
                _scriptDecodeTarget = type.CreateInstance();
            }
            catch (Exception ex)
            {
                TraceLog.WriteError("Load DecodeFunc type error:\"{0}\" {1}", decodeFuncTypeName, ex);
            }
        }

        private static string DecodeCallback(string source, string ext)
        {
            if (_scriptDecodeTarget == null)
                return "";
            return _scriptDecodeTarget.DecodeCallback(source, ext);
        }

        /// <summary>
        /// Global cache lifecycle.
        /// </summary>
        public int CacheGlobalPeriod { get; set; }

        /// <summary>
        /// Game players cache lifecycle.
        /// </summary>
        public int CacheUserPeriod { get; set; }

        /// <summary>
        ///
        /// </summary>
        public ICacheSerializer Serializer { get; set; }


    }

}
