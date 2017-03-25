using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace lzz博客.Models
{
    public class UserChangePassword
    {
        [Display(Name = "原密码",Description ="原密码")]
        [Required(ErrorMessage = "×")]
        [StringLength(80, MinimumLength = 6, ErrorMessage = "×")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        /// <summary>
        /// 新密码
        /// </summary>
        [Display(Name = "新密码", Description = "6-20个字符。")]
        [Required(ErrorMessage = "×")]
        [StringLength(80, MinimumLength = 6, ErrorMessage = "×")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        /// <summary>
        /// 确认密码
        /// </summary>
        [Display(Name = "确认密码", Description = "再次输入密码。")]
        [Compare("NewPassword", ErrorMessage = "×")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}