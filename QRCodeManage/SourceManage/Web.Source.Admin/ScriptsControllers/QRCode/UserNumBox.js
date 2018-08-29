$(function () {
    initTable();
});
var $table = $('#table');
function initTable() {
    $table.bootstrapTable({
        url: '/UserNumBox/IndexAsync',            //数据来源地址
        method: 'post',                     //数据请求方式
        striped: true,                      //是否显示行间隔色
        cache: false,                       //是否使用缓存，默认为true，所以一般情况下需要设置一下这个属性（*）
        pagination: true,                   //是否启用分页
        sidePagination: 'server',           //在服务器端分页
        queryParams: queryParams,           //传递参数
        pageNumber: 1,                      //初始化加载第一页，默认第一页
        pageSize: 20,                       //每页的记录行数（*）
        pageList: [10, 20, 30, 40, 50],     //可供选择的每页的行数（*）
        clickToSelect: true,                //是否启用点击选中行
        //toolbar: "#tableToolBar",
        height: getHeight(),
        columns: [{
            width: 38,
            title: '行号',
            align: 'center',
            formatter: function (value, row, index) { return index + 1; }
        }, {
            field: 'userName',
            title: '应用名称',
            align: 'center'
        }, {
            field: 'startNumber',
            title: '起始码',
            align: 'center'
        }, {
            field: 'endNumber',
            title: '结束码',
            align: 'center'
        }, {
            field: 'numCount',
            title: '码数量',
            align: 'center'
        }, {
            field: 'useCount',
            title: '以填充数量',
            align: 'center'
        }, {
            field: 'numBoxStart',
            title: '状态',
            align: 'center',
            formatter: operateFormatterNumBoxStart
        }, {
            field: 'createdTime',
            title: '创建时间',
            align: 'center'
        }],
        onLoadSuccess: function (data) {
            if (data.total == 0) {
                $(".ibox-content").css("padding-bottom", "22px");
            } else {
                $(".ibox-content").css("padding-bottom", "4px");
            }
        }
    });
}

//查询的参数
var queryParams = function (params) {
    var temp = {
        pageSize: params.limit,                             //行数
        pageIndex: (params.offset / params.limit) + 1,      //页码
        search: $('#searchKey').val()                       //查询内容
    };
    console.info(temp);
    return temp;
};

//刷新表格数据
var refreshTable = function () {
    $table.bootstrapTable('refresh');
}





//获取表格高度
function getHeight() {
    return $(window).height() - 150;
}

//重置表格高度
setTimeout(function () {
    $table.bootstrapTable('resetView');
}, 200);

//重置表格高度
$(window).resize(function () {
    $table.bootstrapTable('resetView', {
        height: getHeight()
    });
});

//刷新按钮
$('#refreshBtn').click(function (e) {
    $table.bootstrapTable('refresh');
});

//搜索按钮
$('#searchBtn').click(function (e) {
    $table.bootstrapTable('refresh');
});



function operateFormatterNumBoxStart(value, row, index) {
    var v = value;
    return [
        getUserStare(value),
    ].join('');
}


function getUserStare(value) {
    if (value == 1) {
        return "正常";
    } else {
        return "禁用";
    }
}