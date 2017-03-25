
KindEditor.ready(function (K) {
    window.editor = K.create('textarea[id="kindcontect"]', {
        uploadJson: '../../Attachment/Upload',
        fileManagerJson: '../../Attachment/FileManagerJson',
        allowFileManager: true
    });
    });
var setting = {
    view: { selectedMulti: false },
    callback: {
        onClick: zTreeOnClick
    }
};
$(document).ready(function () {
    $.post("../../Category/JsonUserGeneralTree", { model: "Article" }, function (data) {       
        var zTree = $.fn.zTree.init($("#categorytree"), setting, data);
        zTree.expandAll(true);
    });
    $("#CommonModel_CategoryId_Text").click(function () {
        $("#categorytree").show();
    });
});
function zTreeOnClick(event, treeId, treeNode) {
    if (treeNode.iconSkin == "canadd") {
        $("#CommonModel_CategoryId").val(treeNode.id);
        $("#CommonModel_CategoryId_Text").val(treeNode.name);
        $("#categorytree").hide();
    }
    else {
        alert("该栏目添加不能文章");
    }
};
//$("#CommonModel_ReleaseDate").datepicker({
//})
//$("input[type='submit']").button();



var event = document.createEvent('HTMLEvents');
event.initEvent("load", true, true);
window.dispatchEvent(event);
//ie  
if (document.createEventObject) {
    var event = document.createEventObject();
    window.fireEvent('onload', event);
}