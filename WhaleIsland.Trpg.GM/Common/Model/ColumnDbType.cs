namespace WhaleIsland.Trpg.GM.Common.Model
{
    /// <summary>
    /// 数据库列类型
    /// </summary>
    public enum ColumnDbType
    {
        /// <summary>
        /// Guid
        /// </summary>
        UniqueIdentifier = 0,

        /// <summary>
        /// Int
        /// </summary>
        Int,

        /// <summary>
        /// Varchar
        /// </summary>
        Varchar,

        /// <summary>
        /// DateTime
        /// </summary>
        DateTime,

        /// <summary>
        /// Text
        /// </summary>
        Text,

        /// <summary>
        ///
        /// </summary>
        LongText,

        /// <summary>
        ///
        /// </summary>
        Blob,

        /// <summary>
        ///
        /// </summary>
        LongBlob,
    }
}
