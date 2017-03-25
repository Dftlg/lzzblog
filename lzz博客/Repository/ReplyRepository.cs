using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using lzz博客.Models;
using System.Data.Entity;

namespace lzz博客.Repository
{
    public class ReplyRepository:RepositoryBase<Reply>
    {
        /// <summary>
        /// 添加评论
        /// </summary>
        /// <param name="Tmodel"></param>
        /// <returns></returns>
        public override bool Add(Reply reply)
        {
            dbContext.Replys.Add(reply);
            return dbContext.SaveChanges()>0;
        }
        /// <summary>
        /// 更新评论
        /// </summary>
        /// <param name="reply"></param>
        /// <returns></returns>
        public override bool Update(Reply reply)
        {
            dbContext.Entry<Reply>(reply).State = EntityState.Modified;
            if (dbContext.SaveChanges() > 0) return true;
            else return false;
        }
        /// <summary>
        /// 删除评论
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public override bool Delete(int Id)
        {
            dbContext.Replys.Remove(dbContext.Replys.SingleOrDefault(ry => ry.ReplyId == Id));

            return dbContext.SaveChanges()>0;
        }
        /// <summary>
        /// 回复列表
        /// </summary>
        /// <param name="Id">指定目录列表</param>
        /// <returns></returns>
        public IQueryable<Reply> List(int Id)
        {
            if(Id==0)
            {
                return dbContext.Replys.ToList().AsQueryable();
            }
            return dbContext.Replys.Where(ry => ry.AriticlesId == Id);
        }
        public Reply Findid(int id)
        {
            return dbContext.Replys.SingleOrDefault(ry => ry.ReplyId == id);   
        }
        
    }
}