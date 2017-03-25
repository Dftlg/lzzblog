using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using lzz博客.Models;
using lzz博客.Repository;
using lzz博客.Extensions;
using lzz博客.Areas.Admin.Extensions;

namespace lzz博客.Controllers
{
    public class ArticleController : Controller
    {
        // GET: Article
        ArticleRepository articleRsy;
        CommonModelRepository cModelRsy;
        ArticleRepository articleview;
        public ArticleController()
        {
            articleRsy = new ArticleRepository();
        }
        /// <summary>
        /// 用户默认页
        /// </summary>
        //[UserAuthorize]
        public ActionResult UserDefault()
        {
            return View();
        }
        /// <summary>
        /// 菜单
        /// </summary>
        /// <returns>局部视图</returns>
        [AdminAuthorize]
        public PartialViewResult ArticleMenu()
        {
            return PartialView();
        }
        /// <summary>
        /// 添加文章
        /// </summary>
       // [UserAuthorize]
        [AdminAuthorize]
        public ActionResult UserAdd()
        {
            return PartialView(new Article() { CommonModel = new CommonModel() });
        }
        [AdminAuthorize]
        [HttpPost]
        //[UserAuthorize]
        [ValidateInput(false)]
        public ActionResult UserAdd(Article article)
        {
            //验证栏目
            CategoryRepository _categoryRsy = new CategoryRepository();
            var _category = _categoryRsy.Find(article.CommonModel.CategoryId);
            if (_category == null) ModelState.AddModelError("CommonModel.CategoryId", "栏目不存在");
            if (_category.Model != "Article") ModelState.AddModelError("CommonModel.CategoryId", "该栏目不能添加文章！");
            article.CommonModel.Inputer = UserController.UserName;
            ModelState.Remove("CommonModel.Inputer");
            article.CommonModel.Model = "Article"; 
            ModelState.Remove("CommonModel.Model");
            if (ModelState.IsValid)
            {
                if (articleRsy.Add(article))
                {
                    Notice _n = new Notice { Title = "添加文章成功", Details = "您已经成功添加[" + article.CommonModel.Title + "]文章！", DwellTime = 5, NavigationName = "我的文章", NavigationUrl = Url.RouteUrl("Admin_default",new { controller="Home",action="Index"}) };
                    return RedirectToAction("UserNotice", "Prompt", _n);
                }
                else
                {
                    Error _e = new Error { Title = "添加文章失败", Details = "在添加文章时，未能保存到数据库", Cause = "系统错误", Solution = Server.UrlEncode("<li>返回<a href='" + Url.RouteUrl("Admin_default",new {controller="Home",action="Index"}) + "'>添加文章</a>页面，输入正确的信息后重新操作</li><li>返回<a href='" + Url.Action("UserDefault", "Article") + "'>文章管理首页</a>。</li><li>联系网站管理员</li>") };
                    return RedirectToAction("ManageError", "Prompt", _e);
                }
            }
            return PartialView(article);
        }

        /// <summary>
        /// 我的文章
        /// </summary>
        /// <param name="id">栏目id</param>
        /// <param name="page">页号</param>
       // [UserAuthorize]
        [AdminAuthorize]
        public ActionResult UserOwn(int id = 0, int page = 1)
        {
            int _pageSize = 20;
            int _cOrder = 0;
            Category _c = null;
            cModelRsy = new CommonModelRepository();
            PagerData<CommonModel> _aData;
            if (id > 0)
            {
                var _cRsy = new CategoryRepository();
                _c = _cRsy.Find(id);
                if (_c != null)
                {
                    _pageSize = (int)_c.PageSize;
                    _cOrder = (int)_c.ContentOrder;
                }
            }
            _aData = cModelRsy.List(id, false, "Article", UserController.UserName, page, _pageSize, _cOrder);
            if (_c != null)
            {
                _aData.Config.RecordName = _c.RecordName;
                _aData.Config.RecordUnit = _c.RecordUnit;
            }
            return View(_aData);
        }
        /// <summary>
        /// 修改文章
        /// </summary>
        /// <param name="id">公共模型id</param>
        //[UserAuthorize]
        [AdminAuthorize]
        public ActionResult UserEdit(int id)
        {
            return  PartialView(articleRsy.FindByCommonModelId(id));
        }
        [HttpPost]
        //[UserAuthorize]
        [AdminAuthorize]
        [ValidateInput(false)]
        public ActionResult UserEdit(Article article)
        {
            //验证栏目
            CategoryRepository _categoryRsy = new CategoryRepository();
            var _category = _categoryRsy.Find(article.CommonModel.CategoryId);
            if (_category == null) ModelState.AddModelError("CommonModel.CategoryId", "栏目不存在");
            if (_category.Model != "Article") ModelState.AddModelError("CommonModel.CategoryId", "该栏目不能添加文章！");
            article.CommonModel.Inputer = UserController.UserName;
            ModelState.Remove("CommonModel.Inputer");
            article.CommonModel.Model = "Article";
            ModelState.Remove("CommonModel.Model");
            if (ModelState.IsValid)
            {
                var _article = articleRsy.Find(article.ArticleId);
                if (_article == null)//文章不存在
                {
                    Error _e = new Error { Title = "文章不存在", Details = "查询不到ArticleId为【" + article.ArticleId.ToString() + "】的文章", Cause = "文章已被删除或向服务器提交文章时数据丢失", Solution = Server.UrlEncode("<li>返回<a href='" + Url.Action("UserOwn", "Article") + "'>我的文章</a>重新操作</li><li>返回<a href='" + Url.Action("UserDefault", "Article") + "'>文章管理首页</a>。</li><li>联系网站管理员</li>") };
                    return RedirectToAction("ManageError", "Prompt", _e);
                }
                if (_article.CommonModel.Title != article.CommonModel.Title) _article.CommonModel.Title = article.CommonModel.Title;
                if (_article.CommonModel.CategoryId != article.CommonModel.CategoryId) _article.CommonModel.CategoryId = article.CommonModel.CategoryId;
                if (article.CommonModel.ReleaseDate != null) _article.CommonModel.ReleaseDate = article.CommonModel.ReleaseDate;
                if (article.CommonModel.Status != _article.CommonModel.Status) _article.CommonModel.Status = article.CommonModel.Status;
                if (article.CommonModel.PicUrl != null) _article.CommonModel.PicUrl = article.CommonModel.PicUrl;
                if (article.CommonModel.CommentStatus != _article.CommonModel.CommentStatus) _article.CommonModel.CommentStatus = article.CommonModel.CommentStatus;
                if (article.Source != null) _article.Source = article.Source;
                if (article.Author != null) _article.Author = article.Author;
                if (article.Intro != null) _article.Intro = article.Intro;
                _article.Content = article.Content;
                if (articleRsy.Update(_article))
                {

                    Notice _n = new Notice { Title = "修改文章成功", Details = "您已经成功修改了[" + article.CommonModel.Title + "]文章！", DwellTime = 5, NavigationName = "我的文章", NavigationUrl = Url.RouteUrl("Admin_default",new { controller="Home",action="Index"}) };
                    return RedirectToAction("UserNotice", "Prompt", _n);
                }
                else
                {
                    Error _e = new Error { Title = "修改文章失败", Details = "在修改文章时，未能保存到数据库", Cause = "系统错误", Solution = Server.UrlEncode("<li>返回<a href='" + Url.RouteUrl("Admin_default", new { controller="Home",action="Index", id = article.ArticleId }) + "'>修改文章</a>页面，输入正确的信息后重新操作</li><li>返回<a href='" + Url.Action("UserDefault", "Article") + "'>文章管理首页</a>。</li><li>联系网站管理员</li>") };
                    return RedirectToAction("ManageError", "Prompt", _e);
                }
            }
            return View(article);
        }
        /// <summary>
        /// 删除文章
        /// </summary>
        /// <param name="id">公共模型id</param>
        /// <returns></returns>
        //[UserAuthorize]
        [AdminAuthorize]
        public ActionResult UserDelete(int id)
        {
            bool _deleted = articleRsy.Delete(id);
            if (Request.IsAjaxRequest())
            {
                return Json(_deleted);
            }
            else
            {
                if (_deleted)
                {
                    Notice _n = new Notice { Title = "删除文章成功", Details = "您已经成功删除了该文章！", DwellTime = 5, NavigationName = "我的文章", NavigationUrl = Url.RouteUrl("Admin_default", new { controller="Home",action= "Index" }) };
                    return RedirectToAction("UserNotice", "Prompt", _n);
                }
                else
                {
                    Error _e = new Error { Title = "删除文章失败", Details = "在删除文章时发生错误", Cause = "该文章已经被删除", Solution = Server.UrlEncode("<li>返回<a href='" + Url.RouteUrl("Admin_default", new { controller="Home",action="Index"}) + "'>我的文章</a>页面，输入正确的信息后重新操作</li><li>返回<a href='" + Url.Action("UserDefault", "Article") + "'>文章管理首页</a>。</li><li>联系网站管理员</li>") };
                    return RedirectToAction("ManageError", "Prompt", _e);
                }
            }
        }
        /// <summary>
        /// 管理员查看文章
        /// </summary>
        /// <param name="id">栏目id</param>
        /// <param name="page"></param>
        /// <returns></returns>
        [AdminAuthorize]
        public ActionResult UserAll(int id = 0, int page = 1)
        {
            int _pageSize = 20;
            int _cOrder = 0;
            Category _c = null;
            cModelRsy = new CommonModelRepository();
            PagerData<CommonModel> _aData;
            if (id > 0)
            {
                var _cRsy = new CategoryRepository();
                _c = _cRsy.Find(id);
                if (_c != null)
                {
                    _pageSize = (int)_c.PageSize;
                    _cOrder = (int)_c.ContentOrder;
                }
            }
            _aData = cModelRsy.List(id, false, "Article", null, page, _pageSize, _cOrder);
            if (_c != null)
            {
                _aData.Config.RecordName = _c.RecordName;
                _aData.Config.RecordUnit = _c.RecordUnit;
            }
            return View(_aData);
        
        }
        /// <summary>
        /// 评论管理列表
        /// </summary>
        /// <returns></returns>
        public ActionResult UserComment()
        {
            return View();
        }
        /// <summary>
        /// 用户查看文章
        /// </summary>
        /// <returns></returns>
        public ActionResult Userview(int id = 0, int page = 1)
        {
            int _pageSize = 5;
            int _cOrder = 0;
            Category _c = null;
            articleview = new ArticleRepository();
            PagerData<Article> _ArticData;
           
            if (id > 0)
            {
                var _cRsy = new CategoryRepository();
                _c = _cRsy.Find(id);
                try
                {
                    if (_c != null)
                    {
                        _pageSize = (int)_c.PageSize;
                        _cOrder = (int)_c.ContentOrder;
                    }
                }
                catch
                {
                    return Content("                             Sorry This Category Have No Contect");
                }
            }
            _ArticData = articleview.List(id, false, "Article", null, page, _pageSize, _cOrder);
           
            if (_c != null)
            {
                _ArticData.Config.RecordName = _c.RecordName;
                _ArticData.Config.RecordUnit = _c.RecordUnit;
            }
            return View(_ArticData);
        }
        /// <summary>
        /// 文章内容
        /// </summary>
        /// <param name="id">CommonModelId</param>
        /// <param name="view">视图</param>
        public ActionResult PartialDetail(int id, string view = "PartialDetail")
        {
            return View(view, articleRsy.FindByCommonModelId(id));
        }

        //public PartialViewResult PartialDetail(int id, string view = "PartialDetail")
        //{
        //    return PartialView(view, articleRsy.FindByCommonModelId(id));
        //}

    }
}