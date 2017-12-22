using ProtoBuf;
using System;
using WhaleIsland.Trpg.GM.Common.Model;
using WhaleIsland.Trpg.GM.Model.Enum;

namespace WhaleIsland.Trpg.GM.Model.Entity.PersonalEntity
{
    [Serializable, ProtoContract, EntityTable("Game", "equip", Const.Entity.PeriodTime)]
    class Equip : BaseEntity
    {
        protected internal override long GetIdentityId() { return UserId; }

        [ProtoMember(1), EntityField(true)]
        public long UserId { get; set; }

        public EquipType Type { get; set; }


    }
}
