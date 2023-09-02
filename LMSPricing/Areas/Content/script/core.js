var core = {

    loader: function (container) {
        $(container).html('درحال پردازش اطلاعات<img src="/Areas/Content/images/ajax-loader.gif" />');
    },
    message: function (container, text, type, append) {
        var type_tag = "";
        if (type == "danger") {
            type_tag = "خطا";
        }
        else if (type == "success") {
            type_tag = "موفقیت";
        }
        if (append)
            $(container).append(
                    '<div class="alert alert-' + type + ' fade in">'
                    + '<a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>'
                    + '<strong>' + (type_tag) + '!</strong> : <span>' + text + '</span>'
                    + '</div>'
                );
        else
            $(container).html(
                    '<div class="alert alert-' + type + ' fade in">'
                    + '<a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>'
                    + '<strong>' + (type_tag) + '!</strong> : <span>' + text + '</span>'
                    + '</div>'
                );

    },
    isEmail:function(email) {
        var re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        return re.test(email);
    }

};