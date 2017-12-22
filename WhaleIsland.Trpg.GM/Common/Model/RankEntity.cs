using Newtonsoft.Json;
using ProtoBuf;
using System;
using WhaleIsland.Trpg.GM.Common.Event;

namespace WhaleIsland.Trpg.GM.Common.Model
{
    /// <summary>
    /// Rank entity
    /// </summary>
    [ProtoContract, Serializable]
    public abstract class RankEntity : AbstractEntity, IComparable<RankEntity>
    {
        /// <summary>
        /// init
        /// </summary>
        protected RankEntity()
            : base(false)
        {

        }

        /// <summary>
        /// Gets key of rank
        /// </summary>
        public abstract string Key { get; }

        /// <summary>
        /// Get score of rank no, from hight to low
        /// </summary>
        [JsonIgnore]
        public double Score { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        protected internal override long GetIdentityId()
        {
            return DefIdentityId;
        }

        /// <summary>
        /// from hight to low
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(RankEntity other)
        {
            return other.Score.CompareTo(Score);
        }

        /// <summary>
        /// 当前对象(包括继承)的属性触发通知事件
        /// </summary>
        /// <param name="sender">触发事件源</param>
        /// <param name="eventArgs"></param>
        protected override void Notify(object sender, CacheItemEventArgs eventArgs)
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender">由IContainer对象触发</param>
        /// <param name="eventArgs"></param>
        protected override void NotifyByChildren(object sender, CacheItemEventArgs eventArgs)
        {
        }

        internal override void UnChangeNotify(object sender, CacheItemEventArgs eventArgs)
        {
        }
    }

}
