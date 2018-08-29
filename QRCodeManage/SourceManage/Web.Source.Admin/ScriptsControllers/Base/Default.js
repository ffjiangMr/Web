
//首页
function reloadAvatar(img_path) {
    alert(img_path);
    $('#user_avatar').attr('src', img_path);
}
var HouseSource_Open = function () {
    alert(1);
    var aList = $(".J_menuItem", window.parent.document);//
    for (var i = 0; i < aList.length; i++) {
        if ($(aList[i]).attr("href") == "/HouseSource/") {
            $(aList[i]).click();

            $(".J_menuTab.active").dblclick();
        }
    }
}
var CustomerSource_Open = function () {
    alert(1);
    var aList = $(".J_menuItem", window.parent.document);//
    for (var i = 0; i < aList.length; i++) {
        if ($(aList[i]).attr("href") == "/CustomerSource/") {
            $(aList[i]).click();
            console.info($(".J_menuTab.active"));
            $(".J_menuTab.active").dblclick();
        }
    }
}

//登录
$('#loginBtn').click(function (e) {
    $userName = $('#loginName').val();
    $passWord = $('#passWord').val();
    $rememberMe = $('#cbRememberMe').is(':checked')
    if ($userName.length == 0 || $passWord.length == 0) {
        layer.alert('账号或密码不能为空哦！', { icon: 5 });
        return;
    }

    $.ajax({
        url: "/Default/LoginAsync",
        type: "post",
        dataType: "json",
        data: {
            name: $userName,
            pass: $passWord,
            remember: $rememberMe ? "1" : "0"
        },
        success: function (data) {
            var type = data.type;
            if (type == 1) {
                window.location.href = "/Default/Index"

            } else {
                layer.msg(data.content, { icon: 5, time: 2000 });
            }
        }
    });
});