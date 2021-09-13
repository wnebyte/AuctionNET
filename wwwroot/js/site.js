$(function () {
    var modal = $('.modal');

    $('#modalBtn').click(function () {
        getAsyncSession(function (result) {
            if (result.status == 'success') {
                if (result.data.Username != null) {
                    var node = $('#account-modal');
                    $('p', node).html('Logged in as <strong>' + result.data.Username + '</strong>');
                    node.show();
                }
                else {
                    $('#login-modal').show();
                }
            }
            else {
                if (result.statusCode == 412) {
                    alert('your session has timed out');
                    document.location.reload();
                }
            }
        });
    });

    $(window).click(function (evt) {
        modal.each(function () {
            if (evt.target.id == $(this).attr('id'))
                $(this).hide();
        });
    });

    $('.modal-close').click(function () {
        $('.modal').hide();
    });

    $('.form-login button[type="submit"]').click(function (evt) {
        evt.preventDefault();
        var form = $('.form-login');
        form.validate();

        if (form.valid()) {
            postAsyncLogin(form, function (result) {
                if (result.status == 'success') {
                    $('input:visible, span', form).val(null);
                    $('.modal').hide();
                    $('#modalBtn').click();
                }
                else {
                    switch (result.statusCode) {
                        case 412:
                            alert('your session has (probably) timed out');
                            document.location.reload();
                            break;
                        case 417:
                            alert('user was not found');
                            break;
                        case 423:
                            alert('password does not match');
                            break;
                    }
                }
            });
        }
    });

    $('#logoutBtn').click(function () {
        postAsyncLogout(function (result) {
            if (result.status == 'success') {
                $('#account-modal p').html(null);
                $('.modal').hide();
            }
            else {
                if (result.statusCode == 412) {
                    alert('your session has timed out');
                    document.location.reload();
                }
            }
        });
    });

});