// delay = 225
var CLICK_DELAY = 500, clicks = 0, timer_dropdown = null;
const WAIT = 750; 

const tr_active = 'table-active', tr_success = 'bg-success'; 

$(function () {

    $(document).on('click', '#category-dropdown-menu', function (e) {
        e.stopPropagation();
    });

    var category = $('#category'), category_main = $('#categoryMain'), category_sub = $('#categorySub');

    $('#category-dropdown-menu .list-group-item').on('click', function () {
        clicks++;
        var node = $(this).blur();

        if (clicks == 1) {
            timer_dropdown = setTimeout(function () {
                // code here
                if (!node.hasClass('collapser')) {
                    category.val(node.attr('id'));
                    category_main.val(node.attr('id').split(' | ')[0]);
                    category_sub.val(node.attr('id').split(' | ')[1]);
                    $('#categoryBtn').dropdown('toggle');
                }
                clicks = 0;
            }, CLICK_DELAY);
        }
        else {
            clearTimeout(timer_dropdown);
            if (node.hasClass('collapser')) {
                node.siblings('div > .list-group').slideToggle();
                $('.svg-inline--fa', node)
                    .toggleClass('fa-chevron-right')
                    .toggleClass('fa-chevron-down');
            }
            clicks = 0;
        }
    });

    $('#currency-dropdown-menu .dropdown-item').on('click', function () {
        $('#currency').val($(this).html());
    });

    if (category_main.val() != '' && category_sub.val() != '')
        category.val(category_main.val().concat(' | ').concat(category_sub.val()));

    $('#content-table > table > tbody > tr').on('click', function () {
        clicks++;
        var node = $(this);

        if (clicks == 1) {
            timer_table = setTimeout(function () {
                click_table(node);
                clicks = 0;
            }, CLICK_DELAY);
        }
        else {
            clearTimeout(timer_table);
            clicks = 0;
        }
    })
        .on('dbclick', function (e) {
            e.preventDefault();
        });

    $('input[value="Delete"]').on('click', function () {
        node = $('.' + tr_active);
        if (node.length != 1) return; 

        postAsyncDelete(node.attr('id'), function (result) {
            alert(result.status);
            if (result.status == 'success') {
                node.toggleClass(tr_active).toggleClass(tr_success);
                setTimeout(function () {
                    node.remove();
                }, WAIT);
            }
            else if (result.status == 'error' && result.statusCode == 412) {
                alert('your session has expired.');
                document.location.reload();
            }
        });
    });
}); 

function click_table(node) {
    $('#content-table > table > tbody > tr').not(node).removeClass(tr_active);
    node.toggleClass(tr_active);
}