$(document).on("click", ".update", function () {
    var id = $(this).attr("id");
    id = id.substring(6);
    $.get("/Reply/RyUpdate/", { rid: id }, function (date) {
        $(".ry" + id).html(date);
    });
})
//$(document).on("click", ".but", function () {
//    var id = $(this).attr("id");
//    var nam = $("#nam").val();
//    id = id.substring(7);
//    var Arid = window.location.pathname;
//    var number = Arid.lastIndexOf("\/");
//    Arid = Arid.substring(number + 1, Arid.length);
//    var str = "#textarea" ;
//    var conte = $(str).val();   
//    $.post("/Reply/RyUpdate?", { ReplyContent: conte, ReplyId: id, AriticlesId: Arid, User:nam}, function () {
        
//    });
//})
$(document).on("click", ".delete", function () {
    var Arid = window.location.pathname;
    var number = Arid.lastIndexOf("\/");
    Arid = Arid.substring(number + 1, Arid.length);
    var id = $(this).attr("id");
    id = id.substring(6);
    var r = confirm("是否要删除此评论");
    if (r == true) {
       
        $.post('/Reply/RyDelete/', { rid: id, aid: Arid }, function(data){
            if(data)
            {
                alert("删除成功");
                window.location.reload();
            }
            else {
                alert("删除失败！");
            }
        })
      
        //$.ajax({
        //    type: "post",
        //    url: "/Reply/RyDelete",
        //    date: {
        //        rid: id,
        //        aid:Arid
        //    },
        //    cache:false,
        //    async: false,
        //    success: function (date) {
        //        alert("删除成功");
        //        window.location.reload();
        //    },
        //    dataType:"json"
        //})
    }
})