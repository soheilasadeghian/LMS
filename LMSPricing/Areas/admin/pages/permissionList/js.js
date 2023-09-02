var pageCount = 10;
var pageIndex = 1;

$(function () {

    initRemoveDate();
    onPermissionPossiobleRoleResult();
    onPermissionDeleteResult();
    onPermissionListResult();
    onPermissionEditResult();
    onPermissionNewResult();
    onPermissionGetResult();
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
    $("#module").on('change', function () {
        DoSearch();
    });
    $("#roleC").on('change', function () {
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
    $(".checkclass").change(function () {
        DoSearch();
    });

}

function DoSearch() {

    var filter = $("#txtFilter").val();
    var orderby = $("#slcOrderby").val();
    var ordertype = $("#slcOrderType").val();
    var fromdate = $("#fromDate").val();
    var todate = $("#toDate").val();
    var moduleID = $("#module").val();
    var roleID = $("#roleC").val();

    var edit = $("#editchk").is(":checked");
    var del = $("#delchk").is(":checked");
    var insert = $("#insertchk").is(":checked");
    var all = $("#allchk").is(":checked");

    core.loader($("#loader"));
    permissionlist.f1(moduleID, roleID, edit, del, insert, all, filter, orderby, ordertype, fromdate, todate, pageIndex, pageCount);

}


function onPermissionDeleteResult() {
    $(document).on("permission-delete-success", function (event, result) {
        DoSearch();
    });
}
function onPermissionPossiobleRoleResult() {
    $(document).on("permission-posrole-success", function (event, result) {
        $("#new-modal-loader").html('دسترسی جدید');
        if (result.result.code != 0) {
            core.message($("#new-modal-msg"), result.message, "danger", false);
            return;;
        }

        var lst = $("#new-roleC");
        lst.html('');

        $.each(result.role, function (index, el) {
            var html = '<option value="' + el.ID + '">' + el.title + "</option>";
            lst.append(html);
        });
        if (lst.html() == '') {
            lst.html('<option value="-1" >هیچ نقشی وجود ندارد</option>')
        }

    });
}
function onPermissionGetResult() {
    $(document).on("permission-get-success", function (event, result) {

        if (result.result.code == 0) {

            $("#edit-modal-loader").html("ویرایش دسترسی");

            $("#edit-module").val(result.per.moduleID);
            $("#edit-roleC").val(result.per.roleID);

            $("#edit-editchk").prop('checked', result.per.edit);
            $("#edit-delchk").prop('checked', result.per.del);
            $("#edit-insertchk").prop('checked', result.per.insert);

        }
        else {
            $("#edit-modal-loader").html("ویرایش دسترسی");
            core.message($("#edit-modal-msg"), result.result.message, "danger", false);
        }

    });
}
function onPermissionNewResult() {
    $(document).on("permission-new-success", function (event, result) {
        $("#new-modal-loader").html("دسترسی جدید");

        if (result.code != 0) {
            core.message($("#new-modal-msg"), result.message, "danger", false);
            return;
        }
        core.message($("#new-modal-msg"), result.message, "success", false);
        DoSearch();
        $('#newModal').modal('hide');
    });
}
function onPermissionEditResult() {
    $(document).on("permission-edit-success", function (event, result) {
        $("#edit-modal-loader").html("ویرایش دسترسی");

        if (result.code != 0) {
            core.message($("#edit-modal-msg"), result.message, "danger", false);
            return;
        }
        core.message($("#edit-modal-msg"), result.message, "success", false);
        DoSearch();
        $('#editModal').modal('hide');

    });
}
function onPermissionListResult() {
    $(document).on("permission-list-success", function (event, result) {
        $("#loader").html(" جستجو");

        if (result.result.code == 0) {
            core.message($("#msg"), result.result.message, "success", false);

            var lst = $("#lst");
            lst.html('');

            $.each(result.per, function (index, el) {

                var html = $('<a  class="list-group-item ">'
                + '<div class="row " data-id="' + el.ID + '">'
                + '<div class="col-sm-4">'
                + '<div class="form-group">'
                + '<div class="visible-xs">'
                + '<label class="block-inline pl-5">ماژول</label>'
                + '</div>'
                + '<span>' + el.moduleTitle + '</span>'
                + '</div>'
                + '</div>'
                + '<div class="col-sm-4">'
                + '<div class="form-group">'
                + '<div class="visible-xs ">'
                + '<label class="block-inline pl-5">نقش</label>'
                + '</div>'
                + '<span>' + el.roleTitle + '</span>'
                + '</div>'
                + '</div>'
                + '<div class="col-sm-4">'
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
        if (confirm("آیا از حذف این دسترسی اطمینان دارید؟")) {
            permissionlist.f4(id);
        }
    });
}

function initEdit(element) {
    element.find('.edit').click(function () {
        var id = $(this).attr('data-id');
        $('#editModal').attr('data-id', id);

        core.loader($("#edit-modal-loader"));

        permissionlist.f5(id);

        $('#editModal').modal('show');
    });
}
function initNew() {

    $("#new-module").on('change', function () {
        core.loader($("#new-modal-loader"));

        permissionlist.f2($("#new-module").val());

    });

    $('#btnNew').click(function () {
        $("#new-module").change();
        $("#new-modal-msg").html('');
        $('#newModal').modal('show');
    });

    $("#new-module").change();

}
function saveNew() {

    $("#new-btnSave").click(function () {

        var moduleID = $("#new-module").val();
        var roleID = $("#new-roleC").val();

        var edit = $("#new-editchk").prop('checked');
        var del = $("#new-delchk").prop('checked');
        var insert = $("#new-insertchk").prop('checked');

        core.loader($("#new-modal-loader"));
        permissionlist.f3(moduleID, roleID, edit, del, insert);

    });

}
function saveEdit() {
    $("#edit-btnSave").click(function () {

        var permissionID = $("#editModal").attr('data-id');

        var moduleID = $("#edit-module").val();
        var roleID = $("#edit-roleC").val();

        var edit = $("#edit-editchk").prop('checked');
        var del = $("#edit-delchk").prop('checked');
        var insert = $("#edit-insertchk").prop('checked');

        core.loader($("#edit-modal-loader"));
        permissionlist.f6(permissionID, moduleID, roleID, edit, del, insert);

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