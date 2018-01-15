using Flexlive.CQP.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WhaleIsland.Trpg.Dice
{
    public class DiceService
    {
        private const string ErrorMessage = "指令错误,输入.rh可查看帮助。";

        public static string ReceivedGroupMessage(long fromQQ, long fromGroup, string message)
        {
            try
            {
                message = message.Trim(' ');
                if (string.IsNullOrEmpty(message))
                    return null;

                int index = message.IndexOf(".rh");
                if (index >= 0) {
                    StringBuilder sb = new StringBuilder("格式：.r [一千以内数字]d[最小值，不填则默认1]-[最大值] 内容");
                    sb.Append("\r\n示例：");
                    sb.Append("\r\n.r");
                    sb.Append("\r\n.r d50");
                    sb.Append("\r\n.r d50 内容");
                    sb.Append("\r\n.r 5d100");
                    sb.Append("\r\n.r 5d100 内容");
                    sb.Append("\r\n.r d50-100");
                    sb.Append("\r\n.r d50-100 内容");
                    sb.Append("\r\n.r 10d50-100");
                    sb.Append("\r\n.r 10d50-100 内容");
                    return sb.ToString();
                }


                index = message.IndexOf(".r");
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
                return ErrorMessage;
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
                return ErrorMessage;
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
                return ErrorMessage;
            }

            return null;
        }


        private static string Roll(List<string> cmd, string nickname)
        {
            Random random = new Random();
            if (cmd.Count() == 1)
            {
                return string.Format("时间：{0}，{1} 投掷骰子D20=>{2}", DateTime.Now.ToString(), nickname, random.Next(1, 21));
            }
            else
            {
                string keys = cmd[1].ToUpper();
                string context = cmd.Count() > 2 ? cmd[2] : "";

                string str1 = keys.ToUpper().Substring(0, keys.IndexOf('D'));
                string str2 = keys.ToUpper().Substring(keys.IndexOf('D') + 1);

                int count = string.IsNullOrEmpty(str1) ? 1 : int.Parse(str1);
                int max = 0;
                int min = 1;

                if (string.IsNullOrEmpty(str2))
                {
                    max = 20;
                }
                else if (str2.IndexOf('-') < 0)
                {
                    max = string.IsNullOrEmpty(str2) ? 20 : int.Parse(str2);
                }
                else
                {
                    min = int.Parse(str2.Substring(0, str2.IndexOf('-')));
                    max = int.Parse(str2.Substring(str2.IndexOf('-') + 1));
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
                    result = "[" + result + "] = " + total;
                }

                if (min == 1)
                    return string.Format("时间：{0}，{1} 投掷 {2} 骰子{3}D{4}=>{5}", DateTime.Now.ToString(), nickname, context, count, max, result);
                return string.Format("时间：{0}，{1} 投掷 {2} 骰子{3}D{4}-{5}=>{6}", DateTime.Now.ToString(), nickname, context, count, min, max, result);
            }
        }
    }
}
