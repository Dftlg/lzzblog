using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using lzz博客.Repository;
using lzz博客.Models;

namespace lzz博客.Areas.Admin.Controllers
{
    public class SystemController : Controller
    {
        /// <summary>
        /// 基本信息设置
        /// </summary>
        /// <returns></returns>
        public PartialViewResult Config()
        {
            var _siteConfig = new SiteConfigRepository().Find();
            if (_siteConfig == null) _siteConfig = new SiteConfig() { Id = 0, Name = "NineSky", Title = "欢迎光临NineSky！" };
            return PartialView(_siteConfig);
        }

        /// <summary>
        /// Config保存
        /// </summary>
        /// <param name="siteConfig"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Config(SiteConfig siteConfig)
        {
            JsonData _jdata = new JsonData();
            if (ModelState.IsValid)
            {
                var _scRsy = new SiteConfigRepository();
                if (_scRsy.Save(siteConfig))
                {
                    _jdata.Success = true;
                    _jdata.Msg = ("保存成功√");
                }
                else
                {
                    _jdata.Success = false;
                    _jdata.Msg = ("保存数据时发生错误");
                }
            }
            else
            {
                _jdata.Success = false;
                var _eItem = ModelState.Where(m => m.Value.Errors.Count > 0);
                foreach (var i in _eItem)
                {
                    _jdata.MsgLsit.Add(i.Key, "验证失败！");
                }
                _jdata.Msg = ("保存数据时发生错误");

            }
            return Json(_jdata);
        }
    }
}