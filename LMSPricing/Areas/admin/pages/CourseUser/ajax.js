
var courseList = {

    urls: [
               "/admin/Panel/courseList",
               "/admin/Panel/getCourse"
    ],

    f1: function (filter, course, orderby, ordertype, pageIndex, pageCount) {
        var token = $('[name=__RequestVerificationToken]').val();
        var headers = {};
        headers["__RequestVerificationToken"] = token;

        $.ajax({
            type: "POST",
            url: courseList.urls[0],
            cache: false,
            headers: headers,
            data: JSON.stringify({ filter: filter, course: course, orderby: orderby, ordertype: ordertype, pageIndex: pageIndex, pageCount: pageCount }),
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

    f2: function (courseID) {
        var token = $('[name=__RequestVerificationToken]').val();
        var headers = {};
        headers["__RequestVerificationToken"] = token;

        $.ajax({
            type: "POST",
            url: courseList.urls[1],
            cache: false,
            headers: headers,
            data: JSON.stringify({ courseID: courseID }),
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
    }


}