using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace lzz博客.Models
{
    public class UserGroup
    {
        [Key]
        public int UserGroupId { get; set; }
        /// <summary>
        /// 用户组类型【True-系统，False-用户定义。】
        /// </summary>
        [Display(Name = "用户组类型")]
        [Required(ErrorMessage = "×")]
        public UserGroupType Type { get; set; }
        /// <summary>
        /// 用户组名称
        /// </summary>
        [Display(Name = "用户组名称", Description = "2-12个字符。")]
        [Required(ErrorMessage = "×")]
        [StringLength(12, MinimumLength = 2, ErrorMessage = "×")]
        public string Name { get; set; }
        /// <summary>
        /// 说明
        /// </summary>
        [Display(Name = "说明", Description = "最多50个字符。")]
        [StringLength(50, ErrorMessage = "×")]
        public string Description { get; set; }
    }
    public enum UserGroupType
    {
        Anonymous, Limited, Normal, Special
    }
}