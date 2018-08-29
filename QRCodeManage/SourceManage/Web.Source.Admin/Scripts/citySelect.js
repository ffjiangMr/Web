/// <reference path="jquery-1.10.2.min.js" />
(function ($) {
    $.fn.citySelect = function (settings) {
        if (this.length < 1) {
            return;
        };
        settings = $.extend({
            url: "/Content/Xml/Provinces.xml",
            prov: null,
            city: null,
            dist: null,
            isCityLoad: true,
            required: false
        }, settings);
        var box_obj = this;
        var prov_obj = box_obj.find(".prov");
        var city_obj = box_obj.find(".city");
        var dist_obj = box_obj.find(".dist");
        var prov_val = settings.prov;
        var city_val = settings.city;
        var dist_val = settings.dist;
        var select_prehtml = function (val) { return (settings.required) ? "" : "<option value='0'>请选择" + val + "</option>" };
        var city_json;

        // 赋值市级函数
        var cityStart = function () {
            var prov_id = prov_obj.val();
            city_obj.empty();
            dist_obj.empty();
            // 遍历赋值市级下拉列表
            temp_html = select_prehtml("市/县");
            $.each($(city_json).find("province[postcode='" + prov_id + "']").children(), function (i, city) {
                temp_html += "<option value='" + $(city).attr("postcode") + "'>" + $(city).attr("name") + "</option>";
            });
            city_obj.html(temp_html);
            distStart();
        };

        // 赋值地区（县）函数
        var distStart = function () {
            var prov_id = prov_obj.val();
            var city_id = city_obj.val();
            dist_obj.empty();
            // 遍历赋值市级下拉列表
            temp_html = select_prehtml("区/县");
            $.each($(city_json).find("city[postcode = '" + city_id + "']").children(), function (i, dist) {
                temp_html += "<option value='" + $(dist).attr("postcode") + "'>" + $(dist).attr("name") + "</option>";
            });
            dist_obj.html(temp_html);
        };
        var init = function () {
            // 遍历赋值省份下拉列表
            temp_html = select_prehtml("省");
            $.each($(city_json).find("province"), function (i, prov) {
                temp_html += "<option value='" + $(prov).attr("postcode") + "'>" + $(prov).attr("name") + "</option>";
            });
            prov_obj.html(temp_html);
            if (settings.isCityLoad) {
                cityStart();
            }
            // 若有传入省份与市级的值，则选中。（setTimeout为兼容IE6而设置）
            setTimeout(function () {
                if (settings.prov != null) {
                    prov_obj.val(settings.prov);
                    cityStart();
                    setTimeout(function () {
                        if (settings.city != null) {
                            city_obj.val(settings.city);
                            distStart();
                            setTimeout(function () {
                                if (settings.dist != null) {
                                    dist_obj.val(settings.dist);
                                };
                            }, 1);
                        };
                    }, 1);
                };
            }, 1);
            // 选择省份时发生事件
            prov_obj.bind("change", function () {
                cityStart();
            });

            // 选择市级时发生事件
            city_obj.bind("change", function () {
                distStart();
            });

        };
        // 设置省市json数据
        if (typeof (settings.url) == "string") {
            $.get(settings.url, function (json) {
                city_json = json;
                init();
            });
        } else {
            city_json = settings.url;
            init();
        };
    };
    $.extend({       
        GetSelectName: function (settings) {
            var city_json;
            settings = $.extend({
                url: "/Content/Xml/Provinces.xml",
                id: "0",
                type: ""
            }, settings);
            if (typeof (settings.url) == "string") {
                $.ajaxSetup({
                    async: false
                });
                $.get(settings.url, function (json) {
                    city_json = json;                  
                });
            } else {
                city_json = settings.url;               
            };
            if (settings.id == 0) {
                return "";
            } else {
                var item = $(city_json).find("" + settings.type + "[postcode='" + settings.id + "']");
                return $(item).attr("name");
            }
        }      
    })    
})(jQuery)