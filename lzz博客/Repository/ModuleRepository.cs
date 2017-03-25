using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using lzz博客.Models;
using lzz博客.Common;

namespace lzz博客.Repository
{
    public class ModuleRepository
    {
        public IQueryable<Module> List(bool enable)
        {
            List<Module> _module = new List<Module>();
            _module.Add(new Module { Name = "新闻模块", Model = "News", Enable = true, Description = "新闻模块" });
            _module.Add(new Module { Name = "文章模块", Model = "Article", Enable = true, Description = "文章模块" });
            return _module.AsQueryable();
        }
    }
}