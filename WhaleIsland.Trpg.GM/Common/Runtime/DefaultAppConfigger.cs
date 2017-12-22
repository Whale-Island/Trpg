using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using WhaleIsland.Trpg.GM.Common.Configuration;

namespace WhaleIsland.Trpg.GM.Common.Runtime
{
    /// <summary>
    ///
    /// </summary>
    public class DefaultAppConfigger : DefaultDataConfigger
    {
        /// <summary>
        /// init
        /// </summary>
        public DefaultAppConfigger()
        {
            ConfigFile = Path.Combine(MathUtils.RuntimePath, "GameServer.exe.config");
        }

        /// <summary>
        ///
        /// </summary>
        protected override void LoadConfigData()
        {
            ExeConfigurationFileMap configFileMap = new ExeConfigurationFileMap();
            configFileMap.ExeConfigFilename = ConfigFile;
            var config = ConfigurationManager.OpenMappedExeConfiguration(configFileMap, ConfigurationUserLevel.None);
            ConfigurationManager.RefreshSection("appSettings");
            ConfigurationManager.RefreshSection("connectionStrings");
            var er = config.ConnectionStrings.ConnectionStrings.GetEnumerator();
            while (er.MoveNext())
            {
                var connSetting = er.Current as ConnectionStringSettings;
                if (connSetting == null) continue;
                AddNodeData(new ConnectionSection(connSetting.Name, connSetting.ProviderName, connSetting.ConnectionString));
            }
            er = config.AppSettings.CurrentConfiguration.AppSettings.Settings.GetEnumerator();
            while (er.MoveNext())
            {
                var kv = er.Current as KeyValueConfigurationElement;
                ConfigurationManager.AppSettings.Set(kv.Key, kv.Value);
            }
            var setting = GameEnvironment.Setting;
            setting.Reset();
            base.LoadConfigData();
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class AppServerConfigger : DataConfigger
    {
        /// <summary>
        /// init
        /// </summary>
        public AppServerConfigger()
        {
            ConfigFile = Path.Combine(MathUtils.RuntimePath, "AppServer.config");
        }

        /// <summary>
        ///
        /// </summary>
        protected override void LoadConfigData()
        {

        }
    }

}
