using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace lzz博客.Models
{
    /// <summary>
    /// 附件
    /// </summary>
    public class Attachment
    {
        /// <summary>
        /// 附件ID
        /// </summary>
        [Key]
        public int AttachmentId { get; set; }
        public string Extension { get; set; }
        /// <summary>
        /// 路径名称
        /// </summary>
        public string FilePath { get; set; }
       
        /// <summary>
        /// 所有人
        /// </summary>
        public string Owner { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 上传日期
        /// </summary>
        public  DateTime UploadDate { get; set; }
        public int CommonModelId { get; set; }

    }
}