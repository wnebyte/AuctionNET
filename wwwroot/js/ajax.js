function getAsyncSession(callback) {
    $.getJSON('/Ajax/GetSession', function (session) {
        callback({
            'status': 'success', 
            'statusCode': 200, 
            'data': session, 
        }); 
    }).fail(function (xhr) {
        callback({
            'status': 'error',
            'statusCode': xhr.status, 
            'data': null
        });
    });
}

function postAsyncLogin(form, callback) {
    var result = { status: null, statusCode: null };

    $.post({
        url: '/Ajax/Login',
        contentType: 'application/json; charset=utf8',
        data: JSON.stringify({
            'emailaddress': $('#emailaddress', form).val(),
            'password': $('#password', form).val()
        }),
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        error: function (xhr) {
            result.status = 'error';
            result.statusCode = xhr.status;
        },
        success: function () {
            result.status = 'success';
            result.statusCode = 200;
        }
    }).always(function () {
        callback(result);
    });
}

function postAsyncLogout(callback) {
    var result = { status: null, statusCode: null };

    $.post({
        url: '/Ajax/Logout',
        contentType: false,
        data: false,
        processData: false,
        dataType: false,
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        error: function (xhr) {
            result.status = 'error';
            result.statusCode = xhr.status;
        },
        success: function () {
            result.status = 'success';
            result.statusCode = 200;
        }
    }).always(function () {
        callback(result); 
    });
}

function postAsyncSession(state, callback) {
    var result = { status: null, statusCode: null };

    $.post({
        url: "/Ajax/PostSession",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(state),
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        dataType: false,
        error: function (xhr) {
            result.status = 'error';
            result.statusCode = xhr.status;
        },
        success: function () {
            result.status = 'success';
            result.statusCode = 200;
        }
    }).always(function () {
        callback(result);
    });
}

function postAsyncBid(bid, callback) {
    var result = { status: null, statusCode: null, data: null };

    $.post({
        url: "/Ajax/Bid",
        contentType: "application/json; charset=utf8",
        data: JSON.stringify(bid),
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        dataType: "json",
        error: function (xhr) {
            result.status = 'http-error';
            result.statusCode = xhr.status;
        },
        success: function (resp) {
            result.status = resp.Status;
            result.statusCode = resp.StatusCode;
            result.data = resp.Data;
        }
    }).always(function () {
        callback(result);
    });
}

function postAsyncBuyout(buyout, callback) {
    var result = { status: null, statusCode: null }; 

    $.post({
        url: "/Ajax/Buyout",
        contentType: "application/json; charset=utf8",
        data: JSON.stringify(buyout),
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        dataType: "json",
        error: function (xhr) {
            result.status = 'http-error';
            result.statusCode = xhr.status;
        },
        success: function (resp) {
            result.status = resp.Status;
            result.statusCode = resp.StatusCode;
        }
    }).always(function () {
        callback(result);
    });
}

function postAsyncDelete(id, callback) {
    var result = { status: null, statusCode: null };

    $.post({
        url: '/Ajax/Delete', 
        contentType: "application/json; charset=utf8",
        data: json.stringify(id), 
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        dataType: 'json', 
        error: function (xhr) {
            result.status = 'http-error';
            result.statusCode = xhr.status;
        }, 
        success: function (resp) {
            result.status = resp.Status;
            result.statusCode = resp.StatusCode;
        }
    })
        .always(function () {
            callback(result);
        });
}
