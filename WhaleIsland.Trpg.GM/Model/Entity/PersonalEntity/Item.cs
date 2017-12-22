using ProtoBuf;
using System;
using WhaleIsland.Trpg.GM.Common.Model;
using WhaleIsland.Trpg.GM.Model.Enum;

namespace WhaleIsland.Trpg.GM.Model.Entity.PersonalEntity
{
    [Serializable, ProtoContract, EntityTable("Game", "backpack", Const.Entity.PeriodTime)]
    public class Item : BaseEntity
    {
        protected internal override long GetIdentityId() { return UserId; }

        [ProtoMember(1), EntityField(true)]
        public long UserId { get; set; }

        [ProtoMember(2), EntityField(true)]
        public long Id { get; set; }

        [ProtoMember(3), EntityField]
        public ItemType Type { get; set; }

        [ProtoMember(4), EntityField]
        public int ModelId { get; set; }

        [ProtoMember(5), EntityField]
        public int Num { get; set; }

        [ProtoMember(6), EntityField]
        public int Damage { get; set; }

        [ProtoMember(7), EntityField]
        public int MagicDamage { get; set; }

        /// <summary>
        /// 智力
        /// </summary>
        [ProtoMember(8), EntityField]
        public int Brains { get; set; }

        /// <summary>
        /// 精神力
        /// </summary>
        [ProtoMember(9), EntityField]
        public int Spirit { get; set; }

        /// <summary>
        /// 韧性
        /// </summary>
        [ProtoMember(10), EntityField]
        public int Tenacity { get; set; }

        /// <summary>
        /// 耐力
        /// </summary>
        [ProtoMember(11), EntityField]
        public int Stamina { get; set; }

        /// <summary>
        /// 力量
        /// </summary>
        [ProtoMember(12), EntityField]
        public int Strength { get; set; }

        /// <summary>
        /// 速度
        /// </summary>
        [ProtoMember(13), EntityField]
        public int Speed { get; set; }

        /// <summary>
        /// 免疫力
        /// </summary>
        [ProtoMember(14), EntityField]
        public int Immunity { get; set; }

        [ProtoMember(15), EntityField]
        public int HP { get; set; }

        [ProtoMember(16), EntityField]
        public int MP { get; set; }

        [ProtoMember(17), EntityField]
        public int Luck { get; set; }

        [ProtoMember(18), EntityField]
        public int Weight { get; set; }
    }
}
