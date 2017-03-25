using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using lzz博客.Models;
using lzz博客.Extensions;
using lzz博客.Common;
using lzz博客.Repository;
using lzz博客.Models.UI;
using lzz博客.Areas.Admin.Extensions;

namespace lzz博客.Controllers
{
    public class CategoryController : Controller
    {
        public CategoryController()
        {
            categoryRsy = new CategoryRepository();
            categoryRepository = new CategoryRepository(); 
        }
        private CategoryRepository categoryRsy;
        private CategoryRepository categoryRepository;

        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 菜单
        /// </summary>
        /// <returns>局部视图</returns>
        public PartialViewResult Menu()
        {
            return PartialView();
        }
        /// <summary>
        /// 添加栏目
        /// </summary>
        /// <returns>局部视图</returns>
        [AdminAuthorize]
        public PartialViewResult Add()
        {
            return PartialView(new Category { CategoryView = "Index", ContentOrder = 0, ContentView = "Detail", Model = "", Order = 0, PageSize = 20, ParentId = 0, RecordName = "文章", RecordUnit = "篇", Type = 0 });
        }

        /// <summary>
        /// ManageAdd默认视图
        /// </summary>
        /// <returns></returns>
       [AdminAuthorize]
        public PartialViewResult ManageAdd()
        {
            ModuleRepository _moduleRsy = new ModuleRepository();
            var _modules = _moduleRsy.List(true);
            List<SelectListItem> _slimodule = new List<SelectListItem>(_modules.Count());
            _slimodule.Add(new SelectListItem { Text = "无", Value = "" });
            foreach (Module _module in _modules)
            {
                _slimodule.Add(new SelectListItem { Text = _module.Name, Value = _module.Model });
            }
            ViewData.Add("Model", _slimodule);
            ViewData.Add("Type", TypeSelectList);
            ViewData.Add("ContentOrders", CommonModel.ContentOrders);
            return PartialView();
        }
        /// <summary>
        /// ManageAdd添加视图
        /// </summary>
        /// <returns></returns>
        //[AdminAuthorize]
        [HttpPost]
        [AdminAuthorize]
        public /*PartialViewResult*/ ActionResult ManageAdd(Category category)
        {
            //父栏目是否存在
            //if (categoryRsy.Find(category.ParentId) == null) ModelState.AddModelError("ParentId", "父栏目不存在。");
            //ParentPath
            ModuleRepository _moduleRsy = new ModuleRepository();
            var _modules = _moduleRsy.List(true);
            List<SelectListItem> _slimodule = new List<SelectListItem>(_modules.Count());
            _slimodule.Add(new SelectListItem { Text = "无", Value = "" });
            foreach (Module _module in _modules)
            {
                _slimodule.Add(new SelectListItem { Text = _module.Name, Value = _module.Model });
            }
            ViewData.Clear();
            ViewData.Add("Model", _slimodule);
            ViewData.Add("Type", TypeSelectList);
            ViewData.Add("ContentOrders", CommonModel.ContentOrders);
            if (category.ParentId == 0) category.ParentPath = "0";
            else category.ParentPath = categoryRsy.Find(category.ParentId).ParentPath + "," + category.ParentId;
            switch (category.Type)
            {
                case 0://常规栏目
                    if (string.IsNullOrEmpty(category.CategoryView)) ModelState.AddModelError("CategoryView", "×");
                    category.Navigation = null;
                    if (!string.IsNullOrEmpty(category.Model))
                    {
                        if (string.IsNullOrEmpty(category.ContentView)) ModelState.AddModelError("ContentView", "×");
                        if (category.ContentOrder == null) category.ContentOrder = 0;
                        if (category.PageSize == null) category.PageSize = 20;
                        if (string.IsNullOrEmpty(category.RecordUnit)) category.RecordUnit = "条";
                        if (string.IsNullOrEmpty(category.RecordName)) category.RecordName = "记录";
                    }
                    else
                    {
                        category.ContentView = null;
                        category.ContentOrder = null;
                        category.PageSize = null;
                        category.RecordUnit = null;
                        category.RecordName = null;
                    }
                    break;
                case 1://单页栏目
                    if (string.IsNullOrEmpty(category.CategoryView)) ModelState.AddModelError("CategoryView", "×");
                    category.Navigation = null;
                    category.ContentView = null;
                    category.ContentOrder = null;
                    category.PageSize = null;
                    category.RecordUnit = null;
                    category.RecordName = null;
                    break;
                case 2://外部链接
                    if (string.IsNullOrEmpty(category.Navigation)) ModelState.AddModelError("LinkUrl", "×");
                    category.CategoryView = null;
                    category.ContentView = null;
                    category.ContentOrder = null;
                    category.PageSize = null;
                    category.RecordUnit = null;
                    category.RecordName = null;
                    break;
                default:
                    ModelState.AddModelError("Type", "×");
                    break;
            }
            if (ModelState.IsValid)
            {
                if (categoryRsy.Add(category))
                {
                    Notice _n = new Notice { Title = "添加栏目成功", Details = "您已经成功添加[" + category.Name + "]栏目！", DwellTime = 5, NavigationName = "栏目列表", NavigationUrl = Url.RouteUrl("Admin_default", new { controller="Home",action="Index"}) };
                    //return RedirectToAction("ManageNotice", "Prompt", _n);
                    //Response.Write("<script>location.href='/Prompt/ManageNotice?DwellTime = 5'</script>");
                    return RedirectToAction("ManageNotice", "Prompt", _n);

                }
                else
                {
                    Error _e = new Error { Title = "添加栏目失败", Details = "在添加栏目时，未能保存到数据库", Cause = "系统错误", Solution = Server.UrlEncode("<li>返回<a href='" + Url.RouteUrl("Admin_default", new { controller = "Home", action = "Index" }) + "'>添加栏目</a>页面，输入正确的信息后重新操作</li><li>联系网站管理员</li>") };
                    return RedirectToAction("ManageError", "Prompt", _e);
                    //Response.Write("<script>location.href='/Prompt/ManageError?" + _e + "'</script>");
                    //return PartialView();
                }
            }
            else
            {
                //ModuleRepository _moduleRsy = new ModuleRepository();
                //var _modules = _moduleRsy.List(true);
                //List<SelectListItem> _slimodule = new List<SelectListItem>(_modules.Count());
                _slimodule.Add(new SelectListItem { Text = "无", Value = "" });
                foreach (Module _module in _modules)
                {
                    _slimodule.Add(new SelectListItem { Text = _module.Name, Value = _module.Model });
                }
                ViewData.Add("Model", _slimodule);
                ViewData.Add("Type", TypeSelectList);
                ViewData.Add("ContentOrders", CommonModel.ContentOrders);
                return PartialView(category);
                //return View(category);
            }
            
        
    }
        /// <summary>
        /// 栏目列表局部树视图
        /// </summary>
        /// <returns></returns>
        public /*PartialViewResult*/ ActionResult ManagePartialTree()
        {
            //return View();
                       
            return PartialView();
        }
        /// <summary>
        /// 栏目详细资料
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AdminAuthorize]
        public ActionResult ManageDetails(int id)
        {
            categoryRsy = new CategoryRepository();
            var _node = categoryRsy.Find(id);
            if (_node == null)
            {
                Error _e = new Error { Title = "栏目不存在", Details = "栏目不存在", Cause = Server.UrlEncode("<li>栏目已经删除</li>"), Solution = Server.UrlEncode("<li>返回<a href='" + Url.Action("Manage", "Cayegory") + "'>栏目栏目管理</a></li>") };
                return RedirectToAction("ManageError", "Prompt", _e);
            }
            ModuleRepository _moduleRsy = new ModuleRepository();
            var _modules = _moduleRsy.List(true);
            List<SelectListItem> _slimodule = new List<SelectListItem>(_modules.Count());
            foreach (Module _module in _modules)
            {
                if (_node.Model == _module.Model) _slimodule.Add(new SelectListItem { Text = _module.Name, Value = _module.Model, Selected = true });
                else _slimodule.Add(new SelectListItem { Text = _module.Name, Value = _module.Model });
            }
            ViewData.Add("Model", _slimodule);
            var _type = TypeSelectList;
            _type.SingleOrDefault(t => t.Value == _node.Type.ToString()).Selected = true;
            ViewData.Add("Type", _type);
            return PartialView(_node);
        }
        /// <summary>
        /// 管理栏目列表
        /// </summary>
        /// <returns></returns>
        public ActionResult Manage()
        {
            return View();
        }

        /// <summary>
        /// 修改栏目信息
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        [AdminAuthorize]
        public ActionResult ManageUpdate(Category category)
        {
            //父栏目不能为本身或子栏目
            if (categoryRsy.IsSelfOrLower(category.CategoryId, category.ParentId)) ModelState.AddModelError("ParentId", "父栏目不能是其本身或其子栏目");
            switch (category.Type)
            {
                case 0://常规栏目
                    if (string.IsNullOrEmpty(category.CategoryView)) ModelState.AddModelError("CategoryView", "×");
                    category.Navigation = null;
                    if (!string.IsNullOrEmpty(category.Model))
                    {
                        if (string.IsNullOrEmpty(category.ContentView)) ModelState.AddModelError("ContentView", "×");
                        if (category.ContentOrder == null) category.ContentOrder = 0;
                        if (category.PageSize == null) category.PageSize = 20;
                        if (string.IsNullOrEmpty(category.RecordUnit)) category.RecordUnit = "条";
                        if (string.IsNullOrEmpty(category.RecordName)) category.RecordName = "记录";
                    }
                    else
                    {
                        category.ContentView = null;
                        category.ContentOrder = null;
                        category.PageSize = null;
                        category.RecordUnit = null;
                        category.RecordName = null;
                    }
                    break;
                case 1://单页栏目
                    if (string.IsNullOrEmpty(category.CategoryView)) ModelState.AddModelError("CategoryView", "×");
                    category.Navigation = null;
                    category.ContentView = null;
                    category.ContentOrder = null;
                    category.PageSize = null;
                    category.RecordUnit = null;
                    category.RecordName = null;
                    break;
                case 2://外部链接
                    if (string.IsNullOrEmpty(category.Navigation)) ModelState.AddModelError("LinkUrl", "×");
                    category.CategoryView = null;
                    category.ContentView = null;
                    category.ContentOrder = null;
                    category.PageSize = null;
                    category.RecordUnit = null;
                    category.RecordName = null;
                    break;
                default:
                    ModelState.AddModelError("Type", "×");
                    break;
            }
            if (ModelState.IsValid)
            {
                var _pId = categoryRsy.Find(category.CategoryId).ParentId;
                var _oldParentPath = categoryRsy.Find(category.CategoryId).ParentPath + "," + category.CategoryId;
                //父栏目发生更改
                if (category.ParentId != _pId)
                {
                    //ParentPath
                    if (category.ParentId == 0) category.ParentPath = "0";
                    else category.ParentPath = categoryRsy.Find(category.ParentId).ParentPath + "," + category.ParentId;
                }
                if (categoryRsy.Update(category))
                {
                    Notice _n = new Notice { Title = "修改栏目成功", Details = "修改栏目成功！", DwellTime = 5, NavigationName = "栏目详细信息", NavigationUrl = Url.RouteUrl("Admin_default", new { controller = "Home", action = "Index" }) };
                    if (_oldParentPath != category.ParentPath)
                    {
                        //修改子栏目ParentPath
                        categoryRsy.UpdateCategorysParentPath(_oldParentPath, category.ParentPath + "," + category.CategoryId);
                    }
                    return RedirectToAction("ManageNotice", "Prompt", _n);
                }
                else
                {
                    Error _e = new Error { Title = "修改栏目失败", Details = "在修改栏目信息时，未能保存到数据库", Cause = "系统错误", Solution = Server.UrlEncode("<li>返回<a href='" + Url.RouteUrl("Admin_default", new { controller = "Home", action = "Index" }) + "'>栏目详细资料</a>页面，修改信息后重新操作</li><li>联系网站管理员</li>") };
                    return RedirectToAction("ManageError", "Prompt", _e);
                }
            }
            else
            {
                ModuleRepository _moduleRsy = new ModuleRepository();
                var _modules = _moduleRsy.List(true);
                List<SelectListItem> _slimodule = new List<SelectListItem>(_modules.Count());
                _slimodule.Add(new SelectListItem { Text = "无", Value = "" });
                foreach (Module _module in _modules)
                {
                    _slimodule.Add(new SelectListItem { Text = _module.Name, Value = _module.Model });
                }
                ViewData.Add("Model", _slimodule);
                ViewData.Add("Type", TypeSelectList);
                ViewData.Add("ContentOrders", CommonModel.ContentOrders);
                //return View("ManageDetails", category);
                return PartialView("ManageDetails", category);
            }
        }
       /// <summary>
       /// 删除栏目信息
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
        [AdminAuthorize]
        public JsonResult ManageDeleteJson(int id)
        {
            categoryRsy = new CategoryRepository();
            if (categoryRsy.Children(id).Count() > 0) return Json(false);
            return Json(categoryRsy.Delete(id));
        }
        /// <summary>
        /// 索引
        /// </summary>
        /// <param name="id">栏目id</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(int id)
        {
            var _category = categoryRsy.Find(id);
            if (_category == null)
            {
                Error _e = new Error { Title = "错误", Details = "指定的栏目不存在", Cause = "你访问的栏目已经删除", Solution = Server.UrlEncode("<li>返回<a href='" + Url.Action("Index", "Home") + "'>网站首页</a></li>") };
                return RedirectToAction("Error", "Prompt", _e);
            }
            if (_category.Type == 2) return Redirect(_category.Navigation);
            return View(_category.CategoryView, _category);
        }
        /// <summary>
        /// 根栏目
        /// </summary>
        /// <returns></returns>
        public ActionResult PartialRoot()
        {
            return View(categoryRsy.Root());
        }
        /// <summary>
        /// 子栏目
        /// </summary>
        /// <param name="id">栏目id</param>
        /// <returns></returns>
        public ActionResult PartialChildren(int id)
        {
            return View(categoryRsy.Children(id));
        }
        /// <summary>
        /// 栏目路径
        /// </summary>
        /// <param name="id">当前栏目Id</param>
        /// <returns></returns>
        public ActionResult PartialPath(int id)
        {
            List<Category> _path = new List<Category>();
            var _category = categoryRsy.Find(id);
            while (_category != null)
            {
                _path.Insert(0, _category);
                _category = categoryRsy.Find(_category.ParentId);
            }
            return View(_path);
        }
        //[HttpPost]
        public ActionResult JsonUserGeneralTree(string model)
        {
            return Json(categoryRsy.TreeGeneral(model));
        }
        /// <summary>
        /// 子栏目树形控件Json数据
        /// </summary>
        /// <param name="id">栏目id</param>
        /// <returns></returns>
        //[AdminAuthorize]
        public JsonResult ManageTreeChildrenJson(int id = 0)
        {
            categoryRsy = new CategoryRepository();
            var _children = categoryRsy.Children(id);           
            List<Tree> _trees = new List<Tree>(_children.Count());
            foreach (var c in _children)
            {
                Tree _t = new Tree { id = c.CategoryId, text = c.Name };
                switch (c.Type)
                {
                    case 0:
                        _t.state = "closed";
                        _t.iconCls = "icon-general";
                        break;
                    case 1:
                        _t.state = "open";
                        _t.iconCls = "icon-page";
                        break;
                    case 2:
                        _t.state = "open";
                        _t.iconCls = "icon-link";
                        break;
                }
                _trees.Add(_t);
            }
            return Json(_trees, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 栏目导航控件Json数据
        /// </summary>
        /// <returns></returns>
        public JsonResult JsonMetisMenu(int id=0)
        {
            categoryRsy = new CategoryRepository();
            var _children = categoryRsy.Children(id);           
            List<Tree> _trees = new List<Tree>(_children.Count());
            foreach (var c in _children)
            {
                Tree _t = new Tree { id = c.CategoryId, text = c.Name };
                if (categoryRsy.Children(c.CategoryId).Count() == 0)
                {
                    _t.state = "false";
                }
                else
                {
                    _t.state = "true";
                }               
                _trees.Add(_t);
            }
            return Json(_trees, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 返回父栏目的栏目树列表
        /// </summary>
        /// <returns></returns>
        //[AdminAuthorize]
        public JsonResult JsonTreeParent()
        {
            categoryRsy = new CategoryRepository();
            var _children = categoryRsy.TreeGeneral();
            if (_children == null) _children = new List<Tree>();
            _children.Insert(0, new Tree { id = 0, text = "无", iconCls = "icon-general" });
            return Json(_children);
        }
        public List<SelectListItem> TypeSelectList
        {
            get
            {
                List<SelectListItem> _items = new List<SelectListItem>();
                _items.Add(new SelectListItem { Text = CategoryType.一般栏目.ToString(), Value = ((int)CategoryType.一般栏目).ToString() });
                _items.Add(new SelectListItem { Text = CategoryType.单页栏目.ToString(), Value = ((int)CategoryType.单页栏目).ToString() });
                _items.Add(new SelectListItem { Text = CategoryType.外部链接.ToString(), Value = ((int)CategoryType.外部链接).ToString() });
                return _items;
            }
        }
    



    }
}