using System;
using System.Collections.Generic;
using System.Linq;

namespace WhaleIsland.Trpg.GM
{
    public class GMService
    {
        public static string Received(long qq, string nickname, string message)
        {
            try
            {
                message = message.Trim(' ');
                if (string.IsNullOrEmpty(message))
                    return null;

                var cmd = message.Split(' ').ToList();
                cmd.RemoveAll(t => string.IsNullOrEmpty(t)); ;

                switch (cmd[0])
                {
                    case ".cp":
                       // return CreatePlayer(cmd, nickname);
                    case ".rd":
                        return Roll(cmd, nickname);
                    case ".cm":
                        return ChangeMoney(cmd, nickname);
                    case ".chp":
                        return ChangeHP(cmd, nickname);
                    case ".cmp":
                        return ChangeMP(cmd, nickname);
                    case ".spc":
                        return SelectPC(cmd, nickname);
                    default:
                        break;
                }
            }
            catch (Exception e)
            {
               // TraceLog.WriteError("发送者：{0},昵称：{1} 执行指令:“{2}”，发生异常,异常为：{3}", qq, nickname, message, e.Message);
                return "指令错误.";
            }

            return null;
        }

        //private static string CreatePlayer(List<string> cmd, string nickname)
        //{
        //    if (cmd.Count < 2) return "创建PC失败，参数不完整。";

        //    string name = cmd[1];
        //    if (Cache.Player.IsExist(p => p.Nickname.Equals(name))) return "创建PC失败，昵称重复。";

        //    int gender = cmd.Count > 2 ? int.Parse(cmd[2]) : 0;
        //    var player = new Player()
        //    {
        //        UserId = Cache.Player.GetNextNo(),
        //        Nickname = name,
        //        Gender = (Gender)gender,
        //        HP = 1000,
        //        MaxHP = 1000,
        //        MaxMP = 100,
        //        MP = 100,
        //        Level = 1,
        //    };

        //    Cache.Player.Add(player);

        //    return string.Format("创建PC成功，昵称为{0},性别为{1}.", player.Nickname, EnumService.GetDescription(player.Gender));
        //}

        private static string Roll(List<string> cmd, string nickname)
        {
            Random random = new Random();
            if (cmd.Count() == 1)
            {
                return random.Next(1, 21).ToString();
            }
            else
            {
                string keys = cmd[1].ToUpper();
                string context = cmd.Count() == 3 ? cmd[2] : "";

                string str1 = keys.ToUpper().Substring(0, keys.IndexOf('D'));
                string str2 = keys.ToUpper().Substring(keys.IndexOf('D') + 1);

                int count = string.IsNullOrEmpty(str1) ? 1 : int.Parse(str1);
                int num = string.IsNullOrEmpty(str2) ? 20 : int.Parse(str2);
                count = count > 100 ? 100 : count;

                string result = "";

                for (int i = 0; i < count; i++)
                {
                    result += random.Next(1, num + 1) + ",";
                }
                result = result.TrimEnd(',');

                if (result.Contains(','))
                {
                    result = "{" + result + "}";
                }

                return string.Format("时间：{0}，{1}投掷 {2} 骰子{3}={4}", DateTime.Now.ToString(), nickname, context, keys, result);
            }
        }

        private static string ChangeMoney(List<string> cmd, string nickname)
        {
            string name = cmd[1];
            int num = int.Parse(cmd[2]);


            return null;
        }

        private static string ChangeHP(List<string> cmd, string nickname)
        {
            throw new NotImplementedException();
        }

        private static string ChangeMP(List<string> cmd, string nickname)
        {
            throw new NotImplementedException();
        }

        private static string SelectPC(List<string> cmd, string nickname)
        {
            throw new NotImplementedException();
        }



    }
}
