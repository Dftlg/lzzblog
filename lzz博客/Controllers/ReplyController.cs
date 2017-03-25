using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using lzz博客.Models;
using lzz博客.Repository;
using lzz博客.Areas.Admin.Repository;
using lzz博客.Models.UI;
using lzz博客.Extensions;
using lzz博客.Areas.Admin.Extensions;

namespace lzz博客.Controllers
{
    public class ReplyController : Controller
    {
        // GET: Reply
        ReplyRepository replyRsy;
        AdministratorRepository adminRsy;
        Reply reply;
       


        public ReplyController()
        {
            replyRsy = new ReplyRepository();
            adminRsy = new AdministratorRepository();
            reply = new Reply();

        }
        
        public ActionResult RyAdd()
        {
            
            return PartialView(/*new Reply() { CommonModel=new CommonModel()}*/);
        }
        [UserAuthorize]
        [HttpPost]

        public ActionResult RyAdd(Reply reply)
        {

            //int a = 10;
            //reply.CommonModel.CategoryId = a;
            string Arid=Request.AppRelativeCurrentExecutionFilePath;
            int nlen = Request.AppRelativeCurrentExecutionFilePath.LastIndexOf("/")+1;
            int len = Request.AppRelativeCurrentExecutionFilePath.Length-nlen;
            Arid=Arid.Substring(nlen,len);
            reply.ReplyTime = Convert.ToDateTime(Request["time"]);
            reply.User = Request["user"];
            //string sq = Request.Cookies["User"].Values["Password"];           
            reply.AriticlesId = Convert.ToInt32(Arid);
            if (ModelState.IsValid)
            {
                replyRsy.Add(reply);               
            }
            return Redirect("/Article/PartialDetail/" + Arid);
        }
        [UserAuthorize]
        //[HttpPost]
        public JsonResult RyDelete(int rid,int aid)
        {           
            HttpCookie cook = Request.Cookies["User"];
            string name = cook.Values["UserName"];
       
             if (name == replyRsy.Findid(rid).User)
            {
                replyRsy.Delete(rid);
                return Json("true");
            }
            else 
            {
                return Json("false");
            }
            
        }
        [AdminAuthorize]
        [UserAuthorize]
        [HttpPost]
        public ActionResult RyDeleteAdmin(int rid)
        {
            adminRsy = new AdministratorRepository();
            if (replyRsy.Delete(rid))
            {
                return Json("true");
            }         
            else
            {
                return Json("false");
            }
           
        }
        public ActionResult RyUpdate(int rid)
        {
            ViewData["id"] = rid;
            return PartialView();
        }
        //[HttpPost]
        //public ActionResult RyUpdate(string con,string rid,string aid ,string name)
        //{
        //    Reply reply;
        //    reply.ReplyContent = con;
        //    reply.ReplyTime = Convert.ToDateTime(Request["time"]);
        //    reply.ReplyId = Convert.ToInt32(rid);
        //    reply.AriticlesId = Convert.ToInt32(aid);
        //    reply.User = name;
        //    replyRsy.Update(reply);                        
        //    return Redirect("/Article/PartialDetail/"+aid);
        //    //+Arid
        //}
        //,string con, string rid, string aid, string name
        //[HttpPost]
        //public ActionResult RyUpdate(/*string con, string rid, string aid, string nam*/Reply reply)
        //{
        //    if (replyRsy.Update(reply))
        //    {
        //        Notice _n = new Notice { Title = "修改文章成功", Details = "您已经成功修改了[" + reply.AriticlesId + "]文章！", DwellTime = 5, NavigationName = "我的文章", NavigationUrl = Url.RouteUrl("Admin_default", new { controller = "Home", action = "Index" }) };
        //        return RedirectToAction("UserNotice", "Prompt", _n);
        //    }
        //    return Redirect("/Article/PartialDetail/" + reply.AriticlesId);
        //    //+Arid
        //}
        //传输数据调试随意跳跃
        public ActionResult Rylist(int id=0)
        {
            var list= replyRsy.List(id);
            List<Replysp> lists = new List<Replysp>(list.Count());
            foreach(var lt in list)
            {
                Replysp _r = new Replysp { id=lt.ReplyId, content = lt.ReplyContent, replytime = lt.ReplyTime.ToString(), user = lt.User };
                lists.Add(_r);
            }
            return Json(lists, JsonRequestBehavior.AllowGet);
        }
    }
}