using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace lzz博客.Models
{
    public class Reply
    {
        /// <summary>
        /// 回复Id
        /// </summary>
        [Key]
        public int ReplyId { get; set; }
        /// <summary>
        /// 回复内容
        /// </summary>
        [Display(Name ="内容")]
        [Required(ErrorMessage ="Can't be null")]
        public string ReplyContent { get; set; }
        /// <summary>
        /// 回复日期
        /// </summary>
        [Display(Name ="评论日期")]
        [Required(ErrorMessage = "×")]
        public DateTime ReplyTime { get; set; }
        /// <summary>
        /// 文章Id
        /// </summary>
        public int AriticlesId { get; set; }
        /// <summary>
        /// 用户
        /// </summary>
        public string User { get; set; }
       
        //public virtual CommonModel CommonModel { get; set; }

    }
}