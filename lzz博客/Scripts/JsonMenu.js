$(document).ready(function () {
    $(".leftmenu").hide();
    $(".leftmenu").append("<div class=clearfix><aside class=sidebar ><nav class=sidebar-nav> <div id=incontainer ><ul class=metismenu id=menu>")
    $.ajax({
        type: 'get',//请求方式
        dataType: 'json',//设置返回数据的格式
        url: '/Category/JsonMetisMenu',//请求地址
        data: {},//请求参数
        success: function (data) {
            var bool = true;
            $.each(data, function (index, _trees) {
                //alert(_trees.state);
                if (bool == true) {
                    $(".metismenu").append("<li class=active ><a href=# aria-expanded=first class='Menu fill Clk' id=" + _trees.id + " ><span class=sidebar-nav-item-icon fa fa-github fa-lg></span><span class=sidebar-nav-item>" + _trees.text + "</span><span class=fa arrow></span> </a><ul aria-expanded=true id=Node" + _trees.id + "></ul>");
                    //$(".metismenu").append("<li class=active ><a href=# aria-expanded=true class='Menu fill' id=Node" + _trees.id + " ><span class=sidebar-nav-item-icon fa fa-github fa-lg></span><span class=sidebar-nav-item>" + _trees.text + "</span><span class=fa arrow></span> </a>");
                    bool = false;
                }
                else {
                    if (_trees.state == 'true')
                        $(".metismenu").append("<li><a href=# aria-expanded=first class=Menu id=" + _trees.id + ">" + _trees.text + "<span class=fa arrow></span></a><ul aria-expanded=false class=fill id=Node" + _trees.id + ">    </ul></li>")
                    else
                        $(".metismenu").append("<li><a href=# class='Menu Clk'id=" + _trees.id + ">" + _trees.text + "</a></li>")
                }
            });
        },
        error: function (msg) {
            alert("失败");
        }
    });
    $(".leftmenu").append("</ul></div></nav></aside></div>");
})
//点击时继续扩展内容
$(document).on("click", ".Menu", function () {
    var click = $(this).attr("aria-expanded");//获取当前点击对象aria-expanded判断是否展开or回收
    var newTh = $(this);//获取点击的对象
    var node = $(this).attr("id");
    //alert(click);
    if (click == 'first') {
        $.ajax({
            type: 'get',
            dataType: 'json',
            url: '/Category/JsonMetisMenu/' + node,
            data: {},
            success: function (data) {
                //$("#" + node + "").append("<ul aria-expanded=true>")
                $.each(data, function (index, _trees) {
                    //alert(_trees.state);
                    if (_trees.state == 'true') {
                        //$(".fill #Node" + _trees.id + "").append("<li><a href=# aria-expanded=false class=Menu id=" + _trees.id + ">" + _trees.text + "<span class=fa arrow ></span></a><ul aria-expanded=false class=fill id=Node" + _trees.id + ">    </ul></li>")
                        $("#Node" + node + "").append("<li><a href=# aria-expanded=first class='fill Menu' id=" + _trees.id + ">" + _trees.text + "<span class=fa plus-times></span></a><ul aria-expanded=false class=fill id=Node" + _trees.id + ">    </ul></li>")
                    }
                    else {
                        $("#Node" + node + "").append("<li><a href=# class='Menu Clk' id=" + _trees.id + ">" + _trees.text + "</a></li>")
                    }
                })
                //$("#"+node+"").append("</ul>")
                newTh.attr("aria-expanded", "false");//更改点击对象属性为false
            },
            error: function (msg) {
                alert("错误");
            }

        })
        $(this).append()
    }
    else if (click == 'false') {
        $("#Node" + node).slideUp();
        newTh.attr("aria-expanded", "true");
    }
    else if (click == 'true') {
        $("#Node" + node).slideDown();
        newTh.attr("aria-expanded", "false");
    }


})

//主页左侧导航栏异步加载文章概况
$(document).on("click", ".Clk", function () {
    var id = $(this).attr("id");
    $.get("/Article/Userview/" + id + "", null, function (data) {  
        $("#content1").html(data);
    })
})


//主页加载文章
//$(document).on("click", ".Clt", function () {
//    var id = $(this).attr("id");
//    $.get("/Article/PartialDetail/" + id + "", null, function (data) {
//        $("#content").html(data);
//    })
//})

//$(function () {
//    $('#menu').metisMenu();
//});