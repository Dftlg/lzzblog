using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace lzz博客.Models.Config
{
    /// <summary>
    /// 键值创建元素
    /// <remarks>
    /// 创建
    /// <remarks>
    /// </summary>
    public class KeyValueElement : ConfigurationElement
    {
        /// <summary>
        /// 键
        /// </summary>
        [ConfigurationProperty("key", IsRequired = true)]
        public string Key
        {
            get
            {
                return this["key"].ToString();
            }
            set
            {
                this["key"] = value;
            }
        }
        /// <summary>
        /// 值
        /// </summary>
        [ConfigurationProperty("value")]
        public string Value
        {
            get
            {
                return this["value"].ToString();
            }
            set
            {
                this["value"] = value;
            }

        }
    }
}