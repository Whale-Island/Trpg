using System;
using System.Diagnostics;
using System.Text;
using WhaleIsland.Trpg.GM.Common.Log;

namespace WhaleIsland.Trpg.GM.Common.Timing
{
    /// <summary>
    /// Watch code process run time.
    /// </summary>
    public class RunTimeWatch : IDisposable
    {
        private static string MessageTip = "{0} {1}ms{2}";
        private string _message;
        private Stopwatch _watch;
        private long preTime;
        private StringBuilder log = new StringBuilder();

        /// <summary>
        ///
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static RunTimeWatch StartNew(string message)
        {
            return new RunTimeWatch(message);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="message"></param>
        public RunTimeWatch(string message)
        {
            this._message = message;
            _watch = Stopwatch.StartNew();
        }

        /// <summary>
        /// Check of tag use time.
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="timeout"></param>
        public void Check(string tag, int timeout = 0)
        {
            var ms = _watch.ElapsedMilliseconds;
            var ts = ms - preTime;
            preTime = ms;
            if (timeout == 0 || (timeout > 0 && ts > timeout))
            {
                log.AppendLine();
                log.AppendFormat(">>{0} time:{1}ms run:{2}ms", tag, preTime, ts);
            }
        }

        /// <summary>
        /// Write to log.
        /// </summary>
        public void Flush(bool error = false, int timeout = 0)
        {
            var time = _watch.ElapsedMilliseconds;
            if (error && (timeout == 0 || time > timeout))
            {
                TraceLog.WriteError(MessageTip, _message, time, log);
            }
            else if (timeout == 0 || time > timeout)
            {
                TraceLog.ReleaseWriteDebug(MessageTip, _message, time, log);
            }
        }

        /// <summary>
        ///
        /// </summary>
        public TimeSpan Elapsed
        {
            get;
            private set;
        }

        /// <summary>
        ///
        /// </summary>
        public void Dispose()
        {
            _watch.Stop();
            Elapsed = _watch.Elapsed;
            Flush();
            _watch = null;
        }

    }

}
