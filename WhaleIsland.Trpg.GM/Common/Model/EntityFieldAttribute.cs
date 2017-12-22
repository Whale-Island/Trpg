using System;

namespace WhaleIsland.Trpg.GM.Common.Model
{
    /// <summary>
    ///
    /// </summary>
    public enum FieldModel
    {
        /// <summary>
        ///
        /// </summary>
        Read = 1,

        /// <summary>
        ///
        /// </summary>
        Write,

        /// <summary>
        ///
        /// </summary>
        All
    }

    /// <summary>
    /// 实体字段映射属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class EntityFieldAttribute : Attribute
    {
        /// <summary>
        ///
        /// </summary>
        public EntityFieldAttribute()
        {
            FieldName = string.Empty;
            FieldModel = FieldModel.All;
            DbType = ColumnDbType.Varchar;
            Isnullable = false;
            JsonDateTimeFormat = "yyyy'-'MM'-'dd' 'HH':'mm':'ss";
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="fieldName"></param>
        public EntityFieldAttribute(string fieldName)
            : this()
        {
            FieldName = fieldName;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="isKey"></param>
        public EntityFieldAttribute(bool isKey)
            : this()
        {
            IsKey = isKey;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="dbType"></param>
        /// <param name="isJsonSerialize"></param>
        public EntityFieldAttribute(bool isJsonSerialize, ColumnDbType dbType)
            : this()
        {
            IsJsonSerialize = isJsonSerialize;
            DbType = dbType;
        }

        /// <summary>
        /// 字段名
        /// </summary>
        public string FieldName
        {
            get;
            set;
        }

        /// <summary>
        /// 列的大小长度
        /// </summary>
        public int ColumnLength { get; set; }

        /// <summary>
        /// 列的小数位数
        /// </summary>
        public int ColumnScale { get; set; }

        /// <summary>
        /// 列允许为空
        /// </summary>
        public bool Isnullable { get; set; }

        /// <summary>
        /// 读写模式
        /// </summary>
        public FieldModel FieldModel
        {
            get;
            set;
        }

        /// <summary>
        /// 是否主键
        /// </summary>
        public bool IsKey
        {
            get;
            set;
        }

        /// <summary>
        ///
        /// </summary>
        public bool IsUnique { get; set; }

        /// <summary>
        /// 是否自增
        /// </summary>
        public bool IsIdentity
        {
            get;
            set;
        }

        /// <summary>
        /// 自增开始编号
        /// </summary>
        public int IdentityNo { get; set; }

        /// <summary>
        /// 禁用或排除数据库取值
        /// </summary>
        public bool Disable
        {
            get;
            set;
        }

        /// <summary>
        /// 是否使用Json序列化
        /// </summary>
        public bool IsJsonSerialize
        {
            get;
            set;
        }

        /// <summary>
        /// 使用Json日期格式化
        /// </summary>
        public string JsonDateTimeFormat
        {
            get;
            set;
        }

        /// <summary>
        /// 取值范围
        /// </summary>
        public int MinRange
        {
            get;
            set;
        }

        /// <summary>
        /// 取值范围
        /// </summary>
        public int MaxRange
        {
            get;
            set;
        }

        /// <summary>
        /// Db映射类型
        /// </summary>
        public ColumnDbType DbType
        {
            get;
            set;
        }
    }
}
