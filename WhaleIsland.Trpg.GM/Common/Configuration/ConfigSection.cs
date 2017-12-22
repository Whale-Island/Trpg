using System.Collections.Generic;
using System.Linq;

namespace WhaleIsland.Trpg.GM.Common.Configuration
{
    /// <summary>
    ///
    /// </summary>
    public class ConfigSection
    {
        private List<ConfigNode> _configNodes;

        /// <summary>
        ///
        /// </summary>
        public ConfigSection()
        {
        }

        /// <summary>
        ///
        /// </summary>
        public ConfigSection(string nodeString)
        {
            Load(nodeString);
        }

        /// <summary>
        ///
        /// </summary>
        public ConfigSection(IEnumerable<ConfigNode> nodes)
        {
            Load(nodes);
        }

        private List<ConfigNode> Nodes
        {
            get { return _configNodes ?? (_configNodes = new List<ConfigNode>()); }
        }

        /// <summary>
        ///
        /// </summary>
        public int NodeCount { get { return _configNodes == null ? 0 : _configNodes.Count; } }

        /// <summary>
        ///
        /// </summary>
        public void Load(string nodeString)
        {
            string[] nodes = nodeString.Split(';');
            foreach (string str in nodes)
            {
                string[] items = str.Split('=');
                if (items.Length == 2)
                {
                    Nodes.Add(new ConfigNode(items[0], items[1]));
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        public void Load(IEnumerable<ConfigNode> nodes)
        {
            Nodes.AddRange(nodes);
        }

        /// <summary>
        ///
        /// </summary>
        public IList<ConfigNode> GetNodes()
        {
            return Nodes.ToList();
        }

        /// <summary>
        ///
        /// </summary>
        public string[] GetKeys()
        {
            return Nodes.Select(t => t.Key).ToArray();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public int GetValue(string key, int defaultValue = 0)
        {
            return GetValue(key, defaultValue.ToString()).ToInt();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public decimal GetValue(string key, decimal defaultValue = 0)
        {
            return GetValue(key, defaultValue.ToString()).ToDecimal();
        }

        /// <summary>
        ///
        /// </summary>
        public string GetValue(string key, string defaultValue)
        {
            var node = Nodes.Find(t => t.Key == key);
            if (node != null)
            {
                return node.Value;
            }
            return defaultValue;
        }

        /// <summary>
        ///
        /// </summary>
        public string[] GetValues()
        {
            return Nodes.Select(t => t.Value).ToArray();
        }

        /// <summary>
        ///
        /// </summary>
        public ConfigNode GetNode(string key)
        {
            return Nodes.Find(t => t.Key == key);
        }

        /// <summary>
        ///
        /// </summary>
        public void AddNode(ConfigNode node)
        {
            Nodes.Add(node);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool SetValue(string key, int value)
        {
            return SetValue(key, value.ToString());
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool SetValue(string key, decimal value)
        {
            return SetValue(key, value.ToString());
        }

        /// <summary>
        ///
        /// </summary>
        public bool SetValue(string key, string value)
        {
            var settingNode = Nodes.Find(t => t.Key == key);
            if (settingNode != null)
            {
                settingNode.Value = value;
                return true;
            }
            AddNode(new ConfigNode(key, value));
            return true;
        }

        /// <summary>
        ///
        /// </summary>
        public bool SetNode(ConfigNode node)
        {
            return SetValue(node.Key, node.Value);
        }

        /// <summary>
        ///
        /// </summary>
        public bool RemoveKey(string key)
        {
            var settingNode = Nodes.Find(t => t.Key == key);
            if (settingNode != null)
            {
                return RemoveNode(settingNode);
            }
            return false;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public bool RemoveNode(ConfigNode node)
        {
            return Nodes.Remove(node);
        }

        /// <summary>
        ///
        /// </summary>
        public void Clear()
        {
            Nodes.Clear();
        }
    }
}
