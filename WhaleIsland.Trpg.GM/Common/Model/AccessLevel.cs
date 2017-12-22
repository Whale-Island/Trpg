namespace WhaleIsland.Trpg.GM.Common.Model
{
    /// <summary>
    /// 访问权限级别
    /// </summary>
    public enum AccessLevel
    {
        /// <summary>
        /// 只读的,值=0
        /// </summary>
        ReadOnly = 0,

        /// <summary>
        /// 只写,值=1
        /// </summary>
        WriteOnly,

        /// <summary>
        /// 读写,值=2
        /// </summary>
        ReadWrite
    }
}
