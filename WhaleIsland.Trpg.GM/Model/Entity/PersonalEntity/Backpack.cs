using ProtoBuf;
using System;
using WhaleIsland.Trpg.GM.Common.Cache.Generic;
using WhaleIsland.Trpg.GM.Common.Model;

namespace WhaleIsland.Trpg.GM.Model.Entity.PersonalEntity
{
    [Serializable, ProtoContract, EntityTable("Game", "backpack", Const.Entity.PeriodTime)]
    public class Backpack : BaseEntity
    {
        protected internal override long GetIdentityId() { return UserId; }

        [ProtoMember(1), EntityField(true)]
        public long UserId { get; set; }

        [ProtoMember(2), EntityField(true)]
        public long Id { get; set; }

        [ProtoMember(2), EntityField]
        public CacheList<Item> ItemList { get; set; }

    }
}
