using Flexlive.CQP.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace WhaleIsland.Trpg.Dice
{
    public class DiceService
    {
        private const string ErrorMessage = "指令错误,输入.rh可查看帮助。";
        private const int DEFAULT_COUNT = 1;
        private const int DEFAULT_MAX = 20;
        private const int DEFAULT_MIN = 1;
        private const float DEFAULT_PERCENT = 1;
        private const int DEFAULT_WEIGHTING = 0;
        private readonly static char[] WEIGHTING_SPLIT = new char[] { '+', '-' };

        public static Dictionary<long, List<long>> OBGroupMap = new Dictionary<long, List<long>>();
        public static Dictionary<long, List<long>> OBDiscussMap = new Dictionary<long, List<long>>();

        private const string help = "格式：.r [一千以内数字]d[最小值，不填则默认1]~[最大值]+-[最终结果加或者减一个值]p[最终结果乘一个值] 内容" +
            "\r\n示例：" +
            "\r\n.r" +
            "\r\n.r d50" +
            "\r\n.r d50 内容" +
            "\r\n.r 5d100" +
            "\r\n.r 10d50~100+20p2.88 内容（50到100范围随机,最终结果先乘2.88再加20）" +
            "\r\n.r 10d50~100p1.88-30 内容（50到100范围随机,最终结果先减30再乘1.88）";


        /// <summary>
        /// 接收群消息
        /// </summary>
        /// <param name="fromQQ"></param>
        /// <param name="fromGroup"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static string ReceivedGroupMessage(long fromQQ, long fromGroup, string message)
        {
            try
            {
                message = message.Trim(' ');
                if (string.IsNullOrWhiteSpace(message))
                    return null;

                int index = message.ToLower().IndexOf(".rh");
                if (index >= 0)
                    return help;

                index = message.ToLower().IndexOf(".r");
                if (index >= 0)
                {
                    var groupMember = CQ.GetGroupMemberInfo(fromGroup, fromQQ);
                    string nickname = string.IsNullOrWhiteSpace(groupMember.GroupCard) ? groupMember.QQName : groupMember.GroupCard;

                    return Roll(message, index, nickname);
                }
                index = message.ToLower().IndexOf(".ww");
                if (index >= 0)
                {
                    var groupMember = CQ.GetGroupMemberInfo(fromGroup, fromQQ);
                    string nickname = string.IsNullOrWhiteSpace(groupMember.GroupCard) ? groupMember.QQName : groupMember.GroupCard;
                    return WW(message, index, nickname);
                }
                index = message.ToLower().IndexOf(".f");
                if (index >= 0)
                {
                    var groupMember = CQ.GetGroupMemberInfo(fromGroup, fromQQ);
                    string nickname = string.IsNullOrWhiteSpace(groupMember.GroupCard) ? groupMember.QQName : groupMember.GroupCard;
                    return Fata(message, index, nickname);
                }

                index = message.ToLower().IndexOf(".oblist");
                if (index >= 0)
                {
                    return OBList(0, fromGroup, fromQQ);
                }

                index = message.ToLower().IndexOf(".clob");
                if (index >= 0)
                {
                    return CLOB(0, fromGroup, fromQQ);
                }

                index = message.ToLower().IndexOf(".exob");
                if (index >= 0)
                {
                    var groupMember = CQ.GetGroupMemberInfo(fromGroup, fromQQ);
                    string nickname = string.IsNullOrWhiteSpace(groupMember.GroupCard) ? groupMember.QQName : groupMember.GroupCard;
                    return EXOB(0, fromGroup, fromQQ, nickname);
                }

                index = message.ToLower().IndexOf(".ob");
                if (index >= 0)
                {
                    var groupMember = CQ.GetGroupMemberInfo(fromGroup, fromQQ);
                    string nickname = string.IsNullOrWhiteSpace(groupMember.GroupCard) ? groupMember.QQName : groupMember.GroupCard;
                    return ADDOB(0, fromGroup, fromQQ, nickname);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return ErrorMessage;
            }

            return null;
        }



        /// <summary>
        /// 接收讨论组消息
        /// </summary>
        /// <param name="fromQQ"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static string ReceivedDiscussMessage(long fromQQ, long fromDiscuss, string message)
        {
            try
            {
                message = message.Trim(' ');
                if (string.IsNullOrWhiteSpace(message))
                    return null;

                int index = message.ToLower().IndexOf(".rh");
                if (index >= 0)
                    return help;

                index = message.ToLower().IndexOf(".r");
                if (index >= 0)
                {
                    string nickname = CQ.GetQQName(fromQQ);
                    return Roll(message, index, nickname);
                }

                index = message.ToLower().IndexOf(".clob");
                if (index >= 0)
                {
                    return CLOB(1, fromDiscuss, fromQQ);
                }

                index = message.ToLower().IndexOf(".oblist");
                if (index >= 0)
                {
                    return OBList(1, fromDiscuss, fromQQ);
                }

                index = message.ToLower().IndexOf(".exob");
                if (index >= 0)
                {
                    return EXOB(1, fromDiscuss, fromQQ, string.Empty);
                }

                index = message.ToLower().IndexOf(".ob");
                if (index >= 0)
                {
                    return ADDOB(1, fromDiscuss, fromQQ, string.Empty);
                }





                // 私人骰
                index = message.ToLower().IndexOf(".ww");
                if (index >= 0)
                {
                    string nickname = CQ.GetQQName(fromQQ);
                    return WW(message, index, nickname);
                }
                index = message.ToLower().IndexOf(".f");
                if (index >= 0)
                {
                    string nickname = CQ.GetQQName(fromQQ);
                    return Fata(message, index, nickname);
                }

            }
            catch (Exception)
            {
                return ErrorMessage;
            }

            return null;
        }

        /// <summary>
        /// 接收私人消息
        /// </summary>
        /// <param name="qq"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static string Received(long qq, string message)
        {
            try
            {
                message = message.Trim(' ');
                if (string.IsNullOrWhiteSpace(message))
                    return null;

                int index = message.ToLower().IndexOf(".rh");
                if (index >= 0)
                    return help;

                index = message.ToLower().IndexOf(".r");
                if (index >= 0)
                    return Roll(message, index, string.Empty);
                index = message.ToLower().IndexOf(".ww");
                if (index >= 0)
                    return WW(message, index, string.Empty);
                index = message.ToLower().IndexOf(".f");
                if (index >= 0)
                    return Fata(message, index, string.Empty);
            }
            catch (Exception)
            {
                return ErrorMessage;
            }
            return null;
        }

        /// <summary>
        /// 通用骰
        /// </summary>
        /// <param name="message"></param>
        /// <param name="index"></param>
        /// <param name="nickname"></param>
        /// <returns></returns>
        private static string Roll(string message, int index, string nickname)
        {
            message = message.Substring(index + 2);
            var cmd = message.Trim(' ').Split(' ').ToList();
            cmd.RemoveAll(t => string.IsNullOrWhiteSpace(t));

            Random random = new Random();
            if (cmd.Count == 0 || string.IsNullOrWhiteSpace(cmd[0]))
            {
                return string.Format("时间：{0}，{1} 投掷  骰子1D20=>{2}", DateTime.Now.ToString(), nickname, random.Next(1, 21));
            }
            else
            {
                string keys = cmd[0].ToUpper();
                string context = cmd.Count() >= 2 ? cmd[1] : "";

                int count = DEFAULT_COUNT;
                int max = DEFAULT_MAX;
                int min = DEFAULT_MIN;
                float percent = DEFAULT_PERCENT;//百分比加成
                int weighting = DEFAULT_WEIGHTING;//加权值

                var match = Regex.Match(keys, "\\d*D\\d*~?\\d*");
                if (match.Success)
                {
                    string[] dice = match.Value.Split('D');
                    if (!string.IsNullOrWhiteSpace(dice[0]))
                    {
                        count = int.Parse(dice[0]);
                    }

                    if (!string.IsNullOrWhiteSpace(dice[1]))
                    {
                        if (dice[1].IndexOf('~') < 0)
                        {
                            max = int.Parse(dice[1]);
                        }
                        else
                        {
                            string[] values = dice[1].Split('~');
                            min = int.Parse(values[0]);
                            max = int.Parse(values[1]);
                        }
                    }
                }

                match = Regex.Match(keys, "P\\d+\\.?\\d*");
                if (match.Success)
                {
                    percent = float.Parse(match.Value.Substring(1));
                }

                match = Regex.Match(keys, "(\\+|\\-)\\d*");
                if (match.Success)
                {
                    weighting = int.Parse(match.Value);
                }

                count = count > 1000 ? 1000 : count;

                string result = "";
                int total = 0;

                for (int i = 0; i < count; i++)
                {
                    int t = random.Next(min, max + 1);
                    total += t;

                    result += t.ToString();
                    if (i < count - 1)
                        result += ",";
                }

                if (count > 1)
                {
                    result = string.Format("[{0}]→{1}", result, total);
                }

                if (percent != DEFAULT_PERCENT && weighting != DEFAULT_WEIGHTING)
                {
                    int p_index = keys.IndexOf('P');
                    int w_index = keys.IndexOfAny(WEIGHTING_SPLIT);
                    if (p_index < w_index)
                    {
                        total = (int)(total * percent);
                        result = string.Format("{0}，加成{1}→{2}", result, percent, total);

                        total = total + weighting;
                        result = string.Format("{0}，加权{1}→{2}", result, weighting, total);
                    }
                    else
                    {
                        total = total + weighting;
                        result = string.Format("{0}，加权{1}→{2}", result, weighting, total);

                        total = (int)(total * percent);
                        result = string.Format("{0}，加成{1}→{2}", result, percent, total);
                    }
                }
                else if (percent != DEFAULT_PERCENT)
                {
                    total = (int)(total * percent);
                    result = string.Format("{0}，加成{1}→{2}", result, percent, total);
                }
                else if (weighting != DEFAULT_WEIGHTING)
                {
                    total = total + weighting;
                    result = string.Format("{0}，加权{1}→{2}", result, weighting, total);
                }

                if (min == DEFAULT_MIN)
                    return string.Format("时间：{0}，{1} 投掷 {2} 骰子{3}D{4}=>{5}", DateTime.Now.ToString(), nickname, context, count, max, result);
                return string.Format("时间：{0}，{1} 投掷 {2} 骰子{3}D{4}-{5}=>{6}", DateTime.Now.ToString(), nickname, context, count, min, max, result);
            }
        }

        private static string CLOB(byte type, long fromGroup, long fromQQ)
        {
            Dictionary<long, List<long>> OBMap = type == 0 ? OBGroupMap : OBDiscussMap;
            if (OBMap.TryGetValue(fromGroup, out List<long> list))
            {
                list.Clear();
            }
            return string.Format("OB列表已清空。");
        }

        private static string OBList(byte type, long fromGroup, long fromQQ)
        {
            Dictionary<long, List<long>> OBMap = type == 0 ? OBGroupMap : OBDiscussMap;
            if (OBMap.TryGetValue(fromGroup, out List<long> list) && list.Count > 0)
            {
                StringBuilder sb = new StringBuilder();

                foreach (long qq in list)
                {
                    var groupMember = CQ.GetGroupMemberInfo(fromGroup, qq);
                    string nn = string.IsNullOrWhiteSpace(groupMember.GroupCard) ? groupMember.QQName : groupMember.GroupCard;
                    sb.Append(nn).Append("  ");
                }
                return string.Format("当前OB人数为{0}，名单：{1}", list.Count, sb.ToString());
            }

            return string.Format("当前OB列表为空，还未有用户加入OB。");
        }

        private static string ADDOB(byte type, long fromGroup, long fromQQ, string nickname)
        {
            Dictionary<long, List<long>> OBMap = type == 0 ? OBGroupMap : OBDiscussMap;

            if (OBMap.TryGetValue(fromGroup, out List<long> list))
            {
                if (list.Contains(fromQQ))
                {
                    return string.Format("{0}已加入OB，无需再次加入。", nickname);
                }
                list.Add(fromQQ);
            }
            else
            {
                list = new List<long> { fromQQ };
                OBMap.Add(fromGroup, list);
            }
            return string.Format("{0}加入OB，当前OB人数为：{1}。\r\n输入.exob可退出OB。\r\n输入.oblist检视OB列表。\r\n输入.clob清空OB列表。\r\n注：OB列表每周自动清理。", nickname, list.Count);
        }

        private static string EXOB(byte type, long fromGroup, long fromQQ, string nickname)
        {
            Dictionary<long, List<long>> OBMap = type == 0 ? OBGroupMap : OBDiscussMap;
            if (OBMap.TryGetValue(fromGroup, out List<long> list))
            {
                if (list.Contains(fromQQ))
                {
                    list.Remove(fromQQ);
                    return string.Format("{0}退出OB，当前OB人数为：{1}。", nickname, list.Count);
                }
            }
            return string.Format("{0}不在OB列表内。", nickname);
        }

        /// <summary>
        /// 私人骰
        /// </summary>
        /// <param name="message"></param>
        /// <param name="index"></param>
        /// <param name="nickname"></param>
        /// <returns></returns>
        private static string WW(string message, int index, string nickname)
        {
            message = message.ToUpper();
            int count = 1;
            int attached = 10;
            var match = Regex.Match(message, "WW\\d+");
            if (match.Success)
            {
                count = int.Parse(match.Value.Substring(2));
            }
            match = Regex.Match(message, "A\\d+");
            if (match.Success)
            {
                attached = int.Parse(match.Value.Substring(1));
                if (attached < 8 || attached > 10)
                {
                    return ".ww的加骰必须为8-10";
                }
            }
            Random random = new Random();
            string result = "";
            int ten_strike = 0;
            int success = 0;

            for (int i = 0; i < count; i++)
            {
                int t = random.Next(1, 11);

                if (t == 10)
                {
                    ten_strike++;
                    i--;
                }
                else if (t >= 6)
                {
                    success++;
                }

                result += t.ToString();
                if (i < count - 1)
                    result += ",";
            }
            return string.Format("时间：{0}，{1} 投掷  骰子.ww{2}a{3} →[{4}] 成功数：{5},大成功数：{6}", DateTime.Now.ToString(), nickname, count, attached, result, success, ten_strike);
        }

        /// <summary>
        /// 私人骰
        /// </summary>
        /// <param name="message"></param>
        /// <param name="index"></param>
        /// <param name="nickname"></param>
        /// <returns></returns>
        private static string Fata(string message, int index, string nickname)
        {
            message = message.Substring(index + 2);
            var cmd = message.Trim(' ').Split(' ').ToList();
            cmd.RemoveAll(t => string.IsNullOrWhiteSpace(t));

            Random random = new Random();
            string keys = cmd.Count() >= 1 ? cmd[0].ToUpper() : "";
            string context = cmd.Count() >= 2 ? cmd[1] : "";

            int count = 4;
            int max = 3;
            int min = DEFAULT_MIN;
            float percent = DEFAULT_PERCENT;//百分比加成
            int weighting = -8;//加权值

            var match = Regex.Match(keys, "P\\d+\\.?\\d*");
            if (match.Success)
            {
                percent = float.Parse(match.Value.Substring(1));
            }

            match = Regex.Match(keys, "(\\+|\\-)\\d*");
            if (match.Success)
            {
                weighting = int.Parse(match.Value);
            }

            string result = "";
            int total = 0;

            for (int i = 0; i < count; i++)
            {
                int t = random.Next(min, max + 1);
                total += t;

                result += t.ToString();
                if (i < count - 1)
                    result += ",";
            }

            if (count > 1)
            {
                result = string.Format("[{0}]→{1}", result, total);
            }

            if (percent != DEFAULT_PERCENT && weighting != DEFAULT_WEIGHTING)
            {
                int p_index = keys.IndexOf('P');
                int w_index = keys.IndexOfAny(WEIGHTING_SPLIT);
                if (p_index < w_index)
                {
                    total = (int)(total * percent);
                    result = string.Format("{0}，加成{1}→{2}", result, percent, total);

                    total = total + weighting;
                    result = string.Format("{0}，加权{1}→{2}", result, weighting, total);
                }
                else
                {
                    total = total + weighting;
                    result = string.Format("{0}，加权{1}→{2}", result, weighting, total);

                    total = (int)(total * percent);
                    result = string.Format("{0}，加成{1}→{2}", result, percent, total);
                }
            }
            else if (percent != DEFAULT_PERCENT)
            {
                total = (int)(total * percent);
                result = string.Format("{0}，加成{1}→{2}", result, percent, total);
            }
            else if (weighting != DEFAULT_WEIGHTING)
            {
                total = total + weighting;
                result = string.Format("{0}，加权{1}→{2}", result, weighting, total);
            }

            if (min == DEFAULT_MIN)
                return string.Format("时间：{0}，{1} 投掷 {2} 骰子{3}D{4}=>{5}", DateTime.Now.ToString(), nickname, context, count, max, result);
            return string.Format("时间：{0}，{1} 投掷 {2} 骰子{3}D{4}-{5}=>{6}", DateTime.Now.ToString(), nickname, context, count, min, max, result);
        }

    }
}
