using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using lzz博客.Models;
using lzz博客.Repository;
using System.Data.Entity;
using lzz博客.Models.UI;
using System.Data.Entity.Infrastructure;
using System.Collections.Concurrent;
using System.Data.Entity.Core.Objects;


namespace lzz博客.Repository
{
    public class CategoryRepository:RepositoryBase<Category>
    {
        /// <summary>
        /// 添加栏目
        /// </summary>
        /// <param name="category">栏目</param>
        /// <returns></returns>
        public override bool Add(Category category)
        {
            dbContext.Categorys.Add(category);
            if (dbContext.SaveChanges() > 0) return true;
            else return false;
        }
        /// <summary>
        ///  更新栏目
        /// </summary>
        /// <param name="category">栏目</param>
        /// <returns></returns>
        public override bool Update(Category category)
        {
            RemoveHoldingEntityInContext(category);
            //dbContext.Categorys.Find(category.CategoryId);
            //int aFound = dbContext.Categorys.AsNoTracking().Where(ca => ca.CategoryId == category.CategoryId).Count();
            dbContext.Categorys.Attach(category);
            dbContext.Entry<Category>(category).State = EntityState.Modified;
            if (dbContext.SaveChanges() > 0) return true;
            else return false;
            //return (aFound > 0);
        }

        /// <summary>
        ///  删除栏目
        /// </summary>
        /// <param name="category">栏目</param>
        /// <returns></returns>
        public bool Delete(Category category)
        {
            dbContext.Categorys.Remove(category);
            if (dbContext.SaveChanges() > 0) return true;
            else return false;
        }
        /// <summary>
        /// 删除栏目
        /// </summary>
        /// <param name="CategoryId">栏目Id</param>
        /// <returns></returns>
        public override bool Delete(int CategoryId)
        {
            var _category = dbContext.Categorys.SingleOrDefault(c => c.CategoryId == CategoryId);
            if (_category == null) return false;
            else return Delete(_category);
        }
        /// <summary>
        /// 目标栏目是否是本身或下级栏目
        /// </summary>
        /// <param name="categoryId">栏目id</param>
        /// <param name="targetCategoryId">目标栏目id</param>
        /// <returns></returns>
        public bool IsSelfOrLower(int categoryId, int targetCategoryId)
        {
            if (categoryId == targetCategoryId) return true;
            if (targetCategoryId == 0) return false;
            return Find(targetCategoryId).ParentPath.IndexOf(Find(categoryId).ParentPath + "," + categoryId) == 0;
        }
        /// <summary>
        ///  查找制定栏目
        /// </summary>
        /// <param name="CategoryId">栏目id</param>
        /// <returns></returns>
        public override Category Find(int CategoryId)
        {
            return dbContext.Categorys.SingleOrDefault(c => c.CategoryId == CategoryId);
        }
        /// <summary>
        /// 获取跟栏目
        /// </summary>
        /// <returns></returns>
        public IQueryable<Category> Root()
        {
            return Children(0);
        }
        /// <summary>
        /// 获取子栏目
        /// </summary>
        /// <param name="CategoryId">栏目Id</param>
        /// <returns></returns>
        public IQueryable<Category> Children(int CategoryId)
        {
            return dbContext.Categorys.Where(c => c.ParentId == CategoryId).OrderBy(c => c.Order);
        }
        public IQueryable<Category> Children(int categoryId, int type)
        {
            return dbContext.Categorys.Where(c => c.ParentId == categoryId && c.Type == type).OrderBy(c => c.Order);
        }
        /// <summary>
        /// 栏目列表
        /// </summary>
        /// <param name="model">模型名称</param>
        /// <returns></returns>
        public IQueryable<Category> List(string model)
        {
            return dbContext.Categorys.Where(c => c.Model == model).OrderBy(c => c.Order);
        }
        public List<Tree>TreeGeneral()
        {
            var _root = Children(0).Select(c => new Tree { id = c.CategoryId, text = c.Name, iconCls = "icon-general" }).ToList();
            if (_root != null)
            {
                for (int i = 0; i < _root.Count(); i++)
                {
                    _root[i] = RecursionTreeGeneral(_root[i]);
                }
            }
            return _root;
        }

        private Tree RecursionTreeGeneral(Tree tree)
        {
            var _children = Children(tree.id).Select(c => new Tree { id = c.CategoryId, text = c.Name, iconCls = "icon-general" }).ToList();
            if (_children != null)
            {

                for (int i = 0; i < _children.Count(); i++)
                {
                    _children[i] = RecursionTreeGeneral(_children[i]);
                }
                tree.children = _children;
            }
            return tree;
        }
        /// <summary>
        /// 普通栏目类树形表
        /// </summary>
        /// <param name="model">模型名称</param>
        /// <returns></returns>
        public List<ZtreeNode> TreeGeneral(string model)
        {
            //查找所有类型为model的栏目
            List<ZtreeNode> _trees = new List<ZtreeNode>();//栏目树
            var _ctemp = dbContext.Categorys.AsNoTracking().Where(c => c.Model == model);
            Dictionary<int, Category> _categorys = new Dictionary<int, Category>();
            string _parentPath = string.Empty;
            foreach (var _c in _ctemp)
            {
                _parentPath += "," + _c.ParentPath;
                _categorys.Add(_c.CategoryId, _c);
            }
            //生成父栏目Id列表
            var _strParentIds = _parentPath.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            int _id;
            foreach (var _str in _strParentIds)
            {
                if (_str == "0") continue;
                if (int.TryParse(_str, out _id))
                {
                    if (!_categorys.ContainsKey(_id)) _categorys.Add(_id, Find(_id));
                }
            }
            var _root = _categorys.Values.Where(c => c.ParentId == 0).OrderBy(c => c.Order);
            ZtreeNode _tNode;
            foreach (var c in _root)
            {
                _tNode = new ZtreeNode { id = c.CategoryId, name = c.Name };
                if (c.Model == model) _tNode.iconSkin = "canadd";
                else _tNode.iconSkin = "cantadd";
                //子栏目
                if (_categorys.Values.Any(cg => cg.ParentId == _tNode.id))
                {
                    _tNode = TreeGeneralRecursion(model, _tNode, _categorys.Values.ToList());
                }
                _trees.Add(_tNode);
            }
            return _trees;
        }
        /// <summary>
        /// TreeGeneral(string model)的递归函数
        /// </summary>
        /// <param name = "model" > 模型名称 </ param >
        /// < param name="treeNode">节点</param>
        /// <param name = "source" > 数据源 </ param >
        private ZtreeNode TreeGeneralRecursion(string model, ZtreeNode treeNode, List<Category> source)
        {
            ZtreeNode _tNode;
            treeNode.children = new List<ZtreeNode>();
            var _children = source.Where(c => c.ParentId == treeNode.id).OrderBy(c => c.Order);
            foreach (var _c in _children)
            {
                _tNode = new ZtreeNode { id = _c.CategoryId, name = _c.Name };
                if (_c.Model == model) _tNode.iconSkin = "canadd";
                else _tNode.iconSkin = "cantadd";
                //子栏目
                if (source.Any(cg => cg.ParentId == _tNode.id))
                {
                    _tNode = TreeGeneralRecursion(model, _tNode, source);
                }
                treeNode.children.Add(_tNode);
            }
            return treeNode;
        }

        /// <summary>
        /// 更新栏目的ParentParth
        /// </summary>
        /// <param name="oldPath">原来的ParentParth</param>
        /// <param name="newPath">新的ParentParth</param>
        public bool UpdateCategorysParentPath(string oldPath, string newPath)
        {
            var _categorys = dbContext.Categorys.Where(c => c.ParentPath.IndexOf(oldPath) == 0);
            foreach (var _c in _categorys)
            {
                _c.ParentPath = newPath + _c.ParentPath.Remove(0, oldPath.Length);
            }
            return dbContext.SaveChanges() > 0;
        }

        private Boolean RemoveHoldingEntityInContext(Category entity) 
        {
            var objContext = ((IObjectContextAdapter)dbContext).ObjectContext;
            var objSet = objContext.CreateObjectSet<Category>();
            var entityKey = objContext.CreateEntityKey(objSet.EntitySet.Name, entity);

            Object foundEntity;
            var exists = objContext.TryGetObjectByKey(entityKey, out foundEntity);

            if (exists)
            {
                objContext.Detach(foundEntity);
            }

            return (exists);
        }

    }
}