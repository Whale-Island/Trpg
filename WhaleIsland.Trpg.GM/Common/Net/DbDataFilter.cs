using System;

namespace WhaleIsland.Trpg.GM.Common.Net
{
    /// <summary>
    /// 数据库过滤条件
    /// </summary>
    public class DbDataFilter
    {
        /// <summary>
        ///
        /// </summary>
        public DbDataFilter()
        {
            Parameters = new Parameters();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="capacity"></param>
        public DbDataFilter(int capacity)
            : this()
        {
            Capacity = capacity;
            CreateTime = DateTime.Now;
        }

        /// <summary>
        /// 获取结果的容量，0不限制
        /// </summary>
        public int Capacity
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取或设置where条件
        /// </summary>
        public string Condition
        {
            get;
            set;
        }

        /// <summary>
        /// 获取或设置排序字段
        /// </summary>
        public string OrderColumn
        {
            get;
            set;
        }

        /// <summary>
        /// 获取或设置参数, 数据名不需要@前缀
        /// </summary>
        public Parameters Parameters
        {
            get;
            set;
        }

        /// <summary>
        /// 动态表名指定的日期
        /// </summary>
        public DateTime CreateTime { get; set; }
    }

}
