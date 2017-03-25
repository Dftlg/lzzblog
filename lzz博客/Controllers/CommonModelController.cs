﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using lzz博客.Models;
using lzz博客.Repository;
using lzz博客.Areas.Admin.Extensions;


namespace lzz博客.Controllers
{
    public class CommonModelController : Controller
    {
        private CommonModelRepository cModelRsy;
        public CommonModelController()
        {
            cModelRsy = new CommonModelRepository();
        }
        /// <summary>
        /// 内容列表
        /// </summary>
        /// <param name="id">栏目Id</param>
        /// <param name="cChildren">是否包含子栏目</param>
        /// <param name="page">页码</param>
        /// <param name="pageSize">每页显示的数目【0表示依栏目设置，如栏目不存在设为20】</param>
        /// <param name="order">排序【0依栏目设置或默认】</param>
        /// <param name="view">视图</param>
        [AdminAuthorize]
        public PartialViewResult PartialList(int id, bool cChildren = false, int page = 1, int pageSize = 0, int order = 0, string view = "PartialList")
        {
            if (!cChildren && ((pageSize == 0) || (order == 0)))
            {
                CategoryRepository _categoryRsy = new CategoryRepository();
                var _category = _categoryRsy.Find(id);
                if (_category != null)
                {
                    if (pageSize == 0) pageSize = (int)_category.PageSize;
                    if (order == 0) order = _category.Order;
                }
                else if (pageSize == 0) pageSize = 20;
            }
            var _cModelPd = cModelRsy.List(id, cChildren, null, null, page, pageSize, order);
            return PartialView(view, _cModelPd);
        }
        /// <summary>
        /// 显示内容
        /// </summary>
        /// <param name="id">公共模型Id</param>
        public ActionResult Index(int id)
        {
            var _cModel = cModelRsy.Find(id);
            if (_cModel == null)
            {
                Error _e = new Error { Title = "内容不存在", Details = "未能从数据库中找到指定的内容！", Cause = "该内容已经被删除。", Solution = Server.UrlEncode("<li>返回<a href='" + Url.Action("Index", "Home") + "'>网站首页</a>。</li>") };
                return RedirectToAction("ManageError", "Prompt", _e);
            }
            return View(_cModel.Category.ContentView, _cModel);
        }
    }
}