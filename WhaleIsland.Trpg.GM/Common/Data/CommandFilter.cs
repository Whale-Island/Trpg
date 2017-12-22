using System;
using System.Collections.Generic;
using System.Data;
using WhaleIsland.Trpg.GM.Common.Data.Sql;

namespace WhaleIsland.Trpg.GM.Common.Data
{

    /// <summary>
    /// Sql命令过滤器
    /// </summary>
    public class CommandFilter
    {
        private Dictionary<string, IDataParameter> _parameter = new Dictionary<string, IDataParameter>();

        /// <summary>
        /// init
        /// </summary>
        public CommandFilter()
        {
            Condition = string.Empty;
        }

        /// <summary>
        /// Sql语句中where表达式
        /// </summary>
        public string Condition
        {
            get;
            set;
        }

        /// <summary>
        /// 格式化where表达式，针对不同数据库的参数关键词的处理
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="compareChar"></param>
        /// <param name="paramName"></param>
        public virtual string FormatExpression(string fieldName, string compareChar = "", string paramName = "")
        {
            return SqlParamHelper.FormatFilterParam(fieldName, compareChar, paramName);
        }

        /// <summary>
        /// 格式化where中IN从语句的表达式
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="values"></param>
        public virtual string FormatExpressionByIn(string fieldName, params object[] values)
        {
            if (values.Length == 0)
            {
                throw new ArgumentException("values len:0");
            }
            var paramNames = new string[values.Length];
            for (int i = 0; i < paramNames.Length; i++)
            {
                var paramName = SqlParamHelper.FormatParamName(fieldName + (i + 1));
                paramNames[i] = paramName;
                AddParam(SqlParamHelper.MakeInParam(paramName, values[i]));
            }
            return string.Format("{0} IN ({1})", SqlParamHelper.FormatName(fieldName), string.Join(",", paramNames));

        }

        /// <summary>
        /// 获得Sql命令的参数列表
        /// </summary>
        public IDataParameter[] Parameters
        {
            get
            {
                int index = 0;
                IDataParameter[] resultList = new IDataParameter[_parameter.Count];
                foreach (KeyValuePair<string, IDataParameter> item in _parameter)
                {
                    resultList[index] = item.Value;
                    index++;
                }
                return resultList;
            }
        }

        /// <summary>
        /// 添加Sql命令的参数
        /// </summary>
        /// <param name="paramName">不带@的参数名</param>
        /// <param name="value">参数值</param>
        public virtual void AddParam(string paramName, object value)
        {
            AddParam(SqlParamHelper.MakeInParam(paramName, value));
        }

        /// <summary>
        /// 不使用
        /// </summary>
        /// <param name="paramName"></param>
        /// <param name="dbType"></param>
        /// <param name="size"></param>
        /// <param name="value"></param>
        [Obsolete]
        public void AddParam(string paramName, SqlDbType dbType, int size, object value)
        {
            AddParam(SqlParamHelper.MakeInParam(paramName, dbType, size, value));
        }

        /// <summary>
        /// 添加Sql命令的参数
        /// </summary>
        /// <param name="param"></param>
        public void AddParam(IDataParameter param)
        {
            if (param != null)
            {
                string paramKey = param.GetFieldName();
                if (_parameter.ContainsKey(paramKey))
                {
                    _parameter[paramKey] = param;
                }
                else
                {
                    _parameter.Add(paramKey, param);
                }
            }
        }
    }
}
