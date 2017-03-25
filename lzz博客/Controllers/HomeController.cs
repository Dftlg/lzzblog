using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace lzz博客.Controllers
{
    public class HomeController : Controller
    {
       
        // GET: Home
        public ActionResult Index()
        {           
            //HttpCookie _cookie = new HttpCookie("User");
            //if(_cookie.Values["Pass"]!=true.ToString())
            //_cookie.Values.Add("Pass", false.ToString());
            //Response.Cookies.Add(_cookie);
            return View();
        }
    }
}