var pageCount = 10;
var pageIndex = 1;

$(function () {

    initRemoveDate();
    onUserRemoveResult();
    onUserDeleteResult();
    onUserListResult();
    onUserEditResult();
    onUserNewResult();
    onUserGetResult();
    setup_search();
    saveEdit();
    saveNew();
    initNew();
    iniSelectingPermission();
    DoSearch();
});

function setup_search() {

    $("#slcStatus").on('change', function () {
        DoSearch();
    });
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
    var status = $("#slcStatus").val();
    var orderby = $("#slcOrderby").val();
    var ordertype = $("#slcOrderType").val();
    var fromdate = $("#fromDate").val();
    var todate = $("#toDate").val();

    core.loader($("#loader"));
    userlist.f1( pageIndex, pageCount);

}

function iniSelectingPermission() {

    $("#add-new-btnSave").click(function () {
        var lst = $("#sl-list");

        var PermissionTitle = $("#new-permission option:selected").text();
        var PermissionID = $("#new-permission").val();

        var html = $('<li data-id="' + PermissionID + '" class="list-group-item"><span>' + PermissionTitle + '</span><i class="fa sl  fa-remove left block mt-3 clickable" ></i></li>');
        $(html).find(".sl").click(function () {
            $(this).parent().remove();
        });

        var pers = lst.find(".list-group-item");
        var canInsert = true;
        $.each(pers, function (index, el) {
            if ($(el).attr('data-id') == PermissionID + "")
                canInsert = false;
        });

        if(canInsert)
        lst.append(html);
    });
}
function onUserRemoveResult() {
    $(document).on("user-remove-success", function (event, result) {
        DoSearch();
    });
}

function onUserDeleteResult() {
    $(document).on("user-delete-success", function (event, result) {
        DoSearch();
    });
}
function onUserGetResult() {
    $(document).on("user-get-success", function (event, result) {

        if (result.result.code == 0) {

            $("#modal-loader").html("ویرایش کاربر - " + result.user.fullname);
            $("#modal-txtfullname").val(result.user.fullname);
            $("#modal-txtregisterdate").val(result.user.regrDate);
            $("#modal-txtmobile").val(result.user.mobile);
            $("#modal-img").attr("src", result.user.image);
        }
        else {
            $("#modal-loader").html("ویرایش کاربر");
            core.message($("#modal-msg"), result.result.message, "danger", false);
        }

    });
}
function onUserNewResult() {

    $(document).on("user-permission-success", function (event, result) {
        $("#new-modal-loader").html("کاربر جدید");

        if (result.result.code != 0) {
            core.message($("#new-modal-msg"), result.result.message, "danger", false);
            return;
        }

        var lst = $("#new-permission");
        lst.html('');
        $.each(result.per, function (index, el) {
            lst.append("<option value='" + el.ID + "' >" + el.roleTitle + "</option>");
        });
        if (lst.html() == '') {
            lst.append("<option value='-1' >هیچ دسترسی وجود ندارد</option>");
        }

    });

    $(document).on("user-new-success", function (event, result) {
        $("#new-modal-loader").html("کاربر جدید");

        if (result.code != 0) {
            core.message($("#new-modal-msg"), result.message, "danger", false);
            return;
        }
        core.message($("#new-modal-msg"), result.message, "success", false);
        DoSearch();
        $('#newModal').modal('hide');

    });

}
function onUserEditResult() {
    $(document).on("user-edit-success", function (event, result) {
        $("#modal-loader").html("ویرایش کاربر");

        if (result.code != 0) {
            core.message($("#modal-msg"), result.message, "danger", false);
            return;
        }
        core.message($("#modal-msg"), result.message, "success", false);
        DoSearch();
        $('#editModal').modal('hide');

    });
}
function onUserListResult() {
    $(document).on("user-list-success", function (event, result) {
        $("#loader").html(" جستجو");

        if (result.result.code == 0) {
            core.message($("#msg"), result.result.message, "success", false);

            var lst = $("#lst");
            lst.html('');

            $.each(result.user, function (index, el) {

                
                var html = $('<a  class="list-group-item ">'
                + '<div class="row " data-id="' + el.ID + '">'

                + '<div class="col-sm-3">'
                + '<div class="form-group">'
                + '<div class="visible-xs">'
                + '<label class="block-inline pl-5">عکس کاربر</label>'
                + '</div>'
                + '<img src="' + el.image + '" alt="عکس کاربر" width="100px" height="100px"/>'
                + '</div>'
                + '</div>'

                + '<div class="col-sm-3">'
                + '<div class="form-group">'
                + '<div class="visible-xs ">'
                + '<label class="block-inline pl-5">نام کاربری</label>'
                + '</div>'
                + '<span>' + el.fullname + '</span>'
                + '</div>'
                + '</div>'

                + '<div class="col-sm-3">'
                + '<div class="form-group">'
                + '<div class="visible-xs ">'
                + '<label class="block-inline pl-5">موبایل</label>'
                + '</div>'
                + '<span>' + el.mobile + '</span>'
                + '</div>'
                + '</div>'

                + '<div class="col-sm-3">'
                + '<div class="form-group">'
                + '<div class="visible-xs ">'
                + '<label class="block-inline pl-5">تاریخ ثبت نام</label>'
                + '</div>'
                + '<span>' + el.regrDate + '</span>'
                + '</div>'
                + '</div>'


                + '<div class="tools ">'
                + '<span data-id="' + el.ID + '" class="block ml-10 edit left clickable" data-toggle="tooltip" data-placement="bottom" title="جزییات"><i class="fa fa-edit"></i></span>'
                + '</div>'
                + '</div></a>');


                lst.append(html);
                initBlock(html);
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
        if (confirm("آیا از حذف این کاربر اطمینان دارید؟")) {
            userlist.f2(id);
        }
    });
}
function initBlock(element) {
    element.find('.remove').click(function () {
        var id = $(this).attr('data-id');
        var block = $(this).attr("data-block");
        var msg = "";
        if (block == "1") {
            msg = "آیا از فعالسازی کاربر اطمینان دارید؟";
        }
        else {
            msg = "آیا از غیرفعالسازی کاربر اطمینان دارید؟";
        }
        if (confirm(msg)) {
            userlist.f3(id);
        }
    });
}
function initEdit(element) {
    element.find('.edit').click(function () {
        var id = $(this).attr('data-id');
        $('#editModal').attr('data-id', id);

        core.loader($("#modal-loader"));

        userlist.f4(id);

        $("#modal-txtusername").val('');
        $("#modal-txtexpiredate").val('');
        $("#modal-txtmobile").val('');
        $("#modal-txtemail").val('');
        $("#modal-txtname").val('');
        $("#modal-txtfamily").val('');
        $("#modal-nationalID").val('');

        $('#editModal').modal('show');
    });
}
function initNew() {
    $("#new-module").on('change', function () {
        core.loader($("#new-modal-loader"));
        userlist.f7($("#new-module").val());
    });

    $('#btnNew').click(function () {
        var id = $(this).attr('data-id');
        $('#newModal').attr('data-id', id);

        $("#new-modal-txtusername").val('');
        $("#new-modal-txtexpiredate").val('');
        $("#new-modal-txtmobile").val('');
        $("#new-modal-txtemail").val('');
        $("#new-modal-txtname").val('');
        $("#new-modal-txtfamily").val('');
        $("#new-modal-nationalID").val('');
        $("#new-modal-txtpassword").val('');
        $("#new-modal-txtconfirmpassword").val('');
        $("#sl-list").html('');
        $("#new-module").change();

        $('#newModal').modal('show');
    });
    $("#new-module").change();
}

function saveNew() {
    $("#new-btnSave").click(function () {

        var username = $("#new-modal-txtusername").val();
        var expireDate = $("#new-modal-txtexpiredate").val();
        var mobile = $("#new-modal-txtmobile").val();
        var email = $("#new-modal-txtemail").val();
        var name = $("#new-modal-txtname").val();
        var family = $("#new-modal-txtfamily").val();
        var nationalID = $("#new-modal-nationalID").val();
        var password = $("#new-modal-txtpassword").val();
        var confirm = $("#new-modal-txtconfirmpassword").val();

        var lst = $("#sl-list");
        var permissionID = new Array();
        var pers = lst.find(".list-group-item");
       
        $.each(pers, function (index, el) {
            permissionID.push(parseInt( $(el).attr('data-id')));
        });

        if (password != confirm) {
            core.message($("#new-modal-msg"), "کلمه عبور با تکرار کلمه عبور یکسان نیست", "danger", true);
            return;
        }
        if (!core.isEmail(email)) {
            core.message($("#new-modal-msg"), "ایمیل وارد شده صحیح نمی باشد", "danger", true);
            return;
        }
        core.loader($("#new-modal-loader"));
        userlist.f6(username, password, permissionID,nationalID, email, mobile, name, family, expireDate);

    });
}

function saveEdit() {
    $("#btnSave").click(function () {

        var userID = $("#editModal").attr('data-id');

        var username = $("#modal-txtusername").val();
        var expireDate = $("#modal-txtexpiredate").val();
        var mobile = $("#modal-txtmobile").val();
        var email = $("#modal-txtemail").val();
        var name = $("#modal-txtname").val();
        var family = $("#modal-txtfamily").val();
        var nationalID = $("#modal-nationalID").val();

        if (!core.isEmail(email)) {
            core.message($("#modal-msg"), "ایمیل وارد شده صحیح نمی باشد", "danger", true);
            return;
        }
        core.loader($("#modal-loader"));
        userlist.f5(userID, username, nationalID, email, mobile, name, family, expireDate);

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