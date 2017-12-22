using System;

namespace WhaleIsland.Trpg.GM.Common.Data.MySql
{
    /// <summary>
    /// Mysql数据的命令过滤器
    /// </summary>
    public class MySqlCommandFilter : CommandFilter
    {
        /// <summary>
        ///
        /// </summary>
        public MySqlCommandFilter()
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="paramName"></param>
        /// <param name="value"></param>
        public override void AddParam(string paramName, object value)
        {
            AddParam(MySqlParamHelper.MakeInParam(paramName, value));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="compareChar"></param>
        /// <param name="paramName"></param>
        /// <returns>The expression.</returns>
        public override string FormatExpression(string fieldName, string compareChar = "", string paramName = "")
        {
            return MySqlParamHelper.FormatFilterParam(fieldName, compareChar, paramName);
        }

        public override string FormatExpressionByIn(string fieldName, params object[] values)
        {
            if (values.Length == 0)
            {
                throw new ArgumentException("values len:0");
            }

            var paramNames = new string[values.Length];
            for (int i = 0; i < paramNames.Length; i++)
            {
                var paramName = MySqlParamHelper.FormatParamName(fieldName + (i + 1));
                paramNames[i] = paramName;
                AddParam(MySqlParamHelper.MakeInParam(paramName, values[i]));
            }
            return string.Format("{0} IN ({1})", MySqlParamHelper.FormatName(fieldName), string.Join(",", paramNames));

        }
    }
}
