/////高德地图，查询
////地图加载
//var map = new AMap.Map("container", {
//    resizeEnable: true
//});
////输入提示
//var autoOptions = {
//    input: "tipinput"
//};
//var auto = new AMap.Autocomplete(autoOptions);
//var placeSearch = new AMap.PlaceSearch({
//    map: map
//});  //构造地点查询类
//AMap.event.addListener(auto, "select", select);//注册监听，当选中某条记录时会触发
//function select(e) {
//    placeSearch.setCity(e.poi.adcode);
//    placeSearch.search(e.poi.name);  //关键字查询查询
//}


/////拖拽选址
//AMapUI.loadUI(['misc/PositionPicker'], function (PositionPicker) {
//    var map = new AMap.Map('container', {
//        zoom: 16,
//        scrollWheel: false
//    })
//    var positionPicker = new PositionPicker({
//        mode: 'dragMap',
//        map: map
//    });

//    positionPicker.on('success', function (positionResult) {
//        document.getElementById('lnglat').innerHTML = positionResult.position;
//        document.getElementById('address').innerHTML = positionResult.address;
//        document.getElementById('nearestJunction').innerHTML = positionResult.nearestJunction;
//        document.getElementById('nearestRoad').innerHTML = positionResult.nearestRoad;
//        document.getElementById('nearestPOI').innerHTML = positionResult.nearestPOI;
//    });
//    positionPicker.on('fail', function (positionResult) {
//        document.getElementById('lnglat').innerHTML = ' ';
//        document.getElementById('address').innerHTML = ' ';
//        document.getElementById('nearestJunction').innerHTML = ' ';
//        document.getElementById('nearestRoad').innerHTML = ' ';
//        document.getElementById('nearestPOI').innerHTML = ' ';
//    });
//    var onModeChange = function (e) {
//        positionPicker.setMode(e.target.value)
//    }
//    var startButton = document.getElementById('start');
//    var stopButton = document.getElementById('stop');
//    var dragMapMode = document.getElementsByName('mode')[0];
//    var dragMarkerMode = document.getElementsByName('mode')[1];
//    AMap.event.addDomListener(startButton, 'click', function () {
//        positionPicker.start(map.getBounds().getSouthWest())
//    })
//    AMap.event.addDomListener(stopButton, 'click', function () {
//        positionPicker.stop();
//    })
//    AMap.event.addDomListener(dragMapMode, 'change', onModeChange)
//    AMap.event.addDomListener(dragMarkerMode, 'change', onModeChange);
//    positionPicker.start();
//    map.panBy(0, 1);

//    map.addControl(new AMap.ToolBar({
//        liteStyle: true
//    }))
//});