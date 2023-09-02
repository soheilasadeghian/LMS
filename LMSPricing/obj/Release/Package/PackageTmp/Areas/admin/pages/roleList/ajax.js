
var rolelist = {

    urls: [
               "/admin/Panel/roleList",
               "/admin/Panel/roledelete",
               "",
               "/admin/Panel/getRole",
               "/admin/Panel/editRole",
               "/admin/Panel/newRole"
    ],

    f1: function (filter, orderby, ordertype, fromdate, todate, pageIndex, pageCount) {
        var token = $('[name=__RequestVerificationToken]').val();
        var headers = {};
        headers["__RequestVerificationToken"] = token;

        $.ajax({
            type: "POST",
            url: rolelist.urls[0],
            cache: false,
            headers: headers,
            data: JSON.stringify({ filter: filter, orderby: orderby, ordertype: ordertype, fromdate: fromdate, todate: todate, pageIndex: pageIndex, pageCount: pageCount }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
            success: function (response) {
              
                var json = response;
                $(document).trigger("role-list-success", [json]);
            },
            failure: function (response) {
               
            }
        });

    },
    f2: function (roleID) {
        var token = $('[name=__RequestVerificationToken]').val();
        var headers = {};
        headers["__RequestVerificationToken"] = token;

        $.ajax({
            type: "POST",
            url: rolelist.urls[1],
            cache: false,
            headers: headers,
            data: JSON.stringify({ roleID: roleID }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
            success: function (response) {
              
                var json = response;
                $(document).trigger("role-delete-success", [json]);
            },
            failure: function (response) {
               
            }
        });
    },
    f3: function (roleID) {
        var token = $('[name=__RequestVerificationToken]').val();
        var headers = {};
        headers["__RequestVerificationToken"] = token;

        $.ajax({
            type: "POST",
            url: rolelist.urls[2],
            cache: false,
            headers: headers,
            data: JSON.stringify({ roleID: roleID }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
            success: function (response) {

                var json = response;
                $(document).trigger("role-remove-success", [json]);
            },
            failure: function (response) {

            }
        });
    },
    f4: function (roleID) {
        var token = $('[name=__RequestVerificationToken]').val();
        var headers = {};
        headers["__RequestVerificationToken"] = token;

        $.ajax({
            type: "POST",
            url: rolelist.urls[3],
            cache: false,
            headers: headers,
            data: JSON.stringify({ roleID: roleID }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
            success: function (response) {

                var json = response;
                $(document).trigger("role-get-success", [json]);
            },
            failure: function (response) {

            }
        });
    },
    f5: function (roleID, title, description) {
        var token = $('[name=__RequestVerificationToken]').val();
        var headers = {};
        headers["__RequestVerificationToken"] = token;

        $.ajax({
            type: "POST",
            url: rolelist.urls[4],
            cache: false,
            headers: headers,
            data: JSON.stringify({
                roleID: roleID, title: title, description: description }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
            success: function (response) {

                var json = response;
                $(document).trigger("role-edit-success", [json]);
            },
            failure: function (response) {

            }
        });
    },
    f6: function (title, description) {
        var token = $('[name=__RequestVerificationToken]').val();
        var headers = {};
        headers["__RequestVerificationToken"] = token;

        $.ajax({
            type: "POST",
            url: rolelist.urls[5],
            cache: false,
            headers: headers,
            data: JSON.stringify({ title: title, description: description}),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
            success: function (response) {

                var json = response;
                $(document).trigger("role-new-success", [json]);
            },
            failure: function (response) {

            }
        });
    }
}