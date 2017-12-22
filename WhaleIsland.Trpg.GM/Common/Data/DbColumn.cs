using System;

namespace WhaleIsland.Trpg.GM.Common.Data
{
    /// <summary>
    /// 数据列对象
    /// </summary>
    public class DbColumn
    {
        /// <summary>
        ///
        /// </summary>
        public DbColumn()
        {
            Isnullable = true;
        }

        /// <summary>
        /// 编号
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 获取或设置是否为主键
        /// </summary>
        public bool IsKey { get; set; }

        /// <summary>
        /// 获取或设置主键编码
        /// </summary>
        public int KeyNo { get; set; }

        /// <summary>
        /// 获取或设置是否有唯一约束
        /// </summary>
        public bool IsUnique { get; set; }

        /// <summary>
        /// 获取或设置列的名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置列的类型
        /// </summary>
        public Type Type { get; set; }

        /// <summary>
        /// 获取或设置列的长度
        /// </summary>
        public long Length { get; set; }

        /// <summary>
        /// 获取或设置列的取值，decimal类型指精度范围
        /// </summary>
        public int Scale { get; set; }

        /// <summary>
        /// 获取或设置是否可为空
        /// </summary>
        public bool Isnullable { get; set; }

        /// <summary>
        /// 获取或设置是否是修改列，False则新增列
        /// </summary>
        public bool IsModify { get; set; }

        /// <summary>
        /// 获取或设置主键是否是自增列
        /// </summary>
        /// <value><c>true</c> if this instance is identity; otherwise, <c>false</c>.</value>
        public bool IsIdentity { get; set; }

        /// <summary>
        /// 自增开始编号
        /// </summary>
        public int IdentityNo { get; set; }

        /// <summary>
        /// DB是否有自增编号
        /// </summary>
        public bool HaveIncrement { get; set; }

        /// <summary>
        /// 获取或设置列映射的类型
        /// </summary>
        public string DbType
        {
            get;
            set;
        }

    }
}
