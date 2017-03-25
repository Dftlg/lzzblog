using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using lzz博客.Areas.Admin.Repository;
using lzz博客.Areas.Admin.Extensions;
using lzz博客.Models;
using lzz博客.Areas.Admin.Models;
using lzz博客.Extensions;
using lzz博客.Common;


namespace lzz博客.Areas.Admin.Controllers
{
    public class AdministratorController : Controller
    {
        private IAdministrator adminRsy;
        public AdministratorController()
        {
            adminRsy = new AdministratorRepository();
        }
        /// <summary>
        /// 添加管理员
        /// </summary>
        /// <returns></returns>
        [AdminAuthorize]
        public ActionResult Add()
        {
            return PartialView();
        }
        [AdminAuthorize]
        [HttpPost]
        public JsonResult Add(Administrator admin)
        {
            JsonData _jdata = new JsonData();
            if (ModelState.IsValid)
            {
                if (adminRsy.Find(admin.AdminName) != null)
                {
                    _jdata.Success = false;
                    _jdata.Msg = "管理员名称已存在！";
                }
                else
                {
                    admin.IsPreset = false;
                    if (adminRsy.Add(admin))
                    {
                        _jdata.Success = true;
                        _jdata.Msg = "保存成功√！";
                    }
                    else
                    {
                        _jdata.Success = false;
                        _jdata.Msg = "数据未能保存到数据库！";
                    }
                }
            }
            else
            {
                var _eItem = ModelState.Where(m => m.Value.Errors.Count > 0);
                foreach (var i in _eItem)
                {
                    _jdata.MsgLsit.Add(i.Key, "验证失败！");
                }
            }
            return Json(_jdata);
        }

        /// <summary>
        /// 管理员列表
        /// </summary>
        /// <returns></returns>
        [AdminAuthorize]
        public ActionResult Index()
        {
            return PartialView(); ;
        }
        /// <summary>
        /// 修改资料
        /// </summary>
        /// <returns></returns>
        //[AdminAuthorize]
        public PartialViewResult ChangePassWord()
        {
            return PartialView();
        }
        //[AdminAuthorize]
        [HttpPost]
        public JsonResult ChangePassWord(string oldPwd, string newPwd)
        {
            JsonData _jdata = new JsonData();
            if (ModelState.IsValid)
            {
                var _admin = AdministratorController.AdminInfo;
                if (_admin == null)
                {
                    _jdata.Success = false;
                    _jdata.Msg = "登录已超时，请重新登录！";
                }
                else if (RandomText.Sha256(oldPwd) != _admin.PassWord)
                {
                    _jdata.Success = false;
                    _jdata.Msg = "原密码错误！";
                }
                else
                {
                    _admin.PassWord = RandomText.Sha256(newPwd);
                    if (adminRsy.Modify(_admin))
                    {
                        _jdata.Success = true;
                        _jdata.Msg = "保存成功√！";
                    }
                    else
                    {
                        _jdata.Success = false;
                        _jdata.Msg = "数据未能保存到数据库！";
                    }
                }
            }
            else
            {
                var _eItem = ModelState.Where(m => m.Value.Errors.Count > 0);
                foreach (var i in _eItem)
                {
                    _jdata.MsgLsit.Add(i.Key, "验证失败！");
                }
            }
            return Json(_jdata);
        }
        /// <summary>
        /// 删除管理员
        /// </summary>
        /// <param name="Id">管理员Id</param>
        /// <returns></returns>
        [AdminAuthorize]
        [HttpPost]
        public JsonResult Delete(int Id)
        {
            JsonData _jdata = new JsonData();
            var _admin = adminRsy.Find(Id);
            if (_admin == null)
            {
                _jdata.Success = false;
                _jdata.Msg = "Id为: " + Id + " 的管理员不存在!";
            }
            else if (_admin.IsPreset)
            {
                _jdata.Success = false;
                _jdata.Msg = "不能删除系统预置管理员账号！";
            }
            else
            {
                if (adminRsy.Delete(Id))
                {
                    _jdata.Success = true;
                    _jdata.Msg = "删除成功√";
                }
                else
                {
                    _jdata.Success = false;
                    _jdata.Msg = "删除失败！";
                }
            }
            return Json(_jdata);

        }
        /// <summary>
        /// 管理员列表
        /// </summary>
        /// <returns></returns>
        [AdminAuthorize]
        public ActionResult List()
        {
            return Json(adminRsy.Find());
        }
        /// <summary>
        /// 登录
        /// </summary>
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Administrator admin)
        {
            
            //JsonData _jdata = new JsonData();
            //adminName = Server.HtmlEncode(admin.AdminName);
            //passWord = RandomText.Sha256(Server.HtmlEncode(admin.PassWord));
            //int _code = adminRsy.Authentication(adminName, passWord);
            //if (_code == 1)
            //{
            //    AdministratorController.AdminName = adminName;
            //    _jdata.Success = true;
            //    _jdata.Msg = "登录成功！";
            //}
            //else if (_code == 0)
            //{
            //    _jdata.Success = false;
            //    _jdata.Msg = "密码错误！";
            //}
            //else if (_code == -1)
            //{
            //    _jdata.Success = false;
            //    _jdata.Msg = "管理员账号不存在！";
            //}
            //else
            //{
            //    _jdata.Success = false;
            //    _jdata.Msg = "发生未知错误，请刷新后重新登录！";
            //}
            return RedirectToRoute("Admin_default", new { controller = "Home", action = "Index" });
        }

        /// <summary>
        /// 退出登录
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            Session.Remove("AdminName");
            return RedirectToAction("Login", "Administrator");
        }
        #region 静态属性
        /// <summary>
        /// 管理员登录名
        /// </summary>
        public static string AdminName
        {
            get
            {
                string _adminName = string.Empty;
                if (System.Web.HttpContext.Current.Session["AdminName"] != null) _adminName = System.Web.HttpContext.Current.Session["AdminName"].ToString();
                return _adminName;
            }
            set
            {
                if (string.IsNullOrEmpty(value)) System.Web.HttpContext.Current.Session.Remove("AdminName");
                else
                {
                    System.Web.HttpContext.Current.Session.Timeout = 60;
                    System.Web.HttpContext.Current.Session.Add("AdminName", value);
                }
            }
        }
        /// <summary>
        /// 管理员信息
        /// </summary>
        public static Administrator AdminInfo
        {
            get
            {
                AdministratorRepository _adminRsy = new AdministratorRepository();
                return _adminRsy.Find(AdministratorController.AdminName);
            }
        }
        #endregion
    
   }
}