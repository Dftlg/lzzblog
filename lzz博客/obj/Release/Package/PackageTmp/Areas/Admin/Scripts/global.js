$(document).ready(function () {
    $("#system_cfg").click(function () {
        $("#west").load("/FriendLink/LinkMenu", function () {
            $("#westmenu").accordion({
                animate: false,
                border: false
            });
        });
    });
});
$(document).ready(function () {
    $("#cagegory_cfg").click(function () {
        //alert("aaa");
        $("#west").load("/Category/Menu", function () {          
                $("#westmenu").accordion({
                    animate: false,
                    border: false 
            });
        });
    });
});
$(document).ready(function () {
    $("#file_cfg").click(function () {
        $("#west").load("/Article/ArticleMenu",function(){
            $("#westmenu").accordion({
                animate:false,
                border:false
            });
        });
    });
})
$(document).ready(function () {
    $("#westmenulink").click(function () {
        $("#west").load("/Category/Menu", function () {
            $("#westmenu").accordion({
                animate: false,
                border: false
            });
        });
    });
});
//左侧菜单点击
function WestMenu() {
    $(".WestMenuLink").click(function () {
        var _link = $(this);
        //alert(_link.text());
        if ($("#tabs").tabs('exists', _link.text())) {
            $("#tabs").tabs('select', _link.text());
        }
        else {
            $.get(_link.attr("href"), null, function (data) {
               // alert(_link.text());
               //alert(data);
                $("#tabs").tabs('add', {
                    title: _link.text(),
                    closable: true,
                    content:data
                });
            });
        }
        return false;
    });
}

//关闭当前tab标签
function TabCurClose() {
    var tab = $('#tabs').tabs('getSelected');
    var index = $('#tabs').tabs('getTabIndex', tab);
    $('#tabs').tabs('close', index);
}
///设置当前标签内容
function TabCurContetTextSet(text) {
    var tab = $('#tabs').tabs('getSelected');
    $('#tabs').tabs('update', {
        tab: tab,
        options: {
            content: text
        }
    });
    tab.panel('refresh');
}
//
function TabCurContetUrlSet(url) {
    $.get(url,null, function (data) {
        TabCurContetTextSet(data);
    });
}