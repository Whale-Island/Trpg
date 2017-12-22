using System;
using System.Threading;

namespace WhaleIsland.Trpg.GM.Common.Timing
{
    /// <summary>
    /// 对象显示回收基类
    /// </summary>
    public abstract class BaseDisposable : IDisposable
    {
        private int _isDisposed;

        /// <summary>
        /// 显示释放对象资源
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        /// <summary>
        /// 检查对象是否已被显示释放了
        /// </summary>
        protected void CheckDisposed()
        {
            if (_isDisposed == 1)
            {
                throw new Exception(string.Format("The {0} object has be disposed.", this.GetType().Name));
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            //if (disposing)
            //{
            //    //释放 托管资源
            //}
            //释放非托管资源
            if (disposing)
            {
                Interlocked.Exchange(ref _isDisposed, 1);
                GC.SuppressFinalize(this);
            }
        }
    }

}
