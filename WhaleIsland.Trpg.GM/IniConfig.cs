using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace WhaleIsland.Trpg.GM
{
    class IniConfig
    {
        private string iniPath = "";
        private bool isConfig;
        private ArrayList propertyList;

        /// <summary>
        /// 构造函数：装载配置文件
        /// </summary>
        /// <param name="iniPath">配置文件的路径</param>
        public IniConfig(string iniPath)
        {
            this.IniPath = iniPath;
        }

        public string IniPath
        {
            set
            {
                iniPath = value;
                isConfig = OnIniPataChanged();
            }
        }


        /// <summary>
        /// 读取Ini中的配置
        /// </summary>
        /// <param name="section">节点</param>
        /// <param name="key">键</param>
        /// <param name="value">返回的键值</param>
        /// <returns>读取是否成功</returns>
        public bool ReadConfig(string section, string key, ref string value)
        {
            bool isRead = false;
            try
            {
                if (isConfig)
                {
                    for (int i = 0; i < propertyList.Count; i++)
                    {
                        Property p = (Property)propertyList[i];
                        if (p.Section == section && p.Key == key)
                        {
                            value = p.Value;
                            isRead = true;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
            return isRead;
        }

        /// <summary>
        /// 向INI中写入配置
        /// </summary>
        /// <param name="section">节点</param>
        /// <param name="key">键</param>
        /// <param name="value">要写入的新键值</param>
        /// <returns>写入是否成功</returns>
        public bool WriteConfig(string section, string key, string value)
        {
            bool isWrite = false;
            try
            {
                if (isConfig)
                {
                    for (int i = 0; i < propertyList.Count; i++)
                    {
                        Property p = (Property)propertyList[i];
                        if (p.Section == section && p.Key == key)
                        {
                            p.Value = value;
                            isWrite = SaveIni();
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
            return isWrite;
        }


        #region 私有方法
        private bool OnIniPataChanged()
        {
            bool isLoad = false;
            try
            {
                if (File.Exists(iniPath))
                {
                    isLoad = LoadIni();
                }
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
            return isLoad;
        }

        /// <summary>
        /// 从文件中加载INI配置信息到列表
        /// </summary>
        /// <returns>加载是否成功</returns>
        private bool LoadIni()
        {
            bool isLoad = false;
            try
            {
                propertyList = new ArrayList();
                StreamReader stream = new StreamReader(iniPath, System.Text.Encoding.Default);
                string section = "";
                while (stream.Peek() != -1)
                {
                    string str = stream.ReadLine().Trim();
                    //判断该行是否有数据
                    if (str.Length > 0)
                    {
                        //以“;”开头的行为注释行（硬性规定）
                        if (str.Substring(0, 1) != ";")
                        {
                            //以“[“开头的行为Section行（硬性规定）
                            if (str.Substring(0, 1) == "[")
                            {
                                //记录当前Section
                                section = str.Substring(1, str.IndexOf("]") - 1);
                            }
                            //有“=”的为数据行（硬性规定）
                            if (str.IndexOf("=") > 0)
                            {
                                string[] temp = str.Split('=');
                                //将该数据行的属性添加到列表
                                propertyList.Add(new Property(section, temp[0].Trim(), temp[1].Trim(), ""));
                            }
                        }
                        else
                        {
                            //将注释行的属性添加到列表
                            propertyList.Add(new Property(section, "", "", str));
                        }
                    }
                    else
                    {
                        //为保证格式与加载前相同，因些将空行也加入了列表
                        propertyList.Add(new Property("", "", "", ""));
                    }
                }
                stream.Close();
                isLoad = true;
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
            return isLoad;
        }

        /// <summary>
        /// 将列表中的配置信息保存到INI文件
        /// </summary>
        /// <returns>保存是否成功</returns>
        private bool SaveIni()
        {
            bool isSave = false;
            try
            {
                StreamWriter stream = new StreamWriter(iniPath, false, System.Text.Encoding.Default);
                string section = "";
                for (int i = 0; i < propertyList.Count; i++)
                {
                    Property p = (Property)propertyList[i];
                    //写入Section
                    if (p.Section != "" && p.Section != section)
                    {
                        section = p.Section;
                        stream.WriteLine("[" + section + "]");
                    }
                    //写入注释
                    if (p.Description != "")
                    {
                        stream.WriteLine(p.Description);
                    }
                    //写入键和键值
                    if (p.Key != "")
                    {
                        stream.WriteLine(p.Key + " = " + p.Value);
                    }
                    //写入空行
                    if (p.Section == "" && p.Description == "")
                    {
                        stream.WriteLine("");
                    }
                }
                stream.Close();
                isSave = true;
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
            return isSave;
        }

        /// <summary>
        /// 内部类：Ini属性
        /// </summary>
        private class Property
        {
            string section = "";
            /// <summary>
            /// 节点
            /// </summary>
            public string Section
            {
                get { return section; }
                set { section = value; }
            }
            string key = "";
            /// <summary>
            /// 键
            /// </summary>
            public string Key
            {
                get { return key; }
                set { key = value; }
            }
            string value = "";
            /// <summary>
            /// 键值
            /// </summary>
            public string Value
            {
                get { return this.value; }
                set { this.value = value; }
            }
            string description = "";
            /// <summary>
            /// 注释
            /// </summary>
            public string Description
            {
                get { return description; }
                set { description = value; }
            }
            /// <summary>
            /// 构造函数
            /// </summary>
            /// <param name="section"></param>
            /// <param name="key"></param>
            /// <param name="value"></param>
            /// <param name="description"></param>
            public Property(string section, string key, string value, string description)
            {
                this.Section = section;
                this.Key = key;
                this.Value = value;
                this.Description = description;
            }
        }
        #endregion
    }
}
