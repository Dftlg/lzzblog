using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace lzz博客.Models
{
    public class JsonData
    {
        /// <summary>
        /// 操作成功
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        public string Msg { get; set; }
        /// <summary>
        /// 消息列表
        /// </summary>
        public Dictionary<string, string> MsgLsit { get; set; }
        public JsonData()
        {
            MsgLsit = new Dictionary<string, string>();
        }
    }
}