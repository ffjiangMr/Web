$(function () {
    initTable();
});
var $table = $('#table');
function initTable() {
    $table.bootstrapTable({
        url: '/UserAccount/IndexAsync',            //数据来源地址
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
            field: 'selected',
            checkbox: true,
        }, {
            width: 38,
            title: '行号',
            align: 'center',
            formatter: function (value, row, index) { return index + 1; }
        }, {
            field: 'userName',
            title: '应用名称',
            align: 'center'
        }, {
            field: 'position',
            title: '所属行业',
            align: 'center'
        }, {
            field: 'phone',
            title: '联系电话',
            align: 'center'
        }, {
            field: 'guIdNumber',
            title: '关联标识',
            align: 'center'

        }, {
            field: 'useCount',
            title: '发放码数量',
            align: 'center'
        }, {
            field: 'userStare',
            title: '状态',
            align: 'center',
            formatter: operateFormatterUserStare
        }, {
            field: 'createdTime',
            title: '创建时间',
            align: 'center'
        }, {
            field: 'id',
            title: '操作',
            align: 'center',
            events: operateEvents,
            formatter: operateFormatter
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

//自定义的列事件
function operateFormatter(value, row, index) {
    return [
        '<button type="button" class="btn btn-primary btn-sm" onclick ="editData(' + value + ')"  >编辑</button> ',
        '<button type="button" class="btn btn-primary btn-sm" onclick ="editFenNum(' + value + ')"  >分码管理</button> ',
        '<button type="button" class="btn btn-primary btn-sm" onclick ="editYongNum(' + value + ')"  >用码管理</button> ',
    ].join('');
}

window.operateEvents = {
    'click .edit': function (e, value, row, index) {

    },
    'click .remove': function (e, value, row, index) {

    }
};


//function isTrueFormatter(value, row, index) {
//    return value ? '<i class="glyphicon glyphicon-ok"></i>' : '<i class="glyphicon glyphicon-remove"></i>'
//}

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



//编辑按钮
$('#editBtn').click(function (e) {
    var selected = $table.bootstrapTable('getSelections');

    if (selected.length > 1) {
        layer.msg("一次只能修改一条数据！", { icon: 5, time: 2000 });
        return;
    }

    if (selected.length == 0) {
        layer.msg("请选择您要修改的数据！", { icon: 5, time: 2000 });
        return;
    }
    editData(selected[0].id);
});

//打开编辑窗口
function editData(id) {
    parent.layer.open({
        type: 2,
        title: '修改用户',
        shadeClose: false,
        shade: 0.4,
        area: ['600px', '650px'],
        content: '/UserAccount/Edit?id=' + id,
        end: function () {
            refreshTable();
        }
    });
}
//分码
function editFenNum(id) {
    parent.layer.open({
        type: 2,
        title: '分码管理',
        shadeClose: false,
        shade: 0.4,
        area: ['1000px', '700px'],
        content: '/UserAccount/UserAllocationViwe?id=' + id,
        end: function () {
            refreshTable();
        }
    });

}

//用码
function editYongNum(id) {
    parent.layer.open({
        type: 2,
        title: '用码管理',
        shadeClose: false,
        shade: 0.4,
        area: ['1000px', '700px'],
        content: '/UserAccount/UserNumBoxViwe?id=' + id,
        end: function () {
            refreshTable();
        }
    });

}


function operateFormatterUserStare(value, row, index) {
    var v = value;
    return [
        getUserStare(value),
    ].join('');
}


function getUserStare(value) {
    if (value == 1) {
        return "申请";
    } else if (value==2) {
        return "通过";
    } else if (value == 3) {
        return "拒绝";
    } else if (value == 4) {
        return "禁用";
    }
}