$(document).ready(function () {
    $("selectImg").click(function () {
        var finder = new CKFinder();
        finder.selectActionFunction = function (fileUrl) {
            $("linkImg").val(fileUrl);
        };
        finder.popup();
    });
});