using NLog;
using System;
using System.Collections.Concurrent;
using System.Text;
using System.Web;

namespace WhaleIsland.Trpg.GM.Common.Log
{
    internal class LogHelper
    {
        private static bool _isinit;
        private static bool _logInfoEnable;
        private static bool _logErrorEnable;
        private static bool _logWarnEnable;
        private static bool _logComplementEnable;
        private static bool _logDubugEnable;
        private static bool _logFatalEnabled;
        private static Logger _logger;
        private static ConcurrentDictionary<string, Logger> _customLoggers;

        static LogHelper()
        {
            _customLoggers = new ConcurrentDictionary<string, Logger>();
            _isinit = false;
            _logInfoEnable = false;
            _logErrorEnable = false;
            _logWarnEnable = false;
            _logComplementEnable = false;
            _logDubugEnable = false;
            _logFatalEnabled = false;
            _logger = LogManager.GetCurrentClassLogger();

            if (!_isinit)
            {
                _isinit = true;
                SetConfig();
            }
        }

        public static void SetConfig()
        {
            _logInfoEnable = _logger.IsInfoEnabled;
            _logErrorEnable = _logger.IsErrorEnabled;
            _logWarnEnable = _logger.IsWarnEnabled;
            _logComplementEnable = _logger.IsTraceEnabled;
            _logFatalEnabled = _logger.IsFatalEnabled;
            _logDubugEnable = _logger.IsDebugEnabled;
        }

        public static void WriteInfo(string info)
        {
            if (LogHelper._logInfoEnable)
            {
                var cachedConsoleColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.White;
                LogHelper._logger.Info(LogHelper.BuildMessage(info));
                Console.ForegroundColor = cachedConsoleColor;
            }
        }

        public static void WriteDebug(string info)
        {
            if (LogHelper._logDubugEnable)
            {
                var cachedConsoleColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Gray;
                LogHelper._logger.Debug(LogHelper.BuildMessage(info));
                Console.ForegroundColor = cachedConsoleColor;
            }
        }

        public static void WriteError(string info)
        {
            if (LogHelper._logErrorEnable)
            {
                var cachedConsoleColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                LogHelper._logger.Error(LogHelper.BuildMessage(info));
                Console.ForegroundColor = cachedConsoleColor;
            }
        }

        public static void WriteException(string info, Exception ex)
        {
            if (LogHelper._logErrorEnable)
            {
                var cachedConsoleColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.DarkRed;
                LogHelper._logger.Error(LogHelper.BuildMessage(info, ex));
                Console.ForegroundColor = cachedConsoleColor;
            }
        }

        public static void WriteWarn(string info)
        {
            if (LogHelper._logWarnEnable)
            {
                var cachedConsoleColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Yellow;
                LogHelper._logger.Warn(LogHelper.BuildMessage(info));
                Console.ForegroundColor = cachedConsoleColor;
            }
        }

        public static void WriteWarn(string info, Exception ex)
        {
            if (LogHelper._logWarnEnable)
            {
                var cachedConsoleColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Yellow;
                LogHelper._logger.Warn(LogHelper.BuildMessage(info, ex));
                Console.ForegroundColor = cachedConsoleColor;
            }
        }

        public static void WriteFatal(string info)
        {
            if (LogHelper._logFatalEnabled)
            {
                var cachedConsoleColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.DarkRed;
                LogHelper._logger.Fatal(LogHelper.BuildMessage(info));
                Console.ForegroundColor = cachedConsoleColor;
            }
        }

        public static void WriteComplement(string info)
        {
            WriteTo("", info);
        }

        public static void WriteComplement(string info, Exception ex)
        {
            WriteTo("", info, ex);
        }

        public static void WriteTo(string name, string info, Exception ex = null)
        {
            if (string.IsNullOrEmpty(name))
            {
                name = "Complement";
            }
            var lazy = new Lazy<Logger>(() => LogManager.GetLogger(name));
            Logger customLog = _customLoggers.GetOrAdd(name, lazy.Value);
            if (customLog != null)
            {
                customLog.Log(LogLevel.Trace, LogHelper.BuildMessage(info, ex));
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="message"></param>
        public static void WriteLine(string message)
        {
            WriteLine(LogLevel.Info, message);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="level"></param>
        /// <param name="message"></param>
        public static void WriteLine(LogLevel level, string message)
        {
            _logger.Log(level, message);
        }

        private static string BuildMessage(string info)
        {
            return LogHelper.BuildMessage(info, null);
        }

        private static string BuildMessage(string info, Exception ex)
        {
            StringBuilder stringBuilder = new StringBuilder();
            HttpContext current = HttpContext.Current;
            try
            {
                stringBuilder.AppendFormat("Time:{0}-{1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"), info);
                if (current != null)
                {
                    HttpRequest request = null;
                    try
                    {
                        //IIS 7.0 集成模式不能用
                        request = current.Request;
                    }
                    catch
                    {
                    }

                    if (request != null)
                    {
                        stringBuilder.AppendFormat("\r\nUrl:{0}", current.Request.Url);
                        if (null != current.Request.UrlReferrer)
                        {
                            stringBuilder.AppendFormat("\r\nUrlReferrer:{0}", current.Request.UrlReferrer);
                        }
                        stringBuilder.AppendFormat("\r\nUserHostAddress:{0}", current.Request.UserHostAddress);
                    }
                }
                if (ex != null)
                {
                    stringBuilder.AppendFormat("\r\nException:{0}", ex.ToString());
                }
            }
            catch (Exception error)
            {
                stringBuilder.AppendLine(info + ", Exception:\r\n" + error);
            }
            //stringBuilder.AppendLine();
            return stringBuilder.ToString();
        }
    }
}
