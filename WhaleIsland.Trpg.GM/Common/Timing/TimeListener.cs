using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using WhaleIsland.Trpg.GM.Common.Log;

namespace WhaleIsland.Trpg.GM.Common.Timing
{
    /// <summary>
    /// 定时器监听管理
    /// 使用场合:对间隔时间比较精确
    /// </summary>
    public static class TimeListener
    {
        private static Thread _timer;
        private static int _isRunning = 0;
        private static int _isDisposed;
        private static readonly object asyncRoot = new object();
        private static List<PlanConfig> _listenerQueue = new List<PlanConfig>();
        private static int _msInterval = 100;
        private static int _dueTime = 5000;
        private static long _offsetMillisecond;

        /// <summary>
        /// Thread run interval time
        /// </summary>
        public static long OffsetMillisecond
        {
            get { return _offsetMillisecond; }
        }

        static TimeListener()
        {
            _timer = new Thread(OnProcess);
            _timer.Name = "#TimeListener";
            _timer.Priority = ThreadPriority.Highest;
            //set background run, can be exited while main thread exit.
            _timer.IsBackground = true;
            _timer.Start();
        }

        private static void OnProcess(object state)
        {
            Thread.Sleep(_dueTime);
            var watch = Stopwatch.StartNew();
            while (true)
            {
                if (_isDisposed == 1) break;
                watch.Restart();
                TimerCallback(state);
                Thread.Sleep(_msInterval);
                Interlocked.Exchange(ref _offsetMillisecond, watch.ElapsedMilliseconds);
            }
        }

        /// <summary>
        /// 设置定时器
        /// </summary>
        /// <param name="msInterval"></param>
        public static void SetTimer(int msInterval)
        {
            Interlocked.Exchange(ref _msInterval, msInterval > 0 ? msInterval : _msInterval);
        }

        /// <summary>
        ///
        /// </summary>
        public static bool HasWaitPlan
        {
            get { return _listenerQueue.Count > 0 && _listenerQueue.Exists(p => !p.IsEnd); }
        }

        /// <summary>
        ///
        /// </summary>
        public static IEnumerable<PlanConfig> PlanList
        {
            get { return _listenerQueue; }
        }

        /// <summary>
        /// 显示释放
        /// </summary>
        public static void Dispose()
        {
            if (Interlocked.CompareExchange(ref _isDisposed, 1, 0) == 1)
            {
                return;
            }
            _timer.Abort();
        }

        /// <summary>
        /// 增加定时任务计划
        /// </summary>
        /// <param name="planConfig"></param>
        public static void Append(PlanConfig planConfig)
        {
            lock (asyncRoot)
            {
                _listenerQueue.Add(planConfig);
            }

        }

        /// <summary>
        /// 移除定时任务计划
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        public static bool Remove(Predicate<PlanConfig> match)
        {
            lock (asyncRoot)
            {
                _listenerQueue.RemoveAll(match);
            }
            return true;
        }

        /// <summary>
        ///
        /// </summary>
        public static void Clear()
        {
            lock (asyncRoot)
            {
                _listenerQueue.Clear();
            }
        }

        private static void TimerCallback(object state)
        {
            if (Interlocked.CompareExchange(ref _isRunning, 1, 0) == 1)
            {
                return;
            }

            try
            {
                DateTime currDate = MathUtils.Now;
                var expiredList = new List<PlanConfig>();
                lock (asyncRoot)
                {
                    foreach (var planConfig in _listenerQueue)
                    {
                        if (planConfig == null || planConfig.IsExpired)
                        {
                            if (planConfig != null)
                            {
                                expiredList.Add(planConfig);
                            }
                            continue;
                        }
                        if (planConfig.AutoStart(currDate))
                        {
                            DoNotify(planConfig);
                        }
                    }

                    foreach (var planConfig in expiredList)
                    {
                        _listenerQueue.Remove(planConfig);
                    }
                }
            }
            catch (Exception er)
            {
                TraceLog.WriteError("Timer listenner:{0}", er);
            }
            finally
            {
                Interlocked.Exchange(ref _isRunning, 0);
            }
        }

        private static void DoNotify(PlanConfig planConfig)
        {
            if (planConfig != null && planConfig.Callback != null)
            {
                ThreadPool.QueueUserWorkItem(obj =>
                {
                    Interlocked.Increment(ref planConfig._isExcuting);
                    try
                    {
                        planConfig.Callback((PlanConfig)obj);
                    }
                    catch (Exception ex)
                    {
                        TraceLog.WriteError("TimeListener notify error:{0}", ex);
                    }
                    finally
                    {
                        Interlocked.Decrement(ref planConfig._isExcuting);
                    }
                }, planConfig);
            }
        }

    }
}
