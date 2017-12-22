using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading;
using WhaleIsland.Trpg.GM.Common.Log;

namespace WhaleIsland.Trpg.GM.Common.Configuration
{
    /// <summary>
    ///
    /// </summary>
    public abstract class DataConfigger : ConfigurationSection, IConfigger
    {
        /// <summary>
        ///
        /// </summary>
        public string ConfigFile { get; set; }

        /// <summary>
        ///
        /// </summary>
        protected bool IsDependenced;

        private FileSystemWatcher _watcher;
        private HashSet<string> _changedFiles = new HashSet<string>();
        private Timer _excuteTimer;
        private readonly List<ConfigSection> _dataList = new List<ConfigSection>();

        /// <summary>
        ///
        /// </summary>
        protected int _dueChangeTime = 500;

        /// <summary>
        ///
        /// </summary>
        public void Install()
        {
            if (!string.IsNullOrEmpty(ConfigFile) && File.Exists(ConfigFile))
            {
                InitDependenceFile();
            }
            LoadConfigData();
        }

        /// <summary>
        ///
        /// </summary>
        protected virtual void InitDependenceFile()
        {
            _excuteTimer = new Timer(OnExcute, null, Timeout.Infinite, Timeout.Infinite);
            string path = Path.GetDirectoryName(ConfigFile) ?? "";
            if (!Directory.Exists(path))
            {
                return;
            }
            string file = Path.GetFileName(ConfigFile) ?? "*.config";
            _watcher = new FileSystemWatcher(path, file);
            _watcher.Changed += new FileSystemEventHandler(OnWatcherChanged);
            _watcher.Created += new FileSystemEventHandler(OnWatcherChanged);
            _watcher.Deleted += new FileSystemEventHandler(OnWatcherChanged);
            _watcher.NotifyFilter = NotifyFilters.CreationTime | NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName | NotifyFilters.Size;
            _watcher.IncludeSubdirectories = false;
            _watcher.EnableRaisingEvents = true;
            IsDependenced = true;
        }

        /// <summary>
        ///
        /// </summary>
        public void Reload()
        {
            lock (_dataList)
            {
                DoClearData();
                LoadConfigData();
            }
            TraceLog.WriteLine("{0} The configger has reloaded.", DateTime.Now.ToString("HH:mm:ss"));
            var e = new ConfigReloadedEventArgs();
            ConfigManager.OnConfigReloaded(this, e);
        }

        /// <summary>
        ///
        /// </summary>
        protected virtual void DoClearData()
        {
            _dataList.Clear();
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetFirstConfig<T>() where T : ConfigSection
        {
            return GetConfig<T>().FirstOrDefault();
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetFirstOrAddConfig<T>() where T : ConfigSection, new()
        {
            var lazy = new Lazy<T>(() => new T());
            return GetFirstOrAddConfig<T>(lazy);
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="createFactory"></param>
        /// <returns></returns>
        public T GetFirstOrAddConfig<T>(Lazy<T> createFactory) where T : ConfigSection, new()
        {
            lock (_dataList)
            {
                T result = _dataList.OfType<T>().FirstOrDefault();
                if (result == null)
                {
                    result = createFactory.Value;
                    AddNodeData(result);
                }
                return result;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IList<T> GetConfig<T>() where T : ConfigSection
        {
            lock (_dataList)
            {
                return _dataList.OfType<T>().ToList();
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public IList<ConfigSection> GetAllConfig()
        {
            lock (_dataList)
            {
                return _dataList.ToList();
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public T GetConnetion<T>(string name) where T : ConnectionSection
        {
            lock (_dataList)
            {
                return _dataList.Where(data => data is T && ((T)data).Name == name).Cast<T>().FirstOrDefault();
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="nodeData"></param>
        protected void AddNodeData(ConfigSection nodeData)
        {
            _dataList.Add(nodeData);
        }

        /// <summary>
        ///
        /// </summary>
        protected abstract void LoadConfigData();

        private void OnWatcherChanged(object sender, FileSystemEventArgs e)
        {
            try
            {
                _changedFiles.Add(e.FullPath);
                _excuteTimer.Change(_dueChangeTime, Timeout.Infinite);
            }
            catch (Exception ex)
            {
                TraceLog.WriteError("XmlDataConfigger changed error:{0}", ex);
            }
        }

        private void OnExcute(object state)
        {
            try
            {
                //Repetitive loading process
                var tempFile = Interlocked.Exchange(ref _changedFiles, new HashSet<string>());

                foreach (var fileName in tempFile)
                {
                    var e = new ConfigChangedEventArgs() { FileName = fileName };
                    ConfigManager.OnConfigChanged(this, e);
                    Reload();
                    break;

                }
                //stop timer
                _excuteTimer.Change(Timeout.Infinite, Timeout.Infinite);
            }
            catch (Exception ex)
            {
                TraceLog.WriteError("XmlDataConfigger excute error:{0}", ex);
            }
        }

        /// <summary>
        ///
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            //释放非托管资源
            if (disposing)
            {
                if (_watcher != null) _watcher.Dispose();
                if (_watcher != null) _excuteTimer.Dispose();
                GC.SuppressFinalize(this);
            }
        }
    }
}
