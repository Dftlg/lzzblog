using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using lzz博客.Models;
using lzz博客.Repository;
using lzz博客.Areas.Admin.Extensions;

namespace lzz博客.Controllers
{
    public class FriendLinkController : Controller
    {
        FriendlinkRepository FrLiRey;
        // GET: FriendLink
        public FriendLinkController()
        {
            FrLiRey = new FriendlinkRepository();
        }
        public ActionResult Index()
        {
            return View();
        }
        [AdminAuthorize]
        public ActionResult AddFriendlink()
        {
            return View();
        }
        [HttpPost]
        [AdminAuthorize]
        public ActionResult AddFriendlink(FriendLink friendlink)
        {
            if (ModelState.IsValid)
            {
                if(FrLiRey.Add(friendlink))
                {
                    Notice _n = new Notice { Title = "添加友链成功", Details = "您已经成功添加[" + friendlink.Name + "]友链！", DwellTime = 5, NavigationName = "友链列表", NavigationUrl = Url.RouteUrl("Admin_default", new { controller = "Home", action = "Index" }) };
                    return RedirectToAction("ManageNotice", "Prompt", _n);
                }
                else
                {
                    Error _e = new Error { Title = "添加友链失败", Details = "在添加友链时，未能保存到数据库", Cause = "系统错误", Solution = Server.UrlEncode("<li>返回<a href='" + Url.Action("ManageAdd", "Category") + "'>添加友链</a>页面，输入正确的信息后重新操作</li><li>联系网站管理员</li>") };
                    return RedirectToAction("ManageError", "Prompt", _e);
                }
            }
            else
            {
                return View(friendlink);
            }           
        }
        [AdminAuthorize]
        public ActionResult EditFriendlink(int id)
        {

            return PartialView(FrLiRey.Find(id));
        }
        [HttpPost]
        //[UserAuthorize]
        [ValidateInput(false)]
        [AdminAuthorize]
        public ActionResult EditFriendlink(FriendLink friendlink)
        {
            if (ModelState.IsValid)
            {
                var _friendlink = FrLiRey.Find(friendlink.Linkid);
                if (_friendlink == null)//友链不存在
                {
                    Error _e = new Error { Title = "友链不存在", Details = "查询不到ArticleId为【" + friendlink.Linkid + "】的友链", Cause = "友链已被删除或向服务器提交友链时数据丢失", Solution = Server.UrlEncode("<li>返回<a href='" + Url.Action("UserOwn", "Article") + "'>我的文章</a>重新操作</li><li>返回<a href='" + Url.Action("UserDefault", "Article") + "'>文章管理首页</a>。</li><li>联系网站管理员</li>") };
                    return RedirectToAction("ManageError", "Prompt", _e);
                }
                if (_friendlink.Name != friendlink.Name)
                    _friendlink.Name = friendlink.Name;
                if (_friendlink.Href != friendlink.Href)
                    _friendlink.Href = friendlink.Href;
                if (FrLiRey.Update(_friendlink))
                {

                    Notice _n = new Notice { Title = "修改友链成功", Details = "您已经成功修改了[" + friendlink.Name + "]友链！", DwellTime = 5, NavigationName = "我的友链", NavigationUrl = Url.RouteUrl("Admin_default", new { controller = "Home", action = "Index" }) };
                    return RedirectToAction("UserNotice", "Prompt", _n);
                }
                else
                {
                    Error _e = new Error { Title = "修改友链失败", Details = "在修改友链时，未能保存到数据库", Cause = "系统错误", Solution = Server.UrlEncode("<li>返回<a href='" + Url.RouteUrl("Admin_default", new { controller = "Home", action = "Index" }) + "'></a>输入正确的信息后重新操作</li><li>返回<a href='" + Url.Action("UserDefault", "Article") + "'>文章管理首页</a>。</li><li>联系网站管理员</li>") };
                    return RedirectToAction("ManageError", "Prompt", _e);
                }
            }
            return View(friendlink);
        }
        [AdminAuthorize]
        public ActionResult ListDelete(int Frlkid)
        {
            
                var _friendlink = FrLiRey.Find(Frlkid);
                if (_friendlink == null)//友链不存在
                {
                    Error _e = new Error { Title = "友链不存在", Details = "查询不到ArticleId为【" + Frlkid + "】的友链", Cause = "友链已被删除或向服务器提交友链时数据丢失", Solution = Server.UrlEncode("<li>返回<a href='" + Url.Action("UserOwn", "Article") + "'>我的文章</a>重新操作</li><li>返回<a href='" + Url.Action("UserDefault", "Article") + "'>文章管理首页</a>。</li><li>联系网站管理员</li>") };
                    return RedirectToAction("ManageError", "Prompt", _e);
                }              
                if (FrLiRey.Delete(Frlkid))
                {

                if (Request.IsAjaxRequest())
                {
                    return Json("true");
                }
            }                            
            return null;

        }

        public PartialViewResult LinkMenu()
        {
            return PartialView();
        }
        public ActionResult ListFriendlink()
        {
            //var lists = FrLiRey.List();
            //return Json(lists, JsonRequestBehavior.AllowGet);
            return View();
        }
        public JsonResult Listlink()
        {
            var lists = FrLiRey.List();
            return Json(lists, JsonRequestBehavior.AllowGet);
        }
    }
}