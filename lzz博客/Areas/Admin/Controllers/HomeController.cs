using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using lzz博客.Areas.Admin.Extensions;

namespace lzz博客.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        // GET: Admin/Home
       [AdminAuthorize]
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 顶部视图
        /// </summary>
        /// <returns></returns>
        public PartialViewResult PartialNorth()
        {
            return PartialView();
        }
    }
}