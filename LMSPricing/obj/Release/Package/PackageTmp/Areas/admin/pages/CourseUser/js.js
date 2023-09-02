var pageCount = 10;
var pageIndex = 1;

$(function () {

    initRemoveDate();
    onUserDeleteResult();
    onUserListResult();
    onUserEditResult();
    onUserGetResult();
    setup_search();
    iniSelectingPermission();
    DoSearch();

    $("#btn-close").click(function () {
       
        DoSearch();
        $('#editModal').modal('hide');
    });
});

function setup_search() {

    $("#slcCourse").on('change', function () {
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

}

function DoSearch() {

    var filter = $("#txtFilter").val();
    var course = $("#slcCourse").val();
    var orderby = $("#slcOrderby").val();
    var ordertype = $("#slcOrderType").val();

    core.loader($("#loader"));
    courseList.f1(filter, course, orderby, ordertype,pageIndex, pageCount);

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

            $("#modal-loader").html(result.CourseUser.fullname);
            $("#modal-txtfullname").val(result.CourseUser.fullname);
            $("#modal-txtregisterdate").val(result.CourseUser.regDate);
            $("#modal-txtmobile").val(result.CourseUser.mobile);
            $("#modal-img").attr("src", result.CourseUser.userimage);
            $("#modal-txtcoursename").val(result.CourseUser.coursename);
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

            $.each(result.CourseUser, function (index, el) {

                
                var html = $('<a  class="list-group-item  ' + el.isread + '">'
                + '<div class="row " data-id="' + el.ID + '">'

                + '<div class="col-sm-4">'
                + '<div class="form-group">'
                + '<div class="visible-xs ">'
                + '<label class="block-inline pl-5">نام دوره</label>'
                + '</div>'
                + '<span>' + el.coursename + '</span>'
                + '</div>'
                + '</div>'
                
                + '<div class="col-sm-4">'
                + '<div class="form-group">'
                + '<div class="visible-xs ">'
                + '<label class="block-inline pl-5">نام کاربر</label>'
                + '</div>'
                + '<span>' + el.fullname + '</span>'
                + '</div>'
                + '</div>'

                + '<div class="col-sm-3">'
                + '<div class="form-group">'
                + '<div class="visible-xs ">'
                + '<label class="block-inline pl-5">تاریخ رزرو</label>'
                + '</div>'
                + '<span>' + el.regDate + '</span>'
                + '</div>'
                + '</div>'

                + '<div class="col-sm-1">'
                + '<div class="tools ">'
                + '<span data-id="' + el.ID + '" class="block ml-10 edit left clickable" data-toggle="tooltip" data-placement="bottom" title="جزییات"><i class="fa fa-edit"></i></span>'
                + '</div>'
                + '</div>'

                + '</div></a>');

                lst.append(html);
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
function initEdit(element) {
    element.find('.edit').click(function () {
        var id = $(this).attr('data-id');
        $('#editModal').attr('data-id', id);

        core.loader($("#modal-loader"));

        courseList.f2(id);

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