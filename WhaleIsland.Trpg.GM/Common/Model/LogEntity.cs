using ProtoBuf;
using System;
using WhaleIsland.Trpg.GM.Common.Model;

namespace WhaleIsland.Trpg.GM.Common.Model
{
    /// <summary>
    /// 日志实体基类
    /// </summary>
    [ProtoContract, Serializable]
    public abstract class LogEntity : AbstractEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WhaleIsland.Trpg.GM.Common.Model.LogEntity"/> class.
        /// </summary>
        protected LogEntity()
            : base(false)
        {

        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        protected internal override long GetIdentityId()
        {
            return DefIdentityId;
        }

    }
}
