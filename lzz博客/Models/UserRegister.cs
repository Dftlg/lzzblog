using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using lzz博客.Common;

namespace lzz博客.Models
{
    public class UserRegister:User
    {
        /// <summary>
        /// 密码
        /// </summary>
        [Display(Name = "密码", Description = "6-20个字符。")]
        [Required(ErrorMessage = "×")]
        [StringLength(80, MinimumLength = 6, ErrorMessage = "×")]
        [DataType(DataType.Password)]
        public new string Password { get; set; }
        /// <summary>
        /// 确认密码
        /// </summary>
        [Display(Name = "确认密码", Description = "再次输入密码。")]
        [Compare("Password", ErrorMessage = "×")]
        [DataType(DataType.Password)]
        public string RePassword { get; set; }
        /// <summary>
        /// 验证码
        /// </summary>
        [Display(Name = "验证码", Description = "请输入图片中的验证码。")]
        [Required(ErrorMessage = "×")]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "×")]
        public string VerificationCode { get; set; }
        public User GetUser()
        {
            return new User { Address = this.Address, Email = this.Email, Gender = this.Gender, GroupId = this.GroupId, Password = RandomText.Sha256(this.Password), PostCode = this.PostCode, QQ = this.QQ, RegTime = this.RegTime, SecurityAnswer = this.SecurityAnswer, SecurityQuestion = this.SecurityQuestion, Tel = this.Tel, UserName = this.UserName };
        }
    }
}