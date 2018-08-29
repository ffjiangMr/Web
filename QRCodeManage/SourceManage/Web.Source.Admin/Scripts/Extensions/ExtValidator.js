function importJs() {
    for (var i = 0; i < arguments.length; i++) {
        var file = arguments[i];
        console.log(new RegExp(/\.js$/,"i").test(file));
        if (new RegExp(/\.js$/,'i').test(file)) {
            document.write('<script type=\\"text/javascript\\" src=\\"' + file + '\\"></sc' + 'ript>');
            console.log('<script type=\\"text/javascript\\" src=\\"' + file + '\\"></sc' + 'ript>');
        }
        else {
            //document.write('<style type=\\"text/css\\">@import url(' + file + ') ;</style>');
            console.log('<style type=\\"text/css\\">@import url(' + file + ') ;</style>');

            document.write('<link type=\\"text/css\\" href=\\"' + file + '\\" rel=\\"stylesheet\\" />');
            console.log('<link type=\\"text/css\\" href=\\"' + file + '\\" rel=\\"stylesheet\\" />');
            //<link href="css/main.css" rel="stylesheet" type="text/css">
        }
    }
}


//以下为修改jQuery Validation插件兼容Bootstrap的方法，没有直接写在插件中是为了便于插件升级
$.validator.setDefaults({
    highlight: function (element) {
        $(element).closest('.form-group').removeClass('has-success').addClass('has-error');
    },
    success: function (element) {
        element.closest('.form-group').removeClass('has-error').addClass('has-success');
    },
    errorElement: "span",
    errorPlacement: function (error, element) {
        if (element.is(":radio") || element.is(":checkbox")) {
            error.appendTo(element.parent().parent().parent());
        } else {
            error.appendTo(element.parent());
        }
    },
    errorClass: "help-block m-b-none",
    validClass: "help-block m-b-none"
});

//自定义验证规则
//addMethod 的第一个参数,就是添加的验证方法的名子,这时是 
//addMethod 的第二个参数,是一个函数,这个比较重要,决定了用这个验证方法时的写法
//addMethod 的第三个参数,就是自定义的错误提示
$.validator.addMethod("coded", function (value, element, params) {
    if (isNaN(parseInt(value))) {
        return false;
    }
    if ($.isArray(params)) {
        if (value.length >= params[0] && value.length <= params[1]) {
            return true;
        } else {
            return false;
        }
    } else {
        return value.length == params;
    }
}, "编码格式错误");
$.validator.addMethod("length", function (value, element, params) {
    //alert($.isArray([10, 25, 3]) + "|" + Array.valueOf(params));
    //alert(value + "|" + $.isArray(eval(params)) + "|" + params + "|" + typeof (params));
    params = eval(params);
    if ($.isArray(params)) {
        if (value.length >= params[0] && value.length <= params[1]) {
            return true;
        } else {
            return false;
        }
    } else {
        return value.length == params;
    }

}, "字符长度错误");
$.validator.addMethod("positiveInteger", function (value, element, params) {

    var reg = /^[1-9]\d*$/;

    if (reg.test(value)) {
        return true;
    } else {
        return false;
    }
}, "必须为正整数");



//$.validator.addMethod("regular", function (value, element, params) {
//    var reg = new RegExp(params); alert($(element).parent().html());
//    if (reg.test(value)) {
//        return true;
//    } else {
//        //VisDoc($(this).attr("id"), $(this).attr("regularMsg"));
//        //flag = false;
//        return false;
//    }
//    return this.optional(element) || (value.length <= params);

//}, $.validator.format("不能超过{0}个字节(一个中文字算2个字节)"));