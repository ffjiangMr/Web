Date.prototype.Format = function (fmt) { //author: meizz   
    var o = {
        "M+": this.getMonth() + 1,                 //月份   
        "d+": this.getDate(),                    //日   
        "h+": this.getHours(),                   //小时   
        "m+": this.getMinutes(),                 //分   
        "s+": this.getSeconds(),                 //秒   
        "q+": Math.floor((this.getMonth() + 3) / 3), //季度   
        "S": this.getMilliseconds()             //毫秒   
    };
    if (/(y+)/.test(fmt))
        fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o)
        if (new RegExp("(" + k + ")").test(fmt))
            fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
    return fmt;
}
Number.prototype.NoToChineseUpper = function () {
    if (!/^\d*(\.\d*)?$/.test(this)) { alert("Number is wrong!"); return "Number is wrong!"; }
    var AA = new Array("零", "壹", "贰", "叁", "肆", "伍", "陆", "柒", "捌", "玖");
    var BB = new Array("", "拾", "佰", "仟", "萬", "億", "点", "");
    var a = ("" + this).replace(/(^0*)/g, "").split("."), k = 0, re = "";
    for (var i = a[0].length - 1; i >= 0; i--) {
        switch (k) {
            case 0: re = BB[7] + re; break;
            case 4: if (!new RegExp("0{4}\\d{" + (a[0].length - i - 1) + "}$").test(a[0]))
                re = BB[4] + re; break;
            case 8: re = BB[5] + re; BB[7] = BB[5]; k = 0; break;
        }
        if (k % 4 == 2 && a[0].charAt(i + 2) != 0 && a[0].charAt(i + 1) == 0) re = AA[0] + re;
        if (a[0].charAt(i) != 0) re = AA[a[0].charAt(i)] + BB[k % 4] + re; k++;
    }

    if (a.length > 1) //加上小数部分(如果有小数部分) 
    {
        re += BB[6];
        for (var i = 0; i < a[1].length; i++) re += AA[a[1].charAt(i)];
    }
    return re;
}
Number.prototype.NoToChineseLower = function () {
    if (!/^\d*(\.\d*)?$/.test(this)) { alert("Number is wrong!"); return "Number is wrong!"; }
    var k = new change(this+"");
    return k.pri_ary();
};
///数组排序
Array.prototype.unique4 = function () {
    this.sort();
    var re = [this[0]];
    for (var i = 1; i < this.length; i++) {
        if (this[i] !== re[re.length - 1]) {
            re.push(this[i]);
        }
    }
    return re;
}
var _change = {
    ary0: ["零", "一", "二", "三", "四", "五", "六", "七", "八", "九"],
    ary1: ["", "十", "百", "千"],
    ary2: ["", "万", "亿", "兆"],
    init: function (name) {
        this.name = name;
    },
    strrev: function () {
        var ary = []
        for (var i = this.name.length; i >= 0; i--) {
            ary.push(this.name[i])
        }
        return ary.join("");
    }, //倒转字符串。
    pri_ary: function () {
        var $this = this
        var ary = this.strrev();
        var zero = ""
        var newary = ""
        var i4 = -1
        for (var i = 0; i < ary.length; i++) {
            if (i % 4 == 0) { //首先判断万级单位，每隔四个字符就让万级单位数组索引号递增
                i4++;
                newary = this.ary2[i4] + newary; //将万级单位存入该字符的读法中去，它肯定是放在当前字符读法的末尾，所以首先将它叠加入$r中，
                zero = ""; //在万级单位位置的“0”肯定是不用的读的，所以设置零的读法为空

            }
            //关于0的处理与判断。
            if (ary[i] == '0') { //如果读出的字符是“0”，执行如下判断这个“0”是否读作“零”
                switch (i % 4) {
                    case 0:
                        break;
                        //如果位置索引能被4整除，表示它所处位置是万级单位位置，这个位置的0的读法在前面就已经设置好了，所以这里直接跳过
                    case 1:
                    case 2:
                    case 3:
                        if (ary[i - 1] != '0') {
                            zero = "零"
                        }
                        ; //如果不被4整除，那么都执行这段判断代码：如果它的下一位数字（针对当前字符串来说是上一个字符，因为之前执行了反转）也是0，那么跳过，否则读作“零”
                        break;

                }

                newary = zero + newary;
                zero = '';
            }
            else { //如果不是“0”
                newary = this.ary0[parseInt(ary[i])] + this.ary1[i % 4] + newary; //就将该当字符转换成数值型,并作为数组ary0的索引号,以得到与之对应的中文读法，其后再跟上它的的一级单位（空、十、百还是千）最后再加上前面已存入的读法内容。
            }

        }
        if (newary.indexOf("零") == 0) {
            newary = newary.substr(1)
        }//处理前面的0
        return newary;
    }
}
function change() {
    this.init.apply(this, arguments);
}
change.prototype = _change

function VisDoc(id, txt) {
    $("#" + id).after("<span class='help-block m-b-none'><i class='fa fa-times-circle'></i> " + txt + "</span>");
    $("#" + id).focus();
}
function VisText(formId) {
    var flag = true; 
    $("#" + formId).find("input[type=text]").each(function () {
        if ($(this).attr("switch") != "true") {
            return true;
        }
        if ($(this).next().attr("class") == "help-block m-b-none") {
            $(this).next().remove();
        }
        if ($(this).attr("isNull") == "true" && $(this).attr("isNull") != undefined) {
            if ($(this).val() == "" || $(this).val()==undefined) {
                var txt = $(this).parent().prev().text() + "不能为空！";
                VisDoc($(this).attr("id"), txt);
                flag = false;
                return true;
            } 
        }
        if ($(this).attr("length") != "" && $(this).attr("length") != undefined) {
            var tt = $(this).attr("length").split('-').length;
            if ($(this).attr("length").split('-').length==2) {
                var minLeng = Number($(this).attr("length").split('-')[0]);
                var MaxLeng = Number($(this).attr("length").split('-')[1]);
                if (minLeng > $(this).val().length || $(this).val().length > MaxLeng) {
                    var txt = $(this).parent().prev().text() + "长度只能在" + minLeng + "至" + MaxLeng + "之间！";
                    VisDoc($(this).attr("id"), txt);
                    flag = false;
                    return true;
                }
            } 
        }
        if ($(this).attr("regular") != "" && $(this).attr("regular") != undefined) {
            var reg  =new RegExp($(this).attr("regular")); 
            if (!reg.test($(this).val())) {
                VisDoc($(this).attr("id"), $(this).attr("regularMsg"));
                flag = false;
                return true;
            } 
        }
    })
    return flag;
}