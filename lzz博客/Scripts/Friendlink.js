function LintFriendLink() {
    $(document).ready(function () {
        $.ajax({
            type: 'get',
            dataType: 'json',
            url: '/FriendLink/Listlink',
            data: {},
            success: function (data) {
                $("#friendlist").empty();
                $.each(data, function (index, lists) {
                    $("#friendlist").append("<div class=row><div class='listst col-xs-3'>" + lists.Href + "</div><div class='col-xs-3 listst'>" + lists.Name + "</div><div class='col-xs-3 listst' ><div class='friendedit' id='edit"+lists.Linkid +" '>修改</div></div><div class='col-xs-3 listst' ><div class='frienddelet' id='delet" + lists.Linkid + "' >删除</div></div></div><hr/>")
                })
            }
        })
    })

}


