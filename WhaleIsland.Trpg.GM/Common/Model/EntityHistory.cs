using ProtoBuf;
using System;

namespace WhaleIsland.Trpg.GM.Common.Model
{
    /// <summary>
    /// Redis backup entity
    /// </summary>
    [ProtoContract, Serializable]
    [EntityTable(CacheType.Entity, "", "Temp_EntityHistory")]
    internal class EntityHistory : ShareEntity
    {
        /// <summary>
        /// init
        /// </summary>
        public EntityHistory()
            : base(false)
        {

        }

        /// <summary>
        /// The key.
        /// </summary>
        [EntityField(true, ColumnLength = 255)]
        public string Key
        {
            get;
            set;
        }

        /// <summary>
        /// The bytes data for hash(value).
        /// </summary>
        [EntityField]
        public byte[] Value
        {
            get;
            set;
        }
    }
}
