namespace WhaleIsland.Trpg.GM.Common.Cache.Generic
{
    /// <summary>
    /// 数据过期接口
    /// </summary>
    public interface IDataExpired
    {
        /// <summary>
        /// 移除过期键
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool RemoveExpired(string key);

    }
}
