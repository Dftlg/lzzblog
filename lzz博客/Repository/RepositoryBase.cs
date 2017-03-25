using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Collections;
using System.Data.Entity.Core.Objects;

namespace lzz博客.Repository
{
    public class RepositoryBase<TModel>
    {    
        protected CMSContext dbContext;
      
        public RepositoryBase()
        {
            dbContext = new CMSContext();
        }
        /// <summary>
        /// 添加【继承类重写后才能正常使用】
        /// </summary>
        public virtual bool Add(TModel Tmodel) { return false; }
        /// <summary>
        /// 更新【继承类重写后才能正常使用】
        /// </summary>
        public virtual bool Update(TModel Tmodel) { return false; }
        /// <summary>
        /// 删除【继承类重写后才能正常使用】
        /// </summary>
        public virtual bool Delete(int Id) { return false; }
        /// <summary>
        /// 查找指定值【继承类重写后才能正常使用】
        /// </summary>
        public virtual TModel Find(int Id) { return default(TModel); }


        //public static void Detach<T>(this DbContext db, T obj) where T : class
        //{
        //    ObjectContext oc = ((IObjectContextAdapter)db).ObjectContext;
        //    oc.Detach(obj);
        //}

        ~RepositoryBase()
        {
            if (dbContext != null)
            {
                dbContext.Dispose();
            }
        }
    }
}