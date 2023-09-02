var pageCount = 10;
var pageIndex = 1;

$(function () {

    initRemoveDate();
    onRoleDeleteResult();
    onRoleListResult();
    onRoleEditResult();
    onRoleNewResult();
    onRoleGetResult();
    setup_search();
    saveEdit();
    saveNew();
    initNew();
    DoSearch();
});

function setup_search() {

    $("#slcOrderby").on('change', function () {
        DoSearch();
    });
    $("#slcOrderType").on('change', function () {
        DoSearch();
    });
    jQuery('#txtFilter').on('input propertychange paste change', function () {
        DoSearch();
    });
    jQuery('#fromDate').on('input propertychange paste change', function () {
        DoSearch();
    });
    jQuery('#toDate').on('input propertychange paste change', function () {
        DoSearch();
    });

}

function DoSearch() {

    var filter = $("#txtFilter").val();
    var orderby = $("#slcOrderby").val();
    var ordertype = $("#slcOrderType").val();
    var fromdate = $("#fromDate").val();
    var todate = $("#toDate").val();

    core.loader($("#loader"));
    rolelist.f1(filter, orderby, ordertype, fromdate, todate, pageIndex, pageCount);

}


function onRoleDeleteResult() {
    $(document).on("role-delete-success", function (event, result) {
        DoSearch();
    });
}
function onRoleGetResult() {
    $(document).on("role-get-success", function (event, result) {

        if (result.result.code == 0) {

            $("#modal-loader").html("ویرایش نقش - " + result.role.title);
            $("#modal-txttitle").val(result.role.title);
            $("#modal-txtdes").val(result.role.Description);

        }
        else {
            $("#modal-loader").html("ویرایش نقش");
            core.message($("#modal-msg"), result.result.message, "danger", false);
        }

    });
}
function onRoleNewResult() {
    $(document).on("role-new-success", function (event, result) {
        $("#new-modal-loader").html("نقش جدید");

        if (result.code != 0) {
            core.message($("#new-modal-msg"), result.message, "danger", false);
            return;
        }
        core.message($("#new-modal-msg"), result.message, "success", false);
        DoSearch();
        $('#newModal').modal('hide');

    });
}
function onRoleEditResult() {
    $(document).on("role-edit-success", function (event, result) {
        $("#modal-loader").html("ویرایش نقش");

        if (result.code != 0) {
            core.message($("#modal-msg"), result.message, "danger", false);
            return;
        }
        core.message($("#modal-msg"), result.message, "success", false);
        DoSearch();
        $('#editModal').modal('hide');

    });
}
function onRoleListResult() {
    $(document).on("role-list-success", function (event, result) {
        $("#loader").html(" جستجو");

        if (result.result.code == 0) {
            core.message($("#msg"), result.result.message, "success", false);

            var lst = $("#lst");
            lst.html('');

            $.each(result.role, function (index, el) {

                var html = $('<a  class="list-group-item ">'
                + '<div class="row " data-id="' + el.ID + '">'
                + '<div class="col-sm-3">'
                + '<div class="form-group">'
                + '<div class="visible-xs">'
                + '<label class="block-inline pl-5">عنوان</label>'
                + '</div>'
                + '<span>' + el.title + '</span>'
                + '</div>'
                + '</div>'
                + '<div class="col-sm-6">'
                + '<div class="form-group">'
                + '<div class="visible-xs ">'
                + '<label class="block-inline pl-5">توضیحات</label>'
                + '</div>'
                + '<span>' + el.Description + '</span>'
                + '</div>'
                + '</div>'
                + '<div class="col-sm-3">'
                + '<div class="form-group">'
                + '<div class="visible-xs">'
                + '<label class="block-inline pl-5">تاریخ ساخت</label>'
                + '</div>'
                + '<span>' + el.regDate + '</span>'
                + '</div>'
                + '</div>'
                + '<div class="tools ">'
                + '<span data-id="' + el.ID + '" class="block ml-10 edit left clickable" data-toggle="tooltip" data-placement="bottom" title="ویرایش"><i class="fa fa-edit"></i></span>'
                + '<span data-id="' + el.ID + '" class="block ml-10 delete left clickable" data-toggle="tooltip" data-placement="bottom" title="حذف"><i class="fa fa-remove"></i></span>'
                + '</div>'
                + '</div></a>');


                lst.append(html);
                initDelete(html);
                initEdit(html);
            });

            initPagination(result.count);

            $('[data-toggle="tooltip"]').tooltip();

            if (result.count == 0) {
                lst.html('<a  class="list-group-item ">' + "هیچ موردی یافت نشد" + '</a>')
            }

        }
        else {
            core.message($("#msg"), result.result.message, "danger", false);
        }


    });
}

function initDelete(element) {
    element.find('.delete').click(function () {
        var id = $(this).attr('data-id');
        if (confirm("آیا از حذف این نقش اطمینان دارید؟")) {
            rolelist.f2(id);
        }
    });
}

function initEdit(element) {
    element.find('.edit').click(function () {
        var id = $(this).attr('data-id');
        $('#editModal').attr('data-id', id);

        core.loader($("#modal-loader"));

        rolelist.f4(id);

        $("#modal-txttitle").val('');
        $("#modal-txtdes").val('');

        $('#editModal').modal('show');
    });
}
function initNew() {
    $('#btnNew').click(function () {
        var id = $(this).attr('data-id');
        $('#newModal').attr('data-id', id);

        $("#new-modal-txttitle").val('');
        $("#new-modal-txtdes").val('');

        $('#newModal').modal('show');
    });
}

function saveNew() {
    $("#new-btnSave").click(function () {

        var title = $("#new-modal-txttitle").val();
        var des = $("#new-modal-txtdes").val();

        core.loader($("#new-modal-loader"));
        rolelist.f6(title, des);

    });
}

function saveEdit() {
    $("#btnSave").click(function () {

        var roleID = $("#editModal").attr('data-id');

        var title = $("#modal-txttitle").val();
        var des = $("#modal-txtdes").val();

        core.loader($("#modal-loader"));
        rolelist.f5(roleID, title, des);

    });
}


function initPagination(count) {

    var paging = $("#paging");
    paging.html("");

    for (var i = 0; i < count; i++) {
        var html = '<li ' + (pageIndex == (i + 1) ? 'class="active"' : '') + ' ><a href="#">' + (i + 1) + '</a></li>';
        paging.append(html);
    }

    paging.find('li').click(function () {
        pageIndex = $(this).find('a').text();
        DoSearch();
    });

}

function initRemoveDate() {
    $(".removeDate").click(function () {
        $(this).parent().find('input').val('');
        DoSearch();
    });
}