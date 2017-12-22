using WhaleIsland.Trpg.GM.Common.Model;

namespace WhaleIsland.Trpg.GM.Common.Cache.Generic
{
    /// <summary>
    /// 默认缓存对象,负责通用更新事件通知
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class DefaultCacheStruct<T> : BaseCacheStruct<T> where T : AbstractEntity, new()
    {
        protected override bool LoadFactory(bool isReplace)
        {
            return true;
        }

        protected override bool LoadItemFactory(string key, bool isReplace)
        {
            return true;
        }

        public override bool Update(bool isChange, string changeKey = null)
        {
            SchemaTable schema;
            if (EntitySchemaSet.TryGet<T>(out schema) &&
                schema.AccessLevel == AccessLevel.ReadWrite)
            {
                if (schema.CacheType == CacheType.Entity)
                {
                    return UpdateEntity(isChange, changeKey);
                }
                if (schema.CacheType == CacheType.Queue)
                {
                    return UpdateQueue(isChange, changeKey);
                }
                return UpdateGroup(isChange, changeKey);
            }
            return false;
        }
    }

}
