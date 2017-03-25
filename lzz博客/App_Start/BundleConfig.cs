using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;


namespace lzz博客.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            BundleTable.EnableOptimizations = false;
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/EasyUi").Include(
                        "~/Scripts/EasyUi/easyloader.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));
            bundles.Add(new ScriptBundle("~/Scripts/Easyui").Include(
                        "~/Scripts/Easyui/jquery.easyui.min.js", "~/Scripts/Easyui/locale/easyui-lang-zh_CN.js"));

            bundles.Add(new ScriptBundle("~/bundles/JqueryUi").Include(
                        "~/Scripts/jquery-ui-{version}.js", "~/Scripts/jquery.ui.datepicker-zh-CN.js"));

            bundles.Add(new ScriptBundle("~/bundles/kindeditor").Include(
                       "~/Scripts/kindeditor/kindeditor-min.js", "~/Scripts/kindeditor/lang/zh_CN.js"));
            bundles.Add(new ScriptBundle("~/Ztree").Include(
                        "~/Scripts/Ztree/jquery.ztree.core-{version}.js"));
            bundles.Add(new ScriptBundle("~/JsonMenujs").Include("~/Scripts/JsonMenu.js"
                ));
            bundles.Add(new ScriptBundle("~/LeftoutMenujs").Include("~/Scripts/LeftoutMenu.js"));
            bundles.Add(new ScriptBundle("~/bootstrapjs").Include("~/Scripts/bootstrap.min.js"));
            // 使用 Modernizr 的开发版本进行开发和了解信息。然后，当你做好
            // 生产准备时，请使用 http://modernizr.com 上的生成工具来仅选择所需的测试。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));
            bundles.Add(new ScriptBundle("~/KindUi/js").Include("~/Scripts/Kindoutinput.js"));
            bundles.Add(new ScriptBundle("~/EasyUi/js").Include("~/Scripts/EasyUi/plugins/jquery.accordion.js"));
            bundles.Add(new ScriptBundle("~/friendjs").Include("~/Scripts/Friendlink.js"));         
            bundles.Add(new ScriptBundle("~/global").Include("~/Areas/Admin/Scripts/global.js"));
            bundles.Add(new ScriptBundle("~/metisMenu/js").Include("~/Scripts/metisMenu.js"));
            bundles.Add(new ScriptBundle("~/MetisMenu.js").Include("~/Scripts/metisMenu.min.js"));
            bundles.Add(new ScriptBundle("~/datetime").Include("~/Scripts/jquery.datetimepicker.min.js"));
            bundles.Add(new ScriptBundle("~/Rylistrpjs").Include("~/Scripts/Rylistrp.js"));
            bundles.Add(new ScriptBundle("~/Areas/Admin/Scripts/Administratorjs").Include("~/Areas/Admin/Scripts/Administrator.js"));
            bundles.Add(new ScriptBundle("~/snowjs").Include("~/Scripts/snow.js"));
            bundles.Add(new ScriptBundle("~/screenjs").Include("~/Scripts/changesize.js"));
            //bundles.Add(new StyleBundle("~/Skins/css").Include("~/Skins/Default/Style.css"));
            //bundles.Add(new StyleBundle("~/Skins/usercss").Include("~/Skins/Default/User.css"));
            bundles.Add(new ScriptBundle("~/admineasyuijs").Include("~/Areas/Admin/Scripts/EasyUi/jquery.easyui.min.js"));

            bundles.Add(new StyleBundle("~/admineasyuiiconcss").Include("~/Areas/Admin/Scripts/EasyUi/demo.css"));


            bundles.Add(new StyleBundle("~/kindcss").Include("~/Scripts/kindeditor/themes/default/default.css"));
            bundles.Add(new StyleBundle("~/democss").Include("~/Scripts/EasyUi/demo.css"));
            bundles.Add(new StyleBundle("~/easyuicss").Include("~/Scripts/EasyUi/themes/default/easyui.css"));
            bundles.Add(new StyleBundle("~/bootstrapcss").Include("~/Content/bootstrap.min.css"));
            bundles.Add(new StyleBundle("~/FriList").Include("~/Content/FriendList.css"));
            bundles.Add(new StyleBundle("~/JqueryUiMetro").Include("~/Content/JqueryUi/jquery-ui-{version}.css"));
            bundles.Add(new StyleBundle("~/EasyUi/icon").Include("~/Scripts/EasyUi/themes/icon.css"));
            bundles.Add(new StyleBundle("~/Css").Include("~/Content/Default/Style.css"));
            bundles.Add(new StyleBundle("~/jqui11").Include("~/Content/jquery-ui-1.10.1.css"));
            bundles.Add(new StyleBundle("~/UserCss").Include("~/Content/Default/User/Style.css"));
            bundles.Add(new StyleBundle("~/ManageCss").Include("~/Content/Default/Manage/Style.css"));          
            bundles.Add(new StyleBundle("~/ZtreeCss").Include("~/Content/zTree/Default.css"));
            bundles.Add(new StyleBundle("~/Css/Admin").Include("~/Areas/Admin/Content/Style.css"));
            //bundles.Add(new StyleBundle("~/Css/Admin/Easyui").Include("~/Areas/Admin/Content/Easyui/icon.css", "~/Areas/Admin/Content/Easyui/metro-blue/easyui.css"));          
            bundles.Add(new StyleBundle("~/metisMenuCss").Include("~/Content/metisMenu.css"));
            bundles.Add(new StyleBundle("~/minicss").Include("~/Content/mini.css"));
            bundles.Add(new StyleBundle("~/democss").Include("~/Content/demo.css"));
            bundles.Add(new StyleBundle("~/LeftMenustylecss").Include("~/Content/LeftMenu.css"));
            bundles.Add(new StyleBundle("~/IndexStyle").Include("~/Content/indexstyle.css"));
            bundles.Add(new StyleBundle("~/messageboard").Include("~/Content/Messageboard.css"));
            bundles.Add(new StyleBundle("~/Articlecss").Include("~/Content/Articlestyle.css"));         
            bundles.Add(new StyleBundle("~/Lycs").Include("~/Content/layui/css/layui.css"));
            bundles.Add(new StyleBundle("~/datetimepicker").Include("~/Content/datetimepicker.css"));         
            bundles.Add(new StyleBundle("~/latoja").Include("~/Content/latoja.datepicker.css"));
            bundles.Add(new StyleBundle("~/buttoncss").Include("~/Content/Button.css"));
            bundles.Add(new StyleBundle("~/userchangecss").Include("~/Content/Userchange.css"));
            
            bundles.Add(new StyleBundle("~/promptcss").Include("~/Content/Promptcss.css"));
        }
    }
}