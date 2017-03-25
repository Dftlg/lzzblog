using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using lzz博客.Models;
using System.Data.Entity;
using System.Web.Mvc;

namespace lzz博客.Repository
{
    public class ArticleRepository:RepositoryBase<Article>
    {
        /// <summary>
        ///添加文章
        /// </summary>
        /// <param name="article">文章</param>
        /// <returns></returns>
        public override bool Add(Article article)
        {
            dbContext.Articles.Add(article);
            return dbContext.SaveChanges() > 0;
        }
        /// <summary>
        /// 更新文章
        /// </summary>
        /// <param name="article">文章</param>
        /// <returns></returns>
        public override bool Update(Article article)
        {
            dbContext.Articles.Attach(article);
            dbContext.Entry<Article>(article).State = EntityState.Modified;
            dbContext.Entry<CommonModel>(article.CommonModel).State = EntityState.Modified;
            return dbContext.SaveChanges() > 0;
        }
        /// <summary>
        /// 删除文章
        /// </summary>
        /// <param name="commonModelId">公共模型id</param>
        /// <returns></returns>
        public override bool Delete(int commonModelId)
        {
            dbContext.CommonModels.Remove(dbContext.CommonModels.SingleOrDefault(cM => cM.CommonModelId == commonModelId));
            dbContext.Articles.Remove(dbContext.Articles.SingleOrDefault(a => a.CommonModelId == commonModelId));
            return dbContext.SaveChanges() > 0;
        }
        /// <summary>
        /// 查找文章
        /// </summary>
        /// <param name="Id">文章id</param>
        /// <returns></returns>
        public override Article Find(int Id)
        {
            return dbContext.Articles.AsNoTracking().Include("CommonModel").SingleOrDefault(a => a.ArticleId == Id);
        }
        /// <summary>
        /// 根据公共模型id查找文章
        /// </summary>
        /// <param name="commonModelId">公共模型Id</param>
        /// <returns>文章</returns>
        public Article FindByCommonModelId(int commonModelId)
        {
            //return dbContext.Articles.AsNoTracking().Include("CommonModel").SingleOrDefault(a => a.CommonModelId == commonModelId);
           Article Art =dbContext.Articles.AsNoTracking().Include("CommonModel").SingleOrDefault(a => a.CommonModelId == commonModelId);
            AddHit(Art.CommonModel);
            return Art;
        }
        public bool AddHit(CommonModel commonMode)
        {
            commonMode.Hits++;
             dbContext.CommonModels.Attach(commonMode);
            dbContext.Entry<CommonModel>(commonMode).State = EntityState.Modified;
            return dbContext.SaveChanges() > 0;

        }
        /// <summary>
        /// 获取分页公共模型内容列表
        /// </summary>
        /// <param name="categoryId">栏目Id</param>
        /// <param name="cChildren">是否包含子栏目</param>
        /// <param name="model">模型名称</param>
        /// <param name="userName">用户名</param>
        /// <param name="currentPage">当前页</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="order">排序方式</param>
        /// <returns>分页数据</returns>
        public PagerData<Article> List(int categoryId, bool cChildren, string model, string userName, int currentPage, int pageSize, int order)
        {
            PagerConfig _pConfig = new PagerConfig { CurrentPage = currentPage, PageSize = pageSize };
            var _cModels = dbContext.CommonModels.Include("Category").AsQueryable();
            //var _artmod = dbContext.Articles.Include("CommonModel").AsQueryable();
            IQueryable<Article> _artne=null;/*=dbContext.Articles.Include("CommonModel").AsQueryable()*/
                       
            if (categoryId != 0)
            {
                if (cChildren)//包含子栏目
                {
                    CategoryRepository _cRsy = new CategoryRepository();
                    IQueryable<int> _children = _cRsy.Children(categoryId, 0).Select(c => c.CategoryId);
                    _cModels = _cModels.Where(m => _children.Contains(m.CategoryId));
                }
                else _cModels = _cModels.Where(m => m.CategoryId == categoryId);//不包含子栏目
            }
            if (!string.IsNullOrEmpty(model)) _cModels = _cModels.Where(m => m.Model == model);
            if (!string.IsNullOrEmpty(userName)) _cModels = _cModels.Where(m => m.Inputer == userName);
            _pConfig.TotalRecord = _cModels.Count();//总记录数
            //排序
            switch (order)
            {
                case 1://id降序
                    _cModels = _cModels.OrderByDescending(m => m.CommonModelId);
                    break;
                case 2://Id升序
                    _cModels = _cModels.OrderBy(m => m.CommonModelId);
                    break;
                case 3://发布日期降序
                    _cModels = _cModels.OrderByDescending(m => m.ReleaseDate);
                    break;
                case 4://发布日期升序
                    _cModels = _cModels.OrderBy(m => m.ReleaseDate);
                    break;
                case 5://点击降序
                    _cModels = _cModels.OrderByDescending(m => m.Hits);
                    break;
                case 6://点击升序
                    _cModels = _cModels.OrderBy(m => m.Hits);
                    break;
                default://默认id降序
                    _cModels = _cModels.OrderByDescending(m => m.CommonModelId);
                    break;
            }
            //分页
            _cModels = _cModels.Skip((_pConfig.CurrentPage - 1) * _pConfig.PageSize).Take(_pConfig.PageSize);
            int k = 0;                
            foreach (var i in _cModels)
            {
                IQueryable<Article> _artmod = dbContext.Articles.Include("CommonModel").AsQueryable();
                if (k==0)
                {
                    _artne=_artmod.Where(m => m.CommonModelId == i.CommonModelId);
                    k++;
                }
                else
                {
                    _artmod = _artmod.Where(m => m.CommonModelId == i.CommonModelId);
                   _artne=_artne.Concat(_artmod);
                    k++;
                }
                /*(IEnumerable<Article>)*/
                
                //_artmod.Concat(
            }                 
            PagerData<Article> _pData = new PagerData<Article>(_artne, _pConfig);
            return _pData;
        }
        

    }
}