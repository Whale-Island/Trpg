using ProtoBuf;
using System;
using WhaleIsland.Trpg.GM.Common.Model;
using WhaleIsland.Trpg.GM.Model.Enum;

namespace WhaleIsland.Trpg.GM.Model.Entity.PersonalEntity
{
    [Serializable, ProtoContract, EntityTable("Game", "player", Const.Entity.PeriodTime)]
    public partial class Player : BaseEntity
    {
        protected internal override long GetIdentityId() { return UserId; }

        [ProtoMember(1), EntityField(true)]
        public long UserId { get; set; }

        [ProtoMember(2), EntityField]
        public string QQ { get; set; }

        [ProtoMember(3), EntityField]
        public string Nickname { get; set; }

        /// <summary>
        /// 1男2女
        /// </summary>
        [ProtoMember(4), EntityField]
        public Gender Gender { get; set; }

        [ProtoMember(5), EntityField]
        public int Level { get; set; }

        [ProtoMember(6), EntityField]
        public int Exp { get; set; }

        [ProtoMember(7), EntityField]
        public int Point { get; set; }

        [ProtoMember(8), EntityField]
        public int Coin { get; set; }

        /// <summary>
        /// 活力
        /// </summary>
        [ProtoMember(9), EntityField]
        public int Energy { get; set; }

        /// <summary>
        /// 种族
        /// </summary>
        [ProtoMember(10), EntityField]
        public Race Race { get; set; }

        /// <summary>
        /// 智力
        /// </summary>
        [ProtoMember(11), EntityField]
        public int Brains { get; set; }

        /// <summary>
        /// 精神力
        /// </summary>
        [ProtoMember(12), EntityField]
        public int Spirit { get; set; }

        /// <summary>
        /// 韧性
        /// </summary>
        [ProtoMember(13), EntityField]
        public int Tenacity { get; set; }

        /// <summary>
        /// 耐力
        /// </summary>
        [ProtoMember(14), EntityField]
        public int Stamina { get; set; }

        /// <summary>
        /// 力量
        /// </summary>
        [ProtoMember(15), EntityField]
        public int Strength { get; set; }

        /// <summary>
        /// 速度
        /// </summary>
        [ProtoMember(16), EntityField]
        public int Speed { get; set; }

        /// <summary>
        /// 免疫力
        /// </summary>
        [ProtoMember(17), EntityField]
        public int Immunity { get; set; }

        [ProtoMember(18), EntityField]
        public int HP { get; set; }
        [ProtoMember(19), EntityField]
        public int MaxHP { get; set; }

        [ProtoMember(20), EntityField]
        public int MP { get; set; }

        [ProtoMember(21), EntityField]
        public int MaxMP { get; set; }

        [ProtoMember(22), EntityField]
        public int Luck { get; set; }
    }
}