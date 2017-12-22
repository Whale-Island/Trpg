
using System.Collections.Specialized;
using System.Configuration;
using WhaleIsland.Trpg.GM.Common.Security;

namespace WhaleIsland.Trpg.GM.Common.Configuration
{
    /// <summary>
    /// 操作Web.config文档
    /// </summary>
    public class ConfigUtils
    {
        /// <summary>
        ///
        /// </summary>
        public static NameValueCollection SettingsCollection
        {
            get
            {
                return ConfigurationManager.AppSettings;
            }
        }

        private ConfigUtils()
        {
        }

        /// <summary>
        /// 读取Config配置文件的Add结点键int配置值
        /// </summary>
        /// <param name="key">键值Key</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static int GetSetting(string key, int defaultValue)
        {
            int result = defaultValue;
            try
            {
                object obj = SettingsCollection[key];
                result = obj == null ? defaultValue : obj.ToInt();
            }
            catch { }
            return result;
        }

        /// <summary>
        /// 读取Config配置文件的Add结点键decimal配置值
        /// </summary>
        /// <param name="key">键值Key</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static decimal GetSetting(string key, decimal defaultValue)
        {
            decimal result = defaultValue;
            try
            {
                object obj = SettingsCollection[key];
                result = obj == null ? defaultValue : obj.ToDecimal();
            }
            catch { }
            return result;
        }

        /// <summary>
        /// 读取Config配置文件的Add结点键bool配置值
        /// </summary>
        /// <param name="key">键值Key</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static bool GetSetting(string key, bool defaultValue)
        {
            bool result = defaultValue;
            try
            {
                object obj = SettingsCollection[key];
                result = obj == null ? defaultValue : obj.ToBool();
            }
            catch { }
            return result;
        }

        /// <summary>
        /// 读取Config配置文件的Add结点键配置值
        /// </summary>
        /// <param name="key">键值Key</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static string GetSetting(string key, string defaultValue)
        {
            string result;
            try
            {
                object obj = SettingsCollection[key];
                result = ((obj == null) ? defaultValue.ToNotNullString() : ((string)obj));
            }
            catch
            {
                result = defaultValue.ToNotNullString();
            }
            return result;
        }

        /// <summary>
        /// 读取Config配置文件的Add结点键值
        /// </summary>
        /// <param name="key">键值Key</param>
        /// <returns></returns>
        public static string GetSetting(string key)
        {
            return GetSetting(key, "");
        }

        /// <summary>
        /// 获取连接字符串
        /// </summary>
        /// <param name="connName">键值Key</param>
        /// <param name="mKey">解密密钥</param>
        /// <returns></returns>
        public static string GetConnectionString(string connName, string mKey)
        {
            string result;
            try
            {
                string text = GetSetting(connName);
                if (!mKey.IsEmpty() && !text.IsEmpty())
                {
                    text = CryptoHelper.DES_Decrypt(text, mKey);
                }
                else if (!text.IsEmpty())
                {
                    text = CryptoHelper.DES_Decrypt(text);
                }
                result = text;
            }
            catch
            {
                result = "";
            }
            return result;
        }

        /// <summary>
        /// 获取连接字符串
        /// </summary>
        /// <param name="connName"></param>
        /// <returns></returns>
        public static string GetConnectionString(string connName)
        {
            return GetConnectionString(connName, string.Empty);
        }
    }
}
