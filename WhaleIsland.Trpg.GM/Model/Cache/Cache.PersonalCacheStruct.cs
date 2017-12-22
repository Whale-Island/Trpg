using WhaleIsland.Trpg.GM.Common.Cache.Generic;
using WhaleIsland.Trpg.GM.Model.Entity.PersonalEntity;

namespace WhaleIsland.Trpg.GM.Model.Cache
{
    public static partial class Cache
    {
        public readonly static PersonalCacheStruct<Player> Player = new PersonalCacheStruct<Player>();
    }
}
