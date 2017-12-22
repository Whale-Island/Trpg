using System;

namespace WhaleIsland.Trpg.GM.Common.Model
{

    /// <summary>
    /// 实体表映射属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class EntityTableAttribute : Attribute
    {
        private AccessLevel _accessLevel;

        /// <summary>
        /// 默认构造配置
        /// </summary>
        public EntityTableAttribute()
            : this(AccessLevel.ReadWrite, CacheType.None, false, null, null, 0, null)
        {
        }

        /// <summary>
        /// PersonalCacheStruct 需要存储数据库时，使用此配置。
        /// </summary>
        /// <param name="connectKey"></param>
        /// <param name="tableName"></param>
        /// <param name="periodTime"></param>
        public EntityTableAttribute(string connectKey, string tableName, int periodTime)
            : this(AccessLevel.ReadWrite, CacheType.Dictionary, true, connectKey, tableName, periodTime, null)
        {
        }

        /// <summary>
        /// ShareCacheStruct 需要存储数据库时，使用此配置。
        /// </summary>
        /// <param name="connectKey"></param>
        /// <param name="tableName"></param>
        public EntityTableAttribute(string connectKey, string tableName)
            : this(AccessLevel.ReadWrite, CacheType.Entity, true, connectKey, tableName, 0, null)
        {
        }

        /// <summary>
        /// 提供给私有缓存模型(PersonalCacheStruct)的初始化构造配置
        /// </summary>
        /// <param name="connectKey">映射到数据连接Key配置</param>
        /// <param name="tableName">映射到表名</param>
        /// <param name="periodTime">缓存的生命周期，0：永久</param>
        /// <param name="personalName">绑定表中主键字段名，如：UserId</param>
        public EntityTableAttribute(string connectKey, string tableName, int periodTime, string personalName)
            : this(AccessLevel.ReadWrite, CacheType.Dictionary, true, connectKey, tableName, periodTime, personalName)
        {
        }

        /// <summary>
        /// 提供给共享缓存模型(ShareCacheStruct)的初始化构造配置
        /// </summary>
        /// <param name="cacheType">缓存的存储结构类型</param>
        /// <param name="connectKey">映射到数据连接Key配置</param>
        /// <param name="tableName">映射到表名</param>
        /// <param name="periodTime">缓存的生命周期，0：永久</param>
        public EntityTableAttribute(CacheType cacheType, string connectKey, string tableName = "", int periodTime = 0)
            : this(AccessLevel.ReadWrite, cacheType, true, connectKey, tableName, periodTime, null)
        {
        }

        /// <summary>
        /// 提供给共享缓存模型（只读或只写方式）的字典类型的初始化构造配置
        /// </summary>
        /// <param name="accessLevel">只读或只写方式</param>
        /// <param name="connectKey">映射到数据连接Key配置</param>
        /// <param name="tableName">映射到表名</param>
        /// <param name="periodTime">缓存的生命周期，0：永久</param>
        public EntityTableAttribute(AccessLevel accessLevel, string connectKey, string tableName = "", int periodTime = 0)
            : this(accessLevel, CacheType.Dictionary, true, connectKey, tableName, periodTime, null)
        {
        }

        /// <summary>
        /// 提供给不更新到数据库中的初始化构造配置
        /// </summary>
        /// <param name="accessLevel">访问级别</param>
        /// <param name="cacheType">缓存的存储结构类型</param>
        /// <param name="isStoreInDb">缓存变动是否更新到数据库</param>
        /// <param name="periodTime"></param>
        public EntityTableAttribute(AccessLevel accessLevel, CacheType cacheType, bool isStoreInDb, int periodTime = 0)
            : this(accessLevel, cacheType, isStoreInDb, "", "", periodTime, null)
        {

        }

        /// <summary>
        /// 提供初始化构造配置
        /// </summary>
        /// <param name="accessLevel">访问级别</param>
        /// <param name="cacheType">缓存的类型</param>
        /// <param name="isStoreInDb">缓存变动是否更新到数据库</param>
        /// <param name="connectKey">映射到数据连接Key配置</param>
        /// <param name="tableName">映射到表名</param>
        /// <param name="periodTime">缓存的生命周期</param>
        /// <param name="personalName">绑定表中主键字段名，如：UserId</param>
        public EntityTableAttribute(AccessLevel accessLevel, CacheType cacheType, bool isStoreInDb, string connectKey, string tableName, int periodTime, string personalName)
        {
            AccessLevel = accessLevel;
            CacheType = cacheType;
            IsStoreInDb = isStoreInDb;
            ConnectKey = connectKey;
            TableName = tableName;
            PeriodTime = periodTime;
            PersonalName = personalName ?? "UserId";//默认值
            IsExpired = true;
        }

        /// <summary>
        /// 访问权限级别
        /// </summary>
        public AccessLevel AccessLevel
        {
            get
            {
                return _accessLevel;
            }
            set
            {
                _accessLevel = value;
                switch (value)
                {
                    case AccessLevel.ReadOnly:
                        StorageType |= StorageType.ReadOnlyDB;
                        break;
                    case AccessLevel.WriteOnly:
                        StorageType |= StorageType.WriteOnlyDB;
                        break;
                    case AccessLevel.ReadWrite:
                        StorageType |= StorageType.ReadWriteRedis;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("value");
                }
            }
        }

        /// <summary>
        /// 存储在缓存的类型
        /// </summary>
        public CacheType CacheType { get; set; }

        /// <summary>
        /// 是否存储到DB
        /// </summary>
        public bool IsStoreInDb
        {
            set
            {
                if (value)
                {
                    StorageType |= StorageType.WriteOnlyDB;
                }
            }
        }

        /// <summary>
        /// 是否持久化到DB，当从Redis内存移除后
        /// </summary>
        [Obsolete("", true)]
        public bool IsPersistence { get; set; }

        /// <summary>
        /// 自增的启始编号[Redis]
        /// </summary>
        public long IncreaseStartNo { get; set; }

        /// <summary>
        /// StorageType.
        /// </summary>
        public StorageType StorageType { get; set; }

        /// <summary>
        /// 是否过期
        /// </summary>
        public bool IsExpired { get; set; }

        /// <summary>
        /// 生命周期，单位秒
        /// </summary>
        public int PeriodTime { get; set; }

        /// <summary>
        /// 容量
        /// </summary>
        public int Capacity { get; set; }

        /// <summary>
        /// 连接串的KEY
        /// </summary>
        public string ConnectKey
        {
            get;
            set;
        }

        /// <summary>
        /// 数据表名
        /// </summary>
        public string TableName
        {
            get;
            set;
        }

        /// <summary>
        /// 表名的格式
        /// </summary>
        public string TableNameFormat { get; set; }

        /// <summary>
        /// 绑定分组字段
        /// </summary>
        public string PersonalName
        {
            get;
            set;
        }

        /// <summary>
        /// index column
        /// </summary>
        public string[] Indexs { get; set; }

        /// <summary>
        /// 条件，不需要加Where
        /// </summary>
        public string Condition
        {
            get;
            set;
        }

        /// <summary>
        /// 排序列,多个以逗号分隔
        /// </summary>
        public string OrderColumn
        {
            get;
            set;
        }

    }
}
