using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Drawing;
using lzz博客.Common;
using lzz博客.Models;
using lzz博客.Repository;
using System.Data;
using lzz博客.Extensions;
using lzz博客.Areas.Admin.Repository;


namespace lzz博客.Controllers
{
    public class UserController : Controller
    {
        private UserRepository userRsy;
        private IAdministrator adminRsy;
        
        // GET: User
        //[UserAuthorize]
        public ActionResult UserCenter()
        {
            userRsy = new UserRepository();
            var _user = userRsy.Find(UserName);
            return View(_user);
        }
        /// <summary>
        /// Register_Action
        /// </summary>
        /// <param name="userReg"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(UserRegister userReg)
        {

            if (Session["VerificationCode"] == null || Session["VerificationCode"].ToString() == "")
            {
                Error _e = new Error { Title = "验证码不存在", Details = "在用户注册时，服务器端的验证码为空，或向服务器提交的验证码为空",
                    Cause = "<li>你注册时在注册页面停留的时间过久页已经超时</li><li>您绕开客户端验证向服务器提交数据</li>",
                    Solution = "返回<a href='" + Url.Action("Register", "User") + "'>注册</a>页面，刷新后重新注册" };
                return RedirectToAction("Error", "Prompt", _e);
            }
            else if (Session["VerificationCode"].ToString() != userReg.VerificationCode.ToUpper())
            {
                ModelState.AddModelError("VerificationCode", "×");
                return View();
            }
            userRsy = new UserRepository();
            if (userRsy.Exists(userReg.UserName))
            {
                ModelState.AddModelError("UserName", "用户名已存在");
                return View();
            }
            User _user = userReg.GetUser();
            _user.RegTime = System.DateTime.Now;
            if (userRsy.Add(_user))
            {
                Notice _n = new Notice { Title = "注册成功", Details = "您已经成功注册，用户为：" + _user.UserName + " ，请牢记您的密码！", DwellTime = 5, NavigationName = "登录页面", NavigationUrl = Url.Action("Login", "User") };
                return RedirectToAction("Notice", "Prompt", _n);
            }
            else
            {
                Error _e = new Error { Title = "注册失败", Details = "在用户注册时，发生了未知错误", Cause = "系统错误", Solution = "<li>返回<a href='" + Url.Action("Register", "User") + "'>注册</a>页面，输入正确的信息后重新注册</li><li>联系网站管理员</li>" };
                return RedirectToAction("Error", "Prompt", _e);
            }
        }
        /// <summary>
        /// Register默认Action
        /// </summary>
        /// <returns></returns>
       
        public ActionResult Register()
        {
            return View();
        }
        /// <summary>
        /// Login默认Action
        /// </summary>
        /// <returns></returns>
        
        public ActionResult Login()
        {
            return View();
        }
        /// <summary>
        /// Login_Action
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserLogin login)
        {
            if(Session["VerificationCode"] ==null||Session["VerificationCode"].ToString()=="")
            {
                Error _e = new Error
                {
                    Title = "验证码不存在",
                    Details = "在用户注册时，服务器端的验证码为空，或向服务器提交的验证码为空",
                    Cause = "<li>你注册时在注册页面停留的时间过久页已经超时</li><li>您绕开客户端验证向服务器提交数据</li>",
                    Solution = "返回<a href='" + Url.Action("Register", "User") + "'>注册</a>页面，刷新后重新注册"
                };
                return RedirectToAction("Error", "Prompt", _e);
            }
            else if(Session["VerificationCode"].ToString()!=login.VerificationCode.ToUpper())
            {
                ModelState.AddModelError("VerificationCode", "×");
                return View();
            }
            userRsy = new UserRepository();
            if (userRsy.Authentication(login.UserName, RandomText.Sha256(login.Password)) == 0)
            {
                
                HttpCookie _cookie = new HttpCookie("User");
                adminRsy = new AdministratorRepository();        
                _cookie.Values.Add("UserName", login.UserName);
                _cookie.Values.Add("Password", RandomText.Sha256(login.Password));
                _cookie.Values.Add("Pass",false.ToString());
                //_cookie.Values.Add("Pass", false.ToString());
                Response.Cookies.Add(_cookie);
                if (adminRsy.Authentication(login.UserName,RandomText.Sha256(login.Password))==1)
                {            
                    return RedirectToRoute("Admin_default", new { controller = "Home", action = "Index" });
                }    
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("Message", "登陆失败！");
                return View();
            }
        }
        /// <summary>
        /// Logout_Action
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
             if(Request.Cookies["User"]!=null)
            {
                HttpCookie _cookie = Request.Cookies["User"];
                _cookie.Expires = DateTime.Now.AddHours(-1);
                Response.Cookies.Add(_cookie);
            }
            Notice _n = new Notice { Title = "成功退出", Details = "您已经成功退出！", DwellTime = 5, NavigationName = "网站首页", NavigationUrl = Url.Action("Index", "Home") };
            return RedirectToAction("Notice", "Prompt", _n);
        }
        /// <summary>
        /// changPassword默认action
        /// </summary>
        /// <returns></returns>
        [UserAuthorize]
        public ActionResult ChangePassword()
        {
            return View();
        }
        /// <summary>
        /// changePassword_action
        /// </summary>
        /// <param name="userChangePassword"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [UserAuthorize]
        public ActionResult ChangePassword(UserChangePassword userChangePassword)
        {
            userRsy = new UserRepository();
            if (userRsy.Authentication(UserName, RandomText.Sha256(userChangePassword.Password)) == 0)
            {
                var _user = userRsy.Find(UserName);
                if (_user == null)
                {
                    Error _e = new Error { Title = "修改密码失败", Details = "修改密码时，系统查询不到用户信息", Cause = Server.UrlEncode("<li>用户在修改密码界面停留的时间过长，登录信息已失效。</li><li>系统错误。</li>"), Solution = Server.UrlEncode("<li>返回<a href='" + Url.Action("ChangePassword", "User") + "'>修改密码</a>页面，输入正确的信息后重新注册</li><li>联系网站管理员</li>") };
                    return RedirectToAction("Error", "Prompt", _e);
                }
                _user.Password = RandomText.Sha256(userChangePassword.NewPassword);
                if (userRsy.Update(_user))
                {
                    Notice _n = new Notice { Title = "成功修改密码", Details = "您已经成功修改密码，请牢记您的新密码！", DwellTime = 5, NavigationName = "登陆页面", NavigationUrl = Url.Action("Login", "User") };
                    if (Request.Cookies["User"] != null)
                    {
                        HttpCookie _cookie = Request.Cookies["User"];
                        _cookie.Expires = DateTime.Now.AddHours(-1);
                        Response.Cookies.Add(_cookie);
                    }
                    return RedirectToAction("Notice", "Prompt", _n);
                }
                else
                {
                    Error _e = new Error { Title = "修改密码失败", Details = "修改密码时，更新数据库失败！", Cause = Server.UrlEncode("<li>系统错误。</li>"), Solution = Server.UrlEncode("<li>返回<a href='" + Url.Action("ChangePassword", "User") + "'>修改密码</a>页面，输入正确的信息后重新注册</li><li>联系网站管理员</li>") };
                    return RedirectToAction("Error", "Prompt", _e);
                }
            }
            else
            {
                ModelState.AddModelError("Password", "原密码不正确，请重新输入");
                return View();
            }
        }
        /// <summary>
        /// ChangeInfo默认action
        /// </summary>
        /// <returns></returns>
        [UserAuthorize]
        public ActionResult ChangeInfo()
        {
            userRsy = new UserRepository();
            var _user = userRsy.Find(UserName);
            return View(_user);
        }
        /// <summary>
        /// 修改密码_action
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [UserAuthorize]
        public ActionResult ChangeInfo(User user)
        {
            userRsy = new UserRepository();
            if (userRsy.Authentication(UserName, RandomText.Sha256(user.Password)) == 0)
            {
                var _user = userRsy.Find(UserName);
                _user.Gender = user.Gender;
                _user.Email = user.Email;
                _user.QQ = user.QQ;
                _user.Tel = user.Tel;
                _user.Address = user.Address;
                _user.PostCode = user.PostCode;
                if (userRsy.Update(_user))
                {
                    Notice _n = new Notice { Title = "修改资料成功", Details = "您已经成功修改资料！", DwellTime = 5, NavigationName = "用户首页", NavigationUrl = Url.Action("UserCenter", "User") };
                    return RedirectToAction("UserNotice", "Prompt", _n);
                }
                else
                {
                    Error _e = new Error { Title = "修改资料失败", Details = "在修改用户资料时时，更新的资料未能保存到数据库", Cause = "系统错误", Solution = Server.UrlEncode("<li>返回<a href='" + Url.Action("ChangeInfo", "User") + "'>修改资料</a>页面，输入正确的信息后重新操作</li><li>联系网站管理员</li>") };
                    return RedirectToAction("UserError", "Prompt", _e);
                }
            }
            else
            {
                ModelState.AddModelError("Password", "密码错误！");
                return View();
            }
        }

        /// <summary>
        /// 验证码Action
        /// </summary>
        /// <returns></returns>
        public ActionResult VerificationCode()
        {
            int _verificationLength = 6;
            int _width = 100, _height = 20;
            SizeF _verificationTextSize;
            //Bitmap _bitmap = new Bitmap(Server.MapPath("~/Resources/pic1.jpg"), true);
            //TextureBrush _brush = new TextureBrush(_bitmap);
            SolidBrush _brush = new SolidBrush(Color.Black);
            //获取验证码
            string _verificationText = RandomText.VerificationText(_verificationLength);
            //存储验证码
            Session["VerificationCode"] = _verificationText.ToUpper();
            Font _font = new Font("Arial", 14, FontStyle.Bold);
            Bitmap _image = new Bitmap(_width, _height);
            Graphics _g = Graphics.FromImage(_image);
            //清空背景色
            _g.Clear(Color.White);
            //绘制验证码
            _verificationTextSize = _g.MeasureString(_verificationText, _font);
            _g.DrawString(_verificationText, _font, _brush, (_width - _verificationTextSize.Width) / 2, (_height - _verificationTextSize.Height) / 2);
            _image.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            return null;
        }
        /// <summary>
        /// 获取cookie
        /// </summary>
        public static string UserName
        {
            get
            {
                HttpCookie _cookie = System.Web.HttpContext.Current.Request.Cookies["User"];
                if (_cookie == null) return "";
                else return _cookie["UserName"];
            }
        }
        public ActionResult Jsontest()
        {
            return View();
        }
    }
}