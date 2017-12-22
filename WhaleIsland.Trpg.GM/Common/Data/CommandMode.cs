namespace WhaleIsland.Trpg.GM.Common.Data
{
    /// <summary>
    /// 实体更新方式
    /// </summary>
    public enum CommandMode
    {
        /// <summary>
        /// 插入
        /// </summary>
        Insert = 0,

        /// <summary>
        /// 修改
        /// </summary>
        Modify,

        /// <summary>
        /// 删除
        /// </summary>
        Delete,

        /// <summary>
        /// 先修改，不成功再插入
        /// </summary>
        ModifyInsert,

        /// <summary>
        /// 查询
        /// </summary>
        Inquiry
    }
}
