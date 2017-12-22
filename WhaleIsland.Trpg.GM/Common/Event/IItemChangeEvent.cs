using Newtonsoft.Json;
using System;

namespace WhaleIsland.Trpg.GM.Common.Event
{
    /// <summary>
    /// 变更事件接口
    /// </summary>
    [Serializable]
    public abstract class IItemChangeEvent : IDisposable, ICloneable
    {
        /// <summary>
        ///
        /// </summary>

        [JsonIgnore]
        public bool IsExpired { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonIgnore]
        public bool IsInCache { get; set; }

        /// <summary>
        /// 是否有变更
        /// </summary>
        public abstract bool HasChanged { get; }

        /// <summary>
        /// 绑定实体类的属性名（表的列名）
        /// </summary>
        internal abstract string PropertyName { get; set; }

        /// <summary>
        /// 当前对象变更事件对象
        /// </summary>
        public abstract CacheItemChangeEvent ItemEvent { get; }

        /// <summary>
        /// 当前对象的子类变更事件对象
        /// </summary>
        public abstract CacheItemChangeEvent ChildrenEvent { get; }

        /// <summary>
        ///
        /// </summary>
        public abstract void TriggerNotify();

        /// <summary>
        /// 禁用子类事件通知
        /// </summary>
        internal abstract void DisableChildNotify();

        /// <summary>
        /// 增加子类监听
        /// </summary>
        /// <param name="changeEvent"></param>
        public abstract void AddChildrenListener(object changeEvent);

        /// <summary>
        /// 移除子类监听
        /// </summary>
        /// <param name="changeEvent"></param>
        internal abstract void RemoveChildrenListener(object changeEvent);

        /// <summary>
        /// 移除与父类事件绑定
        /// </summary>
        public void RemoveParentEvent()
        {
            var obj = ItemEvent.Parent as IItemChangeEvent;
            if (obj != null)
            {
                obj.RemoveChildrenListener(this);
            }
        }

        /// <summary>
        /// 解除变更事件通知
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        internal abstract void UnChangeNotify(object sender, CacheItemEventArgs eventArgs);

        /// <summary>
        /// 对象更新时通知事件
        /// </summary>
        /// <param name="updateHandle"></param>
        public abstract void UpdateNotify(Func<IItemChangeEvent, bool> updateHandle);

        /// <summary>
        /// Get exclusive modify entity property.
        /// </summary>
        /// <param name="modifyHandle"></param>
        [Obsolete("Use ModifyLocked method", true)]
        public abstract void ExclusiveModify(Action modifyHandle);

        /// <summary>
        /// locked modify value.
        /// </summary>
        /// <param name="modifyHandle"></param>
        public abstract void ModifyLocked(Action modifyHandle);

        /// <summary>
        /// 序列化Json
        /// </summary>
        /// <returns></returns>
        public abstract string ToJson();

        /// <summary>
        ///
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            //释放非托管资源
            if (disposing)
            {
                GC.SuppressFinalize(this);
            }
        }

        /// <summary>
        /// Depth copy property and field of object.
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            return ObjectCloner.Clone(this);
        }

        /// <summary>
        /// Depth copy property and field of object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Clone<T>()
        {
            return (T)Clone();
        }
    }
}
