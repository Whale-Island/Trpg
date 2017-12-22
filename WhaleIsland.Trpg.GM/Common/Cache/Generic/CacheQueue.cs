using ProtoBuf;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using WhaleIsland.Trpg.GM.Common.Event;

namespace WhaleIsland.Trpg.GM.Common.Cache.Generic
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [ProtoContract, Serializable]
    public class CacheQueue<T> : EntityChangeEvent, ICollection<T>, IReadOnlyCollection<T>, IDataExpired, IDisposable
    {
        private ConcurrentQueue<T> _cacheStruct;
        private bool _isReadOnly;

        /// <summary>
        ///
        /// </summary>
        public CacheQueue()
            : this(false, null)
        {

        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="isReadOnly"></param>
        public CacheQueue(bool isReadOnly)
            : this(isReadOnly, null)
        {

        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="expiredHandle"></param>
        public CacheQueue(Func<string, CacheQueue<T>, bool> expiredHandle)
            : this(false, expiredHandle)
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="isReadOnly"></param>
        /// <param name="expiredHandle"></param>
        public CacheQueue(bool isReadOnly, Func<string, CacheQueue<T>, bool> expiredHandle)
            : base(isReadOnly)
        {
            _isReadOnly = isReadOnly;
            _cacheStruct = new ConcurrentQueue<T>();
            ExpiredHandle = expiredHandle;
        }

        /// <summary>
        ///
        /// </summary>
        public Func<string, CacheQueue<T>, bool> ExpiredHandle
        {
            get;
            set;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="item"></param>
        public void Add(T item)
        {
            Enqueue(item);
        }

        /// <summary>
        ///
        /// </summary>
        public void Clear()
        {
            _cacheStruct = new ConcurrentQueue<T>();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Contains(T item)
        {
            var list = new List<T>(ToArray());
            return list.Contains(item);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="array"></param>
        /// <param name="arrayIndex"></param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            _cacheStruct.CopyTo(array, arrayIndex);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Remove(T item)
        {
            return false;
        }

        /// <summary>
        ///
        /// </summary>
        public int Count
        {
            get
            {
                return _cacheStruct.Count;
            }
        }

        /// <summary>
        ///
        /// </summary>
        public bool IsReadOnly
        {
            get { return _isReadOnly; }
        }

        /// <summary>
        ///
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                return _cacheStruct.IsEmpty;
            }
        }

        /// <summary>
        /// 尝试取出
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool TryDequeue(out T item)
        {
            return _cacheStruct.TryDequeue(out item);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public bool TryDequeueAll(out T[] items)
        {
            List<T> list = new List<T>();
            while (_cacheStruct.Count > 0)
            {
                T item;
                if (_cacheStruct.TryDequeue(out item))
                {
                    list.Add(item);
                }
            }
            items = list.ToArray();
            return true;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool TryPeek(out T item)
        {
            return _cacheStruct.TryPeek(out item);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="item"></param>
        public void Enqueue(T item)
        {
            _cacheStruct.Enqueue(item);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public T[] ToArray()
        {
            return _cacheStruct.ToArray();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool RemoveExpired(string key)
        {
            if (ExpiredHandle != null)
            {
                return ExpiredHandle(key, this);
            }
            return false;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public IEnumerator<T> GetEnumerator()
        {
            return _cacheStruct.GetEnumerator();
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Func返回false跳出遍历
        /// </summary>
        public void Foreach(Func<string, T, bool> pre, string key)
        {
            var e = GetEnumerator();
            while (e.MoveNext())
            {
                if (!pre(key, e.Current))
                {
                    break;
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _cacheStruct = null;
            }
            base.Dispose(disposing);
        }
    }

}
