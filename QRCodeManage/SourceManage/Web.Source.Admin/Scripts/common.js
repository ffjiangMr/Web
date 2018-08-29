(function ($) {
    //获取url参数支持中文
    $.getUrlParam = function (name) {
        var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
        var r = window.location.search.substr(1).match(reg);
        if (r != null) return decodeURI(r[2]); return null;
    }
})(jQuery);

(function ($) {
    $.fn.extend({
        //初始化Tree
        getSelectNode: function () {
            if (this[0].selectNode == undefined) {
                return { id: 0 };
            }
            return this[0].selectNode;
        },
        initTree: function (url, changed, data) {

            var selectNode = undefined;
            this.jstree({
                'core': {
                    "multiple": false,
                    'data': {
                        "dataType": 'json',
                        "type": "post",
                        "url": url,
                        "data": function (node) {
                            if (data != undefined) {
                                if (node.id != "#") {
                                    data.id = node.id
                                }
                                return data;
                            }
                            return { "id": node.id };
                        }
                    },
                    'dblclick_toggle': true          //tree的双击展开
                }
            });
            this.on('loaded.jstree', function (e, data) {
                var inst = data.instance;
                if (this.selectNode == undefined) {
                    this.selectNode = inst.get_node(e.target.firstChild.firstChild.lastChild);
                }
                inst.select_node(this.selectNode);
            });
            this.on("changed.jstree", function (e, data) {
                if (data.node == undefined) {
                    return;
                }
                this.selectNode = data.node;
                if ($.isFunction(changed)) {
                    changed(e, data);
                }
            });
            return this;
        }
    })
})(jQuery);


(function ($) {
    $.fn.extend({
        //初始化Table
        initTable: function (url, columns, height, queryParams, onLoadSuccess, pagination) {
            pagination = pagination == undefined ? true : pagination 
            var $table = this;
            function getHeight() {
                if ((height + "").indexOf("px") > 0) {
                    return height.replace("px", "");
                } else if (isNaN(parseInt(height))) {
                    return 'auto';
                }
                return $(window).height() - height;
            }
            if (typeof (url) != "string") {
                $table.bootstrapTable({
                    data: url, 
                    striped: true,                      //是否显示行间隔色
                    cache: false,                       //是否使用缓存，默认为true，所以一般情况下需要设置一下这个属性（*）
                    pagination: pagination,                   //是否启用分页 
                    pageNumber: 1,                      //初始化加载第一页，默认第一页
                    pageSize: 20,                       //每页的记录行数（*）
                    pageList: [10, 20, 30, 40, 50],     //可供选择的每页的行数（*）
                    clickToSelect: true,                //是否启用点击选中行
                    height: getHeight(),
                    columns: columns,
                    onLoadSuccess: function (data) {
                        if ($.isFunction(onLoadSuccess)) {
                            onLoadSuccess(data);
                        }
                    }
                });
            } else {
                $table.bootstrapTable({
                    url: url,            //数据来源地址
                    method: 'post',                     //数据请求方式
                    striped: true,                      //是否显示行间隔色
                    cache: false,                       //是否使用缓存，默认为true，所以一般情况下需要设置一下这个属性（*）
                    pagination: pagination,                   //是否启用分页
                    sidePagination: 'server',           //在服务器端分页
                    queryParams: queryParams,           //传递参数
                    pageNumber: 1,                      //初始化加载第一页，默认第一页
                    pageSize: 20,                       //每页的记录行数（*）
                    pageList: [10, 20, 30, 40, 50],     //可供选择的每页的行数（*）
                    clickToSelect: true,                //是否启用点击选中行
                    height: getHeight(),
                    columns: columns,
                    onLoadSuccess: function (data) {
                        if ($.isFunction(onLoadSuccess)) {
                            onLoadSuccess(data);
                        }
                    }
                });
            }

            $(window).resize(function () {
                $table.bootstrapTable('resetView', {
                    height: getHeight()
                });
            });
            setTimeout(function () {
                $table.bootstrapTable('resetView');
            }, 200);
            return $table;
        }
    })
})(jQuery);



(function ($) {
    $.fn.extend({
        //from 转Json对象
        formToJson: function (excludeEmpty) {
            var from = this;
            var data = {};
            var array = from.serializeArray();
        
            for (var i = 0; i < array.length; i++) { 
                if (typeof (excludeEmpty) === "boolean" && excludeEmpty && (array[i].value == "" || array[i].value == undefined || array[i].value == null)) {
                    continue;
                }
                data[array[i].name] = array[i].value;
            }
            return data;
        }
    })
})(jQuery);
(function ($) {
    $.fn.extend({
        //Json对象转from 
        JsonToform: function (json) {
            var input = $(this).find('input,select');
            for (var i = 0; i < input.length; i++) {
                var temp = $(input[i]).attr("id");
                if (temp == undefined) continue;
                var type = input[i].type;
                if (type.indexOf("select-") > -1) type = type.split('-')[0];
                if (type == "checkbox") {
                    temp = $(input[i]).parent().next().attr("id"); 
                }
                for (var key in json) {
                    if (temp.toLowerCase() == key.toLocaleLowerCase()) {
                        if (type == "checkbox") {
                            var cbclass = $(input[i]).parent().attr("class");
                            if (cbclass != undefined && cbclass.indexOf("icheckbox") > -1) {
                                $(input[i]).iCheck('check');
                                $(input[i]).trigger("ifChecked");
                            } else {
                                $(input[i]).attr("checked", true);
                            }

                        } else {
                            $(input[i]).val(json[key]);
                            $(input[i]).trigger("change");
                        }

                        continue;
                    }
                }
            }
           
        }
    })
})(jQuery);


var MyValidateIndex = 0;
(function ($) {
    $.fn.extend({
        CloseValiTip: function () {
            var index = this[0].MyValidateIndex;
            var input = this.find('input,select');
            for (var i = 0; i < input.length; i++) {
             
                var id = "popover_" + $(input[i]).attr('id') + "_" + index;
                var tip = $('#' + id, window.parent.document);
                if (tip.length > 0) {
                    tip.remove();
                } else {
                    tip = $('#' + id);
                    tip.remove();
                }

            }

        },
        //from验证
        MyValidate: function (rules, submitHandler, isPopup) {
            MyValidateIndex += 1;
            var myInxex = MyValidateIndex;
            this[0].MyValidateIndex = myInxex;
            var validate = true;
            var from = this;
            for (var i in rules) {
                var obj = from.find("#" + i);
                if (obj.length == 0) continue;
                var type = obj[0].type;
                var eventName = "";
                if (type.indexOf("select-") > -1) type = type.split('-')[0];
                switch (type) {
                    case "number":
                    case "text": 
                    case "select":
                        eventName = "change";
                        break;
                    default:

                }
                function getParent(obj) {
                    var tempMsg = obj.parent();
                    for (var i = 0; i < 8; i++) {
                        if (tempMsg.attr("class") != "form-group") {
                            tempMsg = tempMsg.parent();
                        } else {
                            break;
                        }
                    }
                    var txt = "";
                    try {
                        txt = tempMsg.find(".control-label").html().replace("：", "");
                    } catch (e) {

                    }
                    return txt;
                }
                function showTip(obj, text) {
                    var top = 0;
                    var left = 0;
                    var objId = obj.attr('id');
                    if (isPopup) {
                        var index = window.parent.layer.index;
                        if ($('#layui-layer' + index, window.parent.document).length == 0) {
                            console.info("当前不是layer弹出层");
                            return;
                        }
                        top = $('#layui-layer' + index, window.parent.document).position().top;
                        left = $('#layui-layer' + index, window.parent.document).position().left;
                        var layerTitleHeight = $('#layui-layer' + index, window.parent.document).find(".layui-layer-title").height();
                        top += obj.offset().top + layerTitleHeight - 3;
                        left += obj.offset().left + obj.parent().width();
                        var content = $('<div class="popover fade right in" role="tooltip" id="popover_' + obj.attr('id') + '_' + myInxex + '" style="top: ' + top + 'px; left: ' + left + 'px; display: block;z-index:' + (parent.layer.zIndex + 1) + ';position: fixed"><div class="arrow" style="top: 50%;"></div><h3 class="popover-title" style="display: none;"></h3><div class="popover-content">' + text + '</div></div>');
                        content.click(function () {
                            $(this).remove();
                        });
                        $('body', window.parent.document).append(content);
                        $(window).resize(function () {
                            var index = window.parent.layer.index;
                            var top = $('#layui-layer' + index, window.parent.document).position().top;
                            var left = $('#layui-layer' + index, window.parent.document).position().left;
                            var layerTitleHeight = $('#layui-layer' + index, window.parent.document).find(".layui-layer-title").height();
                            top += obj.offset().top + layerTitleHeight - 3;
                            left += obj.offset().left + obj.parent().width();
                            $('popover_' + objId + '_' + myInxex, window.parent.document).css("top", top + "px");
                            $('popover_' + objId + '_' + myInxex, window.parent.document).css("left", left + "px");
                        });
                    } else {
                        //$("div[class^='popover_']").remove();
                        if ($("#popover_" + obj.attr('id') + "_" + myInxex).length == 0) {
                            top = obj.offset().top - 3;
                            left = obj.offset().left + obj.width() + 25;
                            var scrollTop = $('body').scrollTop();
                            var scrollLeft = $('body').scrollLeft();
                            scrollTop = scrollTop == null ? 0 : scrollTop;
                            scrollLeft = scrollLeft == null ? 0 : scrollLeft;
                            top -= scrollTop;
                            left -= scrollLeft;
                            var content = $('<div class="popover fade right in" role="tooltip" id="popover_' + objId + '_' + myInxex + '" style="display: block;z-index:' + 99999 + ';position: fixed"><div class="arrow" style="top: 50%;"></div><h3 class="popover-title" style="display: none;"></h3><div class="popover-content">' + text + '</div></div>');
                            content.css({ "top": top + "px", "left": left + "px" });
                            content.click(function () {
                                $(this).remove();
                            });
                            $('body').append(content);
                            $(window).resize(function () {
                                top = $("#" + objId).offset().top - 3;
                                left = $("#" + objId).offset().left + obj.width() + 25;
                                var scrollTop = $('body').scrollTop();
                                var scrollLeft = $('body').scrollLeft();
                                scrollTop = scrollTop == null ? 0 : scrollTop;
                                scrollLeft = scrollLeft == null ? 0 : scrollLeft;
                                top -= scrollTop;
                                left -= scrollLeft;
                                $('#popover_' + objId + '_' + myInxex).css({ "top": top + "px", "left": left + "px" });
                            });
                            $(window).scroll(function () {
                                top = $("#" + objId).offset().top - 3;
                                left = $("#" + objId).offset().left + obj.width() + 25;

                                var scrollTop = $('body').scrollTop();
                                var scrollLeft = $('body').scrollLeft();
                                scrollTop = scrollTop == null ? 0 : scrollTop;
                                scrollLeft = scrollLeft == null ? 0 : scrollLeft;
                                top -= scrollTop;
                                left -= scrollLeft;

                                $('#popover_' + objId + '_' + myInxex).css({ "top": top + "px", "left": left + "px" });
                            });
                        }
                    }
                }





                obj.rule = rules[i]
                obj.bind(eventName, function () {
                    var id = $(this).attr('id');
                    var val = $(this).val();

                    if (isPopup) {
                        var tip = $('#popover_' + $(this).attr('id') + '_' + myInxex, window.parent.document);
                        if (tip.length > 0) {
                            tip.remove();
                        }
                    } else {
                        var tip = $('#popover_' + $(this).attr('id') + '_' + myInxex);
                        if (tip.length > 0) {
                            tip.remove();
                        }
                    }

                    var vali1 = true;
                    if ((rules[id].required != undefined && rules[id].required)) {
                        vali1 = val != "" && val != undefined && val != null;
                    }
                    var vali2 = true;
                    if (vali1 && val != "") {
                        var ret = rules[id].validate;
                        if ($.isFunction(ret)) {
                            vali2 = ret(val, $(this));
                        } else if (ret == undefined) {
                            vali2 = true;
                        } else {
                            ret = new RegExp(ret);
                            vali2 = ret.test(val);
                        }
                    }


                    validate = validate && vali1 && vali2;
                    if (!vali1) {
                        var requiredMsg = rules[id].requiredMsg;
                        if (requiredMsg == undefined) {
                            rules[id].requiredMsg = "请输入" + getParent($(this));
                        }
                        showTip($(this), rules[id].requiredMsg)
                    } else if (!vali2) {
                        var validateMsg = rules[id].validateMsg;
                        if (validateMsg == undefined) {
                            rules[id].validateMsg = getParent($(this)) + "格式不正确！";
                        }
                        showTip($(this), rules[id].validateMsg)
                    }
                });
            }
            from.submit(function () {
                validate = true;
                var input = $(this).find('input,select');
                for (var i = 0; i < input.length; i++) {
                    var id = $(input[i]).attr("id");
                    if (id == undefined || rules[id] == undefined) {
                        continue;
                    }
                    var type = input[i].type;
                    if (type.indexOf("select-") > -1) type = type.split('-')[0];
                    switch (type) {
                        case "number":
                        case "text": 
                        case "select":
                            $(input[i]).trigger("change");
                            break;
                        default:

                    }
                }
                if (validate) {
                    if ($.isFunction(submitHandler)) {
                        return submitHandler(this);
                    }
                    return true;
                } else {
                    return false;
                }
            });
        }
    })
})(jQuery);
