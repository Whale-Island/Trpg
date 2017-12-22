namespace WhaleIsland.Trpg.GM.Common.Configuration
{
    /// <summary>
    ///
    /// </summary>
    public class ConfigNode
    {
        /// <summary>
        ///
        /// </summary>
        public ConfigNode()
        {

        }

        /// <summary>
        ///
        /// </summary>
        public ConfigNode(string key, string value)
        {
            Key = key;
            Value = value;
        }

        /// <summary>
        ///
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string Value { get; set; }
    }
}
