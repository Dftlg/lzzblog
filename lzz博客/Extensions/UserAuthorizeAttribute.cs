using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using lzz博客.Repository;
using System.Web.Mvc;
namespace lzz博客.Extensions
{
    public class UserAuthorizeAttribute:AuthorizeAttribute 
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext.Request.Cookies["User"] == null) return false;
            //验证用户名密码是否正确
            HttpCookie _cookie = httpContext.Request.Cookies["User"];
            string _userName = _cookie["UserName"];
            string _password = _cookie["Password"];
            httpContext.Response.Write("用户名:" + _userName);
            if (_userName == "" || _password == "") return false;
            UserRepository _userRsy = new UserRepository();
            if (_userRsy.Authentication(_userName, _password) == 0) return true;
            else return false;
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            base.HandleUnauthorizedRequest(filterContext);
            //if(filterContext.HttpContext.Response.StatusCode==401)
            //{
                filterContext.Result = new RedirectResult("/User/Login");
            //}
        }
    }
}