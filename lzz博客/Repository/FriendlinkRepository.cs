using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using lzz博客.Models;
using System.Data.Entity;

namespace lzz博客.Repository
{
    public class FriendlinkRepository:RepositoryBase<FriendLink>
    {
        /// <summary>
        ///添加友链
        /// </summary>
        /// <param name="article">友链</param>
        /// <returns></returns>
        public override bool Add(FriendLink friendlink)
        {
            dbContext.FriendLinks.Add(friendlink);
            return dbContext.SaveChanges() > 0;
        }
        /// <summary>
        /// 更新友链
        /// </summary>
        /// <param name="article">友链</param>
        /// <returns></returns>
        public override bool Update(FriendLink friendlink)
        {
            dbContext.FriendLinks.Attach(friendlink);
            dbContext.Entry<FriendLink>(friendlink).State = EntityState.Modified;           
            return dbContext.SaveChanges() > 0;
        }
        /// <summary>
        /// 删除友链
        /// </summary>
        /// <param name="FriendLinkId">FriendLinkid</param>
        /// <returns></returns>
        public override bool Delete(int FriendLinkId)
        {
            dbContext.FriendLinks.Remove(dbContext.FriendLinks.SingleOrDefault(fl => fl.Linkid == FriendLinkId));
            return dbContext.SaveChanges() > 0;
        }

        /// <summary>
        ///  查找制定友链
        /// </summary>
        /// <param name="friendlinkid">友链id</param>
        /// <returns></returns>
        public override FriendLink Find(int friendlinkid)
        {
            return dbContext.FriendLinks.SingleOrDefault(fl => fl.Linkid == friendlinkid);
        }
        public List<FriendLink> List()
        {
            return dbContext.FriendLinks.ToList();
        }
    }
}