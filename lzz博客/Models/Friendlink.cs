using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace lzz博客.Models
{
    public class FriendLink
    {
        /// <summary>
        /// LinkId
        /// </summary>
        [Key]
        [Display(Name="LinkId")]
        public int Linkid { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        [Display(Name ="Url", Description="完整url")]
        [Required(ErrorMessage ="The url is required")]
        [RegularExpression(@"((http|ftp|https)://)(([a-zA-Z0-9\._-]+\.[a-zA-Z]{2,6})|([0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}))(:[0-9]{1,4})*(/[a-zA-Z0-9\&%_\./-~-]*)?",ErrorMessage ="Email doesn't look like a valid email address.")]
        [StringLength(100)]
        public string Href { get; set; }
        /// <summary>
        /// 显示名称
        /// </summary>
        [Display(Name ="Name",Description ="The ower of this url")]
        [Required(ErrorMessage ="The Name is required")]
        public string Name { get; set; }
    }
}