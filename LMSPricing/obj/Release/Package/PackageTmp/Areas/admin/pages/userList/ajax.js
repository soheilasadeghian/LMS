
var userlist = {

    urls: [
               "/admin/Panel/userlist",
               "/admin/Panel/userdelete",
               "/admin/Panel/userblock",
               "/admin/Panel/getUser",
               "/admin/Panel/editUser",
               "/admin/Panel/newUser",
               "/admin/Panel/perList"
    ],

    f1: function (pageIndex, pageCount) {
        var token = $('[name=__RequestVerificationToken]').val();
        var headers = {};
        headers["__RequestVerificationToken"] = token;

        $.ajax({
            type: "POST",
            url: userlist.urls[0],
            cache: false,
            headers: headers,
            data: JSON.stringify({ pageIndex: pageIndex, pageCount: pageCount }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
            success: function (response) {

                var json = response;
                $(document).trigger("user-list-success", [json]);
            },
            failure: function (response) {

            }
        });

    },
    f2: function (userID) {
        var token = $('[name=__RequestVerificationToken]').val();
        var headers = {};
        headers["__RequestVerificationToken"] = token;

        $.ajax({
            type: "POST",
            url: userlist.urls[1],
            cache: false,
            headers: headers,
            data: JSON.stringify({ userID: userID }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
            success: function (response) {

                var json = response;
                $(document).trigger("user-delete-success", [json]);
            },
            failure: function (response) {

            }
        });
    },
    f3: function (userID) {
        var token = $('[name=__RequestVerificationToken]').val();
        var headers = {};
        headers["__RequestVerificationToken"] = token;

        $.ajax({
            type: "POST",
            url: userlist.urls[2],
            cache: false,
            headers: headers,
            data: JSON.stringify({ userID: userID }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
            success: function (response) {

                var json = response;
                $(document).trigger("user-remove-success", [json]);
            },
            failure: function (response) {

            }
        });
    },
    f4: function (userID) {
        var token = $('[name=__RequestVerificationToken]').val();
        var headers = {};
        headers["__RequestVerificationToken"] = token;

        $.ajax({
            type: "POST",
            url: userlist.urls[3],
            cache: false,
            headers: headers,
            data: JSON.stringify({ userID: userID }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
            success: function (response) {

                var json = response;
                $(document).trigger("user-get-success", [json]);
            },
            failure: function (response) {

            }
        });
    },
    f5: function (userID, username, nationalID, email, mobile, name, family, expireDate) {
        var token = $('[name=__RequestVerificationToken]').val();
        var headers = {};
        headers["__RequestVerificationToken"] = token;

        $.ajax({
            type: "POST",
            url: userlist.urls[4],
            cache: false,
            headers: headers,
            data: JSON.stringify({
                userID: userID, username: username, nationalID: nationalID,
                email: email, mobile: mobile, name: name, family: family, expireDate: expireDate
            }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
            success: function (response) {

                var json = response;
                $(document).trigger("user-edit-success", [json]);
            },
            failure: function (response) {

            }
        });
    },
    f6: function (username, password, permissionID, nationalID, email, mobile, name, family, expireDate) {
        var token = $('[name=__RequestVerificationToken]').val();
        var headers = {};
        headers["__RequestVerificationToken"] = token;
        var s = permissionID.join()+"";
        $.ajax({
            type: "POST",
            url: userlist.urls[5],
            cache: false,
            headers: headers,
            data: JSON.stringify({
                username: username, password: password, permissionID: s, nationalID: nationalID,
                email: email, mobile: mobile, name: name, family: family, expireDate: expireDate
            }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
            success: function (response) {

                var json = response;
                $(document).trigger("user-new-success", [json]);
            },
            failure: function (response) {

            }
        });
    },
    f7: function (moduleID) {
        var token = $('[name=__RequestVerificationToken]').val();
        var headers = {};
        headers["__RequestVerificationToken"] = token;

        $.ajax({
            type: "POST",
            url: userlist.urls[6],
            cache: false,
            headers: headers,
            data: JSON.stringify({ moduleID: moduleID }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
            success: function (response) {

                var json = response;
                $(document).trigger("user-permission-success", [json]);
            },
            failure: function (response) {

            }
        });
    }
}