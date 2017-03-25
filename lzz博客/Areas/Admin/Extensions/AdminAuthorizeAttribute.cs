using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using lzz博客.Areas.Admin.Controllers;
using System.Web.Routing;
using lzz博客.Repository;
using lzz博客.Areas.Admin.Repository;

namespace lzz博客.Areas.Admin.Extensions
{
    /// <summary>
    ///  管理员权限验证
    /// </summary>
    public class AdminAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            //if (string.IsNullOrEmpty(AdministratorController.AdminName)) return false;
            //else return true;
            if (httpContext.Request.Cookies["User"] == null) return false;
            HttpCookie _cookie = httpContext.Request.Cookies["User"];
            string _userName = _cookie["UserName"];
            string _password = _cookie["Password"];
            if (_userName == "" || _password == "") return false;
            AdministratorRepository _admRsy = new AdministratorRepository();
            if (_admRsy.Authentication(_userName, _password) == 1) return true;
            else return false;
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult("Default", new RouteValueDictionary(new { controller = "User", action = "Login" }));
        }
    }
}