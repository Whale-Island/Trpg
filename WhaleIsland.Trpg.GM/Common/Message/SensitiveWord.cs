using ProtoBuf;
using System;
using WhaleIsland.Trpg.GM.Common.Model;

namespace WhaleIsland.Trpg.GM.Common.Message
{
    /// <summary>
    ///
    /// </summary>
    [Serializable, ProtoContract]
    [EntityTable(AccessLevel.ReadOnly, "Service", "sensitive_word")]
    public class SensitiveWord : ShareEntity
    {

        /// <summary>
        /// </summary>
        public SensitiveWord()
            : base(AccessLevel.ReadOnly)
        {

        }

        /// <summary>
        /// </summary>
        public SensitiveWord(int code)
            : this()
        {
            Code = code;
        }

        /// <summary>
        ///
        /// </summary>
        [ProtoMember(1)]
        [EntityFieldExtend]
        [EntityField(true)]
        public int Code
        {
            get;
            private set;
        }

        /// <summary>
        ///
        /// </summary>
        [ProtoMember(2)]
        [EntityFieldExtend]
        [EntityField]
        public string Word
        {
            get;
            private set;
        }

    }

}
