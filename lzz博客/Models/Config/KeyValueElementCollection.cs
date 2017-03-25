using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace lzz博客.Models.Config
{
    /// <summary>
    /// 元素集合类
    /// <remarks>
    /// 创建：2014.03.09
    /// </remarks>
    /// </summary>
    [ConfigurationCollection(typeof(KeyValueElement))]
    public class KeyValueElementCollection:ConfigurationElementCollection
    {
        //忽略大小写
        public KeyValueElementCollection() : base(StringComparer.OrdinalIgnoreCase) { }
        /// <summary>
        /// 集合中制定元素
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        new public KeyValueElement this[string name]    
        {
            get
            {
                return (KeyValueElement)base.BaseGet(name);
            }
            set
            {
                if (base.Properties.Contains(name))
                    base[name] = value;
                else
                    base.BaseAdd(value);
            }
        }
        /// <summary>
        /// 创建新元素
        /// </summary>
        /// <returns></returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new KeyValueElement();
        }
        /// <summary>
        /// 获取新元素
        /// </summary>
        /// <param name="elementName"></param>
        /// <returns></returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((KeyValueElement)element).Key;
        }

    }
}