using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace lzz博客.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        /// <summary>
        /// 用户组Id
        /// </summary>
        [Display(Name = "用户组Id")]
        [Required(ErrorMessage = "×")]
        public int GroupId { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        [Display(Name = "用户名", Description = "4-20个字符。")]
        [Required(ErrorMessage = "×")]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "×")]
        public string UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [Required]
        [StringLength(80, ErrorMessage = "×")]
        public string Password { get; set; }
        /// <summary>
        /// 性别【0-男；1-女；2-保密】
        /// </summary>
        [Display(Name = "性别")]
        [Required(ErrorMessage = "×")]
        [Range(0, 2, ErrorMessage = "×")]
        public byte Gender { get; set; }
        /// <summary>
        /// Email
        /// </summary>
        [Display(Name = "Email", Description = "请输入您常用的Email。")]
        [Required(ErrorMessage = "×")]
        [EmailAddress(ErrorMessage = "×")]
        public string Email { get; set; }
        /// <summary>
        /// 密保问题
        /// </summary>
        [Display(Name = "密保问题", Description = "请正确填写，在您忘记密码时用户找回密码。4-20个字符。")]
        [Required(ErrorMessage = "×")]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "×")]
        public string SecurityQuestion { get; set; }
        /// <summary>
        /// 密保答案
        /// </summary>
        [Display(Name = "密保答案", Description = "请认真填写，忘记密码后回答正确才能找回密码。2-20个字符。")]
        [Required(ErrorMessage = "×")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "×")]
        public string SecurityAnswer { get; set; }
        /// <summary>
        /// QQ号码
        /// </summary>
        [Display(Name = "QQ号码")]
        [RegularExpression("[1-9][0-9]{5,12}", ErrorMessage = "6到13位有效数字")]
        [StringLength(13, MinimumLength = 6, ErrorMessage = "6到13位有效数字")]
        public string QQ { get; set; }
        /// <summary>
        /// 电话号码
        /// </summary>
        [Display(Name = "电话号码", Description = "常用的联系电话（手机或固话），固话格式为：区号-号码。")]
        [RegularExpression("[0-9]{11,13}", ErrorMessage = "11到13位有效数字")]
        public string Tel { get; set; }
        /// <summary>
        /// 联系地址
        /// </summary>
        [Display(Name = "联系地址", Description = "常用地址，最多80个字符。")]
        [StringLength(80, ErrorMessage = "×")]
        public string Address { get; set; }
        /// <summary>
        /// 邮编
        /// </summary>
        [Display(Name = "邮编")]
        [RegularExpression("^[0-9]{6}$", ErrorMessage = "6位有效数字")]
        public string PostCode { get; set; }
        /// <summary>
        /// 注册时间
        /// </summary>
        public DateTime? RegTime { get; set; }
        /// <summary>
        /// 上次登录时间
        /// </summary>
        public DateTime? LastLoginTime { get; set; }

        /// <summary>
        /// 用户组
        /// </summary>
        //public virtual UserGroup Group { get; set; }
        
    }
}