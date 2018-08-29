var map = null;

//创建地图
function createMap(City, Address, Remark) {
    // 创建地址解析器实例   
    var myGeo = new BMap.Geocoder();
    // 将地址解析结果显示在地图上，并调整地图视野
    myGeo.getPoint(Address, function (point) {

        if (point) {
            showMap(point, City, Address, Remark);
        }
    }, City);
};
function createMap(point) {
    map = new BMap.Map("rMap", {
        enableHighResolution: true //是否开启高清
    });
    map.centerAndZoom(point, 13); //初始化地图
    map.enableInertialDragging(); //开启关系拖拽
    map.enableScrollWheelZoom();  //开启鼠标滚动缩放

    map.addControl(new BMap.NavigationControl({ anchor: BMAP_ANCHOR_TOP_LEFT, type: BMAP_NAVIGATION_CONTROL_ZOOM }));  //添加默认缩放平移控件
    map.panTo(point);

}

//显示地图
function showMap(point, city, address, des) {
    var sContent = des;
    // 创建地图对象并初始化
    map = new BMap.Map("rMap", {
        enableHighResolution: true //是否开启高清
    });
    map.centerAndZoom(point, 13); //初始化地图
    map.enableInertialDragging(); //开启关系拖拽
    map.enableScrollWheelZoom();  //开启鼠标滚动缩放
    var marker = new BMap.Marker(point);

    //放大缩小控件
    map.addControl(new BMap.NavigationControl({ anchor: BMAP_ANCHOR_TOP_LEFT, type: BMAP_NAVIGATION_CONTROL_ZOOM }));  //添加默认缩放平移控件
    //map.addControl(new BMap.NavigationControl({ anchor: BMAP_ANCHOR_TOP_RIGHT, type: BMAP_NAVIGATION_CONTROL_SMALL }));  //右上角，仅包含平移和缩放按钮
    // map.addControl(new BMap.NavigationControl({ anchor: BMAP_ANCHOR_BOTTOM_LEFT, type: BMAP_NAVIGATION_CONTROL_PAN }));  //左下角，仅包含平移按钮
    // map.addControl(new BMap.NavigationControl({ anchor: BMAP_ANCHOR_BOTTOM_RIGHT, type: BMAP_NAVIGATION_CONTROL_ZOOM }));  //右下角，仅包含缩放按钮
    //信息框
    map.addOverlay(marker);
    //map.panTo(point);
    //map.setCenter(city); 
    marker.enableDragging();    //可拖拽
    marker.addEventListener("dragend", function (e) { //监听标注的dragend事件，获取拖拽后地理坐标
        $("#Longitude").val(e.point.lng);
        $("#Latitude").val(e.point.lat);

        infoWindow.close();
        var pt = e.point;
    })

    var infoWindow = new BMap.InfoWindow(sContent);  // 创建信息窗口对象
    marker.addEventListener("click", function () {
        this.openInfoWindow(infoWindow);
        ////图片加载完毕重绘infowindow
        document.getElementById('imgDemo').onLoad = function () {
            infoWindow.redraw();
        }
    });
    marker.openInfoWindow(infoWindow);

    //鼠标单击事件
    function showInfo(e) {
        var jingdu = e.point.lng;
        var weidu = e.point.lat;
        $("#Txt_longitude").val(jingdu);
        $("#Txt_latitude").val(weidu);
        $("#mapdiv").hide();
    }
    map.addEventListener("click", showInfo);
}

var dragendRemark = "";
function showDragendMarker(point, sContent) {
    var marker = new BMap.Marker(point);

    //放大缩小控件
    map.addControl(new BMap.NavigationControl({ anchor: BMAP_ANCHOR_TOP_LEFT, type: BMAP_NAVIGATION_CONTROL_ZOOM }));  //添加默认缩放平移控件
    //map.addControl(new BMap.NavigationControl({ anchor: BMAP_ANCHOR_TOP_RIGHT, type: BMAP_NAVIGATION_CONTROL_SMALL }));  //右上角，仅包含平移和缩放按钮
    // map.addControl(new BMap.NavigationControl({ anchor: BMAP_ANCHOR_BOTTOM_LEFT, type: BMAP_NAVIGATION_CONTROL_PAN }));  //左下角，仅包含平移按钮
    // map.addControl(new BMap.NavigationControl({ anchor: BMAP_ANCHOR_BOTTOM_RIGHT, type: BMAP_NAVIGATION_CONTROL_ZOOM }));  //右下角，仅包含缩放按钮
    //信息框
    map.addOverlay(marker);
    map.panTo(point);
    marker.enableDragging();    //可拖拽
    marker.addEventListener("dragend", function (e) { //监听标注的dragend事件，获取拖拽后地理坐标
        $("#Longitude").val(e.point.lng);
        $("#Latitude").val(e.point.lat);

        showGeolocation(e.point, marker);
        var pt = e.point;
    })

    var infoWindow = new BMap.InfoWindow(sContent);  // 创建信息窗口对象
    marker.addEventListener("click", function () {
        this.openInfoWindow(infoWindow);
        ////图片加载完毕重绘infowindow
        //document.getElementById('imgDemo').onLoad = function () {
        //    infoWindow.redraw();
        //}
    });
    marker.openInfoWindow(infoWindow);

}

//驾车路线
function showDrLine(map, address) {
    var geolocation = new BMap.Geolocation();
    geolocation.getCurrentPosition(function (r) {
        if (this.getStatus() == BMAP_STATUS_SUCCESS) {
            var gc = new BMap.Geocoder();
            var point = new BMap.Point(r.point.lng, r.point.lat);
            //获取地址信息
            gc.getLocation(point, function (rs) {
                showLocationInfo(point, rs);
            });
        }
        else {
            alert('failed' + this.getStatus());
        }
    }, { enableHighAccuracy: true })
    //关于状态码
    //BMAP_STATUS_SUCCESS	检索成功。对应数值“0”。
    //BMAP_STATUS_CITY_LIST	城市列表。对应数值“1”。
    //BMAP_STATUS_UNKNOWN_LOCATION	位置结果未知。对应数值“2”。
    //BMAP_STATUS_UNKNOWN_ROUTE	导航结果未知。对应数值“3”。
    //BMAP_STATUS_INVALID_KEY	非法密钥。对应数值“4”。
    //BMAP_STATUS_INVALID_REQUEST	非法请求。对应数值“5”。
    //BMAP_STATUS_PERMISSION_DENIED	没有权限。对应数值“6”。(自 1.1 新增)
    //BMAP_STATUS_SERVICE_UNAVAILABLE	服务不可用。对应数值“7”。(自 1.1 新增)
    //BMAP_STATUS_TIMEOUT	超时。对应数值“8”。(自 1.1 新增)


}
//显示当前地址信息
function showLocationInfo(pt, rs) {
    var addComp = rs.addressComponents;
    var addr = "当前位置：" + addComp.province + ", " + addComp.city + ", " + addComp.district + ", " + addComp.street + ", " + addComp.streetNumber + "<br />";
    addr += "纬度: " + pt.lat + ", " + "经度：" + pt.lng;
    alert(addr);
}
//创建地图
function loadMap() {
    var address = $("#Txt_address").val();
    var city = $("#Txt_City option:selected").html();
    if (city == "请选择..." || address.length == 0) {
        alert("请先设置所在城市及详细地址");
        return;
    }
    $("#mapdiv").show();
    createMap(city, address, "");
}
//根据地址获取坐标点
function GetMapPoint(City, Address) {
    // 创建地址解析器实例   
    var myGeo = new BMap.Geocoder();
    myGeo.getPoint(Address, function (point) {
        if (point) {
            var lat = point.lat;
            var lng = point.lng;
            $("#Longitude").val(lng);
            $("#Latitude").val(lat);
        }
    }, City);
};
//根据地址获取坐标点
function GetMapPoint(Address) {
    // 创建地址解析器实例   
    var myGeo = new BMap.Geocoder();
    myGeo.getPoint(Address, function (point) {
        // alert(point);
        if (point) {
            var lat = point.lat;
            var lng = point.lng;
            $("#Longitude").val(lng);
            $("#Latitude").val(lat);

            map.clearOverlays();
            //clearAll(map);//清除 marker
            showDragendMarker(point, Address);
        }
    }, "");
};
//设置调用坐标点
function initGetPoint(addresstr) {
    addresstr = $(addresstr).val();
    GetMapPoint(addresstr);
}

//添加指定范围
function createCircle(point, radius) {
    var circle = new BMap.Circle(point, radius);
    circle.setFillColor("#A6CBA1"); //填充颜色
    circle.setStrokeColor("#A6CBA1"); //边线颜色
    map.addOverlay(circle);
}

function showGeolocation(point, marker) {
    var gc = new BMap.Geocoder();

    gc.getLocation(point, function (rs) {
        var addComp = rs.addressComponents;
        var r = addComp.province + addComp.city + addComp.district + addComp.street + addComp.streetNumber;
        //alert(r);

        marker.openInfoWindow(new BMap.InfoWindow(r));
        $("#UnitAddress").val(r);
    });

}