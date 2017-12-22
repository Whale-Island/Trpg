using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhaleIsland.Trpg.GM.Common.Log;

namespace WhaleIsland.Trpg.GM.Common.Message
{
    public class SensitiveWordManager
    {
        private static readonly SensitiveWordService s_SensitiveWordService = new SensitiveWordService();

        public static void Reload()
        {
            SensitiveWordService.Init();
            TraceLog.WriteInfo("SensitiveWordService reload OK.");
        }

        /// <summary>
        /// 检查是否包含敏感词。
        /// </summary>
        /// <param name="message">要检查的字符串。</param>
        /// <returns>若不包含敏感词，则返回 true，否则返回 false。</returns>
        public static bool Check(string message)
        {
            return !s_SensitiveWordService.IsVerified(message);
        }

        /// <summary>
        /// 过滤敏感词。
        /// </summary>
        /// <param name="message">要过滤的字符串。</param>
        /// <param name="replaceChar">将敏感词替换为此字符。</param>
        /// <returns>过滤后的字符串。</returns>
        public static string Filter(string message, char replaceChar = '*')
        {
            return s_SensitiveWordService.Filter(message, replaceChar);
        }
    }
}
