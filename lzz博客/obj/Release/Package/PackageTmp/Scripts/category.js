function CategoryAddReady() {
    $.post($('#ParentId').attr('data-url'), null, function (data) {
        if (data == "") data = [{ id: 0, text: "无" }];
        else {
            data.unshift({ id: 0, text: "无" });
        }
        $('#ParentId').combotree({ required: true, data: data });

    });
    $('#Type').combobox({
        valueField: 'id',
        textField: 'text',
        data: [{ 'id': 0, 'text': '常规栏目' }, { 'id': 1, 'text': '单页栏目' }, { 'id': 2, 'text': '外部链接' }],
        required: true,
        onSelect: function (rec) {
            CategoryAdd_TypeChange(rec.id);
        }
    });
    $.post($('#Model').attr("data-url"), null, function (data) {
        if (data == "") data = [{ Model: "", Name: "无" }];
        else {
            data.unshift({ Model: "", Name: "无" });
        }
        $('#Model').combobox({
            valueField: 'Model',
            textField: 'Name',
            data: data,
            required: true,
            onSelect: function (rec) {
                CategoryAdd_ModelChange(rec.Model);
            }
        });
    });

    $('#ContentView').parent().parent().hide();
    $('#LinkUrl').parent().parent().hide();
    $('#ContentOrder').parent().parent().hide();
    $('#RecordUnit').parent().parent().hide();
    $('#PageSize').parent().parent().hide();
    $('#RecordName').parent().parent().hide();
    //保存事件
    $('#CategoryAdd_Save').click(function () {
        CategoryAdd_Save();
    });
    //CategoryAdd_TypeChange(0);
}
//Add视图类型切换事件
function CategoryAdd_TypeChange(typeId) {
    //常规栏目
    if (typeId == 0) {
        //模型
        $('#Model').parent().parent().show();
        var _modelValue = $('#Model').combobox('getValue');
        CategoryAdd_ModelChange(_modelValue);

    }//单页栏目
    else if (typeId == 1) {
        //模型
        $('#Model').parent().parent().hide();
        //栏目视图
        $('#CategoryView').parent().parent().show();
        //内容视图
        $('#ContentView').parent().parent().hide();
        //链接地址
        $('#LinkUrl').parent().parent().hide();
        //内容排序
        $('#ContentOrder').parent().parent().hide();
        //每页记录数
        $('#PageSize').parent().parent().hide();
        //记录单位
        $('#RecordUnit').parent().parent().hide();
        //记录名称
        $('#RecordName').parent().parent().hide();
    }//外部链接
    else if (typeId == 2) {
        //模型
        $('#Model').parent().parent().hide();
        //栏目视图
        $('#CategoryView').parent().parent().hide();
        //内容视图
        $('#ContentView').parent().parent().hide();
        //链接地址
        $('#LinkUrl').parent().parent().show();
        //内容排序
        $('#ContentOrder').parent().parent().hide();
        //每页记录数
        $('#PageSize').parent().parent().hide();
        //记录单位
        $('#RecordUnit').parent().parent().hide();
        //记录名称
        $('#RecordName').parent().parent().hide();
    }
}

//Add视图模型切换事件
function CategoryAdd_ModelChange(value) {
    if (value == "")//模型为无
    {
        //栏目视图

        $('#CategoryView').parent().parent().show();
        //内容视图
        $('#ContentView').parent().parent().hide();
        //链接地址
        $('#LinkUrl').parent().parent().hide();
        //内容排序
        $('#ContentOrder').parent().parent().hide();
        //每页记录数
        $('#PageSize').parent().parent().hide();
        //记录单位
        $('#RecordUnit').parent().parent().hide();
        //记录名称
        $('#RecordName').parent().parent().hide();
    }
    else {
        $('#CategoryView').parent().parent().show();
        //内容视图
        $('#ContentView').parent().parent().show();
        //链接地址
        $('#LinkUrl').parent().parent().hide();
        //内容排序
        $('#ContentOrder').parent().parent().show();
        //每页记录数
        $('#PageSize').parent().parent().show();
        //记录单位
        $('#RecordUnit').parent().parent().show();
        //记录名称
        $('#RecordName').parent().parent().show();
        var _modelValue = $('#Model').combobox('getValue');
        $('#ContentView').combobox('reload', $('#ContentView').attr('data-url') + '?controllerName=' + _modelValue);
    }
}
//添加保存
function CategoryAdd_Save() {
    $('#categoryadd_form').form('submit', {
        success: function (data) {
            var rt = jQuery.parseJSON(data);
            //验证
            Authentication(data.Authentication);
            //操作成功
            if (rt.Success) {
                $(document.body).append("<div id='CategoryAdd_SuccessDialog'></div>");
                $('#CategoryAdd_SuccessDialog').dialog({
                    title: '操作成功',
                    width: 280,
                    height: 138,
                    closed: false,
                    cache: false,
                    content: '<br />添加栏目成功',
                    modal: true,
                    buttons: [{
                        text: '继续添加栏目',
                        handler: function () {
                            var _layout = $('#layout');
                            var _center = _layout.layout('panel', 'center');
                            _center.panel('refresh');
                            var _west = _layout.layout('panel', 'west');
                            _west.panel('refresh');
                            $('#CategoryAdd_SuccessDialog').dialog('destroy');
                        }
                    }, {
                        text: '关闭',
                        handler: function () {
                            var _layout = $('#layout');
                            var _west = _layout.layout('panel', 'west');
                            _west.panel('refresh');
                            $('#CategoryAdd_SuccessDialog').dialog('destroy');
                        }
                    }]
                });
            }
            else {
                var msg = "";
                if (rt.ValidationList != undefined) ShowValidationMessage(rt.ValidationList);
                if (msg != "") msg = rt.Message + "<br /> <p> 原因如下：" + "<ul>" + msg + "</ul></p>";
                else msg = rt.Message;
                $.messager.alert("添加栏目失败", msg, "error");
            }
        }
    });
}