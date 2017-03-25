//Config页提交
function ConfigSubmit() {
    $('#siteconfig_form').form('submit', {
        success: function (data) {
            var rt = jQuery.parseJSON(data);
            if (rt.Success) {
                $.messager.alert("保存成功", rt.Msg);
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