namespace WhaleIsland.Trpg.GM.Common.Model
{
    /// <summary>
    /// 缓存项类型
    /// </summary>
    public enum CacheType
    {
        /// <summary>
        /// 空
        /// </summary>
        None,

        /// <summary>
        /// 单一实体方式ShareEntity
        /// </summary>
        Entity,

        /// <summary>
        /// Personal结构
        /// </summary>
        Dictionary,

        /// <summary>
        /// List of rank.
        /// </summary>
        Rank,

        /// <summary>
        /// 队列方式
        /// </summary>
        Queue
    }
}
