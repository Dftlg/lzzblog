//登录
function AdminLogin(url) {
    //alert(url);
    $('form').form('submit', {
        success: function (data) {
           // alert(data);
            var rt = jQuery.parseJSON(data);
            if (rt.Success) location.href = url;
            else {
                var msg = "";
                if (rt.MsgLsit != undefined) {
                    $.each(rt.MsgLsit, function (i, val) {
                        msg += "<li>" + i + ":" + val + "</li>";
                    });
                }
                if (msg != "") msg = rt.Msg + "<br /> <p> 原因如下：" + "<ul>" + msg + "</ul></p>";
                else msg = rt.Msg;
                $.messager.alert("登录失败", msg, "error");
            }
        }
    });
    return false;
}

//显示添加对话框
function AdminAddDlgShow() {
    $("#adminadddlg").dialog("open");
    $("#adminadddlg").dialog("refresh");
}
//添加管理员保存
function AdminAddSave() {

    $('#adminadd_form').form('submit', {
        success: function (data) {
            var rt = jQuery.parseJSON(data);
            if (rt.Success) {
                $.messager.alert("保存成功", rt.Msg, "", function () {
                    $("#adminadddlg").dialog("close");
                    $('#admin_datagrid').datagrid("reload");
                });
            }
            else {
                var msg = "";
                if (rt.MsgLsit != undefined) {
                    $.each(rt.MsgLsit, function (i, val) {
                        msg += "<li>" + i + ":" + val + "</li>";
                    });
                }
                if (msg != "") msg = rt.Msg + "<br /> <p> 原因如下：" + "<ul>" + msg + "</ul></p>";
                else msg = rt.Msg;
                $.messager.alert("保存失败", msg, "error");
            }
        }
    });
}
//删除管理员
function AdminDelRow(url) {
    var row = $('#admin_datagrid').datagrid('getSelected');
    if (row) {
        $.messager.confirm('确认', '你确定要删除此管理员', function (r) {
            if (r) {
                $.post(url, { Id: row.AdministratorId }, function (data) {
                    if (data.Success) {
                        $.messager.alert("删除成功", data.Msg, "", function () { $("#admin_datagrid").datagrid("reload"); });
                    }
                    else $.messager.alert("错误", data.Msg, "error");
                });
            }
        });
    }
}
//显示修改密码窗口
function ShowChangePwdDlg(url) {
    alert("qqq"+url);
    $(document.body).append("<div id='cPwdDlg'></div>");
    alert("qqq" + url);
    $('#cPwdDlg').dialog({
        title: "修改密码",
        width: 480,
        height: 260,
        closed: false,
        cache: false,
        href: url,
        modal: true,
        onClose: function () { $(this).dialog("destroy"); }
    });
}
//修改密码保存
function AdminCPwdSave() {
    $('#admincha_form').form('submit', {
        success: function (data) {

            var rt = jQuery.parseJSON(data);
            if (rt.Success) {
                $.messager.alert("保存成功", rt.Msg, "", function () {
                    location.href = $("#btn_Logout").attr("href");
                    $("#cPwdDlg").dialog("destroy");

                });
            }
            else {
                var msg = "";
                if (rt.MsgLsit != undefined) {
                    $.each(rt.MsgLsit, function (i, val) {
                        msg += "<li>" + i + ":" + val + "</li>";
                    });
                }
                if (msg != "") msg = rt.Msg + "<br /> <p> 原因如下：" + "<ul>" + msg + "</ul></p>";
                else msg = rt.Msg;
                $.messager.alert("保存失败", msg, "error");
            }
        }
    });
}