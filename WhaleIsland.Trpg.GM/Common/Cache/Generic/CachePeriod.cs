using ProtoBuf;
using System;
using System.Threading;

namespace WhaleIsland.Trpg.GM.Common.Cache.Generic
{
    /// <summary>
    /// 缓存周期
    /// </summary>
    [ProtoContract, Serializable]
    internal class CachePeriod
    {
        private DateTime _preAccessTime;
        private DateTime _accessTime;
        private int _preAccessCounter;
        private int _accessCounter;

        /// <summary>
        ///
        /// </summary>
        /// <param name="periodTime"></param>
        public CachePeriod(int periodTime)
        {
            _preAccessTime = DateTime.Now;
            _accessTime = DateTime.Now;
            PeriodTime = periodTime;
        }

        private int _periodTime;

        /// <summary>
        /// 过期时间，单位秒
        /// </summary>
        public int PeriodTime
        {
            get
            {
                return _periodTime;
            }
            set
            {
                _periodTime = value;
                if (_periodTime <= 0)
                {
                    IsPersistence = true;
                }
            }
        }

        /// <summary>
        /// 是否过期
        /// </summary>
        public bool IsPeriod
        {
            get { return !IsPersistence && MathUtils.DiffDate(_accessTime).TotalSeconds > PeriodTime; }
        }

        /// <summary>
        /// 是否持久(周期)
        /// </summary>
        public bool IsPersistence
        {
            get;
            private set;
        }

        /// <summary>
        /// 访问计数频率
        /// </summary>
        public double CounterFrequency
        {
            get
            {

                int counter = MathUtils.Subtraction(_accessCounter, _preAccessCounter);
                double timeSpan = MathUtils.DiffDate(_accessTime, _preAccessTime).TotalSeconds;
                if (timeSpan > 0)
                {
                    return counter / timeSpan;
                }
                return 0;
            }
        }

        /// <summary>
        ///
        /// </summary>
        public void ResetCounter()
        {
            _preAccessCounter = 0;
            _accessCounter = 0;
        }

        /// <summary>
        ///
        /// </summary>
        public void RefreshAccessTime()
        {
            _preAccessTime = _accessTime;
            _accessTime = DateTime.Now;
            _preAccessCounter = _accessCounter;
            Interlocked.Increment(ref _accessCounter);
        }

    }

}
