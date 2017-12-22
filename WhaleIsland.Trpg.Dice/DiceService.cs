using Flexlive.CQP.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WhaleIsland.Trpg.Dice
{
    public class DiceService
    {

        public static string ReceivedGroupMessage(long fromQQ, long fromGroup, string message)
        {
            try
            {
                message = message.Trim(' ');
                if (string.IsNullOrEmpty(message))
                    return null;

                int index = message.IndexOf(".r");
                if (index >= 0)
                {
                    var groupMember = CQ.GetGroupMemberInfo(fromGroup, fromQQ);
                    string nickname = string.IsNullOrWhiteSpace(groupMember.GroupCard) ? groupMember.QQName : groupMember.GroupCard;

                    message = message.Substring(index);
                    var cmd = message.Split(' ').ToList();
                    cmd.RemoveAll(t => string.IsNullOrWhiteSpace(t));
                    return Roll(cmd, nickname);
                }
            }
            catch (Exception)
            {
                return "指令错误,格式为 .r [一千以内数字]d[20亿以内数字] 内容";
            }

            return null;
        }

        public static string ReceivedDiscussMessage(long fromQQ, string message)
        {
            try
            {
                message = message.Trim(' ');
                if (string.IsNullOrEmpty(message))
                    return null;

                int index = message.IndexOf(".r");
                if (index >= 0)
                {
                    string nickname = CQ.GetQQName(fromQQ);

                    message = message.Substring(index);
                    var cmd = message.Split(' ').ToList();
                    cmd.RemoveAll(t => string.IsNullOrWhiteSpace(t));
                    return Roll(cmd, nickname);
                }
            }
            catch (Exception)
            {
                return "指令错误,格式为 .r [一千以内数字]d[20亿以内数字] 内容";
            }

            return null;
        }

        public static string Received(long qq, string message)
        {
            try
            {
                message = message.Trim(' ');
                if (string.IsNullOrEmpty(message))
                    return null;

                int index = message.IndexOf(".r");
                if (index >= 0)
                {
                    message = message.Substring(index);
                    var cmd = message.Split(' ').ToList();
                    cmd.RemoveAll(t => string.IsNullOrWhiteSpace(t));
                    return Roll(cmd, "");
                }
            }
            catch (Exception)
            {
                return "指令错误,格式为 .r [一千以内数字]d[20亿以内数字] 内容";
            }

            return null;
        }


        private static string Roll(List<string> cmd, string nickname)
        {
            Random random = new Random();
            if (cmd.Count() == 1)
            {
                return string.Format("时间：{0}，{1} 投掷骰子D20={2}", DateTime.Now.ToString(), nickname, random.Next(1, 21));
            }
            else
            {
                string keys = cmd[1].ToUpper();
                string context = cmd.Count() == 3 ? cmd[2] : "";

                string str1 = keys.ToUpper().Substring(0, keys.IndexOf('D'));
                string str2 = keys.ToUpper().Substring(keys.IndexOf('D') + 1);

                int count = string.IsNullOrEmpty(str1) ? 1 : int.Parse(str1);
                int num = string.IsNullOrEmpty(str2) ? 20 : int.Parse(str2);
                count = count > 1000 ? 1000 : count;

                string result = "";
                int total = 0;

                for (int i = 0; i < count; i++)
                {
                    int t = random.Next(1, num + 1);
                    total += t;
                    result += t + ",";
                }
                result = result.TrimEnd(',');

                if (result.Contains(','))
                {
                    result = "[" + result + "] = " + total;
                }

                return string.Format("时间：{0}，{1} 投掷 {2} 骰子{3}D{4}={5}", DateTime.Now.ToString(), nickname, context, count, num, result);
            }
        }
    }
}
