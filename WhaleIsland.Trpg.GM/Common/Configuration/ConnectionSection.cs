namespace WhaleIsland.Trpg.GM.Common.Configuration
{
    /// <summary>
    /// Sql connection config
    /// </summary>
    public class ConnectionSection : ConfigSection
    {

        /// <summary>
        ///
        /// </summary>
        public ConnectionSection(string name, string providerName, string connectionString)
        {
            Load(name, providerName, connectionString);
        }

        /// <summary>
        ///
        /// </summary>
        public void Load(string name, string providerName, string connectionString)
        {
            Name = name;
            ProviderName = providerName;
            ConnectionString = connectionString;
        }

        /// <summary>
        ///
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string ProviderName { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string ConnectionString { get; set; }

    }

}
