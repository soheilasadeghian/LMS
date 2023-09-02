
var permissionlist = {

    urls: [
               "/admin/Panel/permissionList",
               "/admin/Panel/possibleRoleList",
               "/admin/Panel/newPermission",
               "/admin/Panel/permissiondelete",
               "/admin/Panel/permissionget",
               "/admin/Panel/permissionedit"
    ],

    f1: function (moduleID, roleID, edit, del, insert, all, filter, orderby, ordertype, fromdate, todate, pageIndex, pageCount) {
        var token = $('[name=__RequestVerificationToken]').val();
        var headers = {};
        headers["__RequestVerificationToken"] = token;

        $.ajax({
            type: "POST",
            url: permissionlist.urls[0],
            cache: false,
            headers: headers,
            data: JSON.stringify({
                moduleID: moduleID, roleID: roleID, edit: edit, del: del, insert: insert, all: all, filter: filter,
                orderby: orderby, ordertype: ordertype, fromdate: fromdate, todate: todate, pageIndex: pageIndex, pageCount: pageCount
            }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
            success: function (response) {

                var json = response;
                $(document).trigger("permission-list-success", [json]);
            },
            failure: function (response) {

            }
        });

    },
    f2: function (moduleID) {
        var token = $('[name=__RequestVerificationToken]').val();
        var headers = {};
        headers["__RequestVerificationToken"] = token;

        $.ajax({
            type: "POST",
            url: permissionlist.urls[1],
            cache: false,
            headers: headers,
            data: JSON.stringify({ moduleID: moduleID }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
            success: function (response) {

                var json = response;
                $(document).trigger("permission-posrole-success", [json]);
            },
            failure: function (response) {

            }
        });
    },
    f3: function (moduleID, roleID, edit, del, insert) {
        var token = $('[name=__RequestVerificationToken]').val();
        var headers = {};
        headers["__RequestVerificationToken"] = token;

        $.ajax({
            type: "POST",
            url: permissionlist.urls[2],
            cache: false,
            headers: headers,
            data: JSON.stringify({ moduleID: moduleID, roleID: roleID, edit: edit, del: del, insert: insert }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
            success: function (response) {

                var json = response;
                $(document).trigger("permission-new-success", [json]);
            },
            failure: function (response) {

            }
        });
    },
    f4: function (permissionID) {
        var token = $('[name=__RequestVerificationToken]').val();
        var headers = {};
        headers["__RequestVerificationToken"] = token;

        $.ajax({
            type: "POST",
            url: permissionlist.urls[3],
            cache: false,
            headers: headers,
            data: JSON.stringify({ permissionID: permissionID }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
            success: function (response) {

                var json = response;
                $(document).trigger("permission-delete-success", [json]);
            },
            failure: function (response) {

            }
        });
    },
    f5: function (permissionID) {
        var token = $('[name=__RequestVerificationToken]').val();
        var headers = {};
        headers["__RequestVerificationToken"] = token;

        $.ajax({
            type: "POST",
            url: permissionlist.urls[4],
            cache: false,
            headers: headers,
            data: JSON.stringify({ permissionID: permissionID }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
            success: function (response) {

                var json = response;
                $(document).trigger("permission-get-success", [json]);
            },
            failure: function (response) {

            }
        });
    },
    f6: function (permissionID, moduleID, roleID, edit, del, insert) {
        var token = $('[name=__RequestVerificationToken]').val();
        var headers = {};
        headers["__RequestVerificationToken"] = token;

        $.ajax({
            type: "POST",
            url: permissionlist.urls[5],
            cache: false,
            headers: headers,
            data: JSON.stringify({ permissionID: permissionID, moduleID: moduleID, roleID: roleID, edit: edit, del: del, insert: insert }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
            success: function (response) {

                var json = response;
                $(document).trigger("permission-edit-success", [json]);
            },
            failure: function (response) {

            }
        });
    }
}