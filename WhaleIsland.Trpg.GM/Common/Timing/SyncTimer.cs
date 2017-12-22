using System;
using System.Threading;
using WhaleIsland.Trpg.GM.Common.Log;

namespace WhaleIsland.Trpg.GM.Common.Timing
{
    /// <summary>
    /// Sync timer
    /// </summary>
    public class SyncTimer
    {
        private readonly TimerCallback _callback;
        private Timer _timer;
        private int isInTimer;
        private Thread _executeThread;
        private Timer _executeTimer;

        /// <summary>
        ///
        /// </summary>
        public int DueTime { get; set; }

        /// <summary>
        ///
        /// </summary>
        public int Period { get; set; }

        /// <summary>
        ///
        /// </summary>
        public int ExecuteTimeout { get; set; }

        /// <summary>
        /// init
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="dueTime"></param>
        /// <param name="period"></param>
        /// <param name="executeTimeout"></param>
        public SyncTimer(TimerCallback callback, int dueTime, int period, int executeTimeout = 60000)
        {
            _callback = callback;
            DueTime = dueTime;
            Period = period;
            ExecuteTimeout = executeTimeout;
        }

        /// <summary>
        /// Start
        /// </summary>
        public virtual void Start()
        {
            _timer = new Timer(InternalDoWork, null, DueTime, Period);
            _executeTimer = new Timer(AbortExecute, null, Timeout.Infinite, Timeout.Infinite);
        }

        /// <summary>
        /// Stop
        /// </summary>
        public virtual void Stop()
        {
            _timer.Dispose();
            while (Interlocked.CompareExchange(ref isInTimer, 1, 0) == 1) Thread.Sleep(10);
        }

        private void AbortExecute(object state)
        {
            try
            {
                if (isInTimer == 1)
                {
                    var myExecuteThread = Interlocked.Exchange<Thread>(ref _executeThread, null);
                    if (myExecuteThread != null)
                    {
                        myExecuteThread.Abort();
                    }
                    isInTimer = 0;
                }
            }
            catch (Exception ex)
            {
                TraceLog.WriteError("SyncTimer execute timeout error:{0}", ex);
            }
        }

        private void InternalDoWork(object state)
        {
            if (Interlocked.CompareExchange(ref isInTimer, 1, 0) == 1)
            {
                return;
            }
            try
            {
                _executeThread = Thread.CurrentThread;
                _executeTimer.Change(ExecuteTimeout, Timeout.Infinite);
                _callback(state);
            }
            catch (Exception ex)
            {
                TraceLog.WriteError("SyncTimer execute error:{0}", ex);
            }
            finally
            {
                _executeThread = null;
                _executeTimer.Change(Timeout.Infinite, Timeout.Infinite);
                Interlocked.Exchange(ref isInTimer, 0);
            }
        }

    }
}
