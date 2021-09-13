// global variables
let state = {
    "toggled": [],
    "selected": ""
};

// delay = 225
var DELAY = 500, clicks = 0, timer_list = null, timer_table = null;
//wait = 750
const WAIT = 10000;

const tr_active  = 'table-active', tr_success = 'bg-success', tr_warning = 'bg-warning', tr_danger  = 'bg-danger'; 
const td_warning = 'bg-warning', border_danger = 'border-danger'; 

// on ready function
$(function () {

    /**
     * <summary> click handler on sessionBtn : event ajax post to server
     **/
    $('#sessionBtn').click(function () {
        postAsyncSession(state, function (result) {
            if (result.status != 'success') {
                alert('postAsyncSession failed with statusCode ' + result.statusCode); 
            }
        });
    });

    /**
     * init #content-list
     * <summary> click handler on list-group-item-action : event (click -> toggle a background) | (dblclick -> toggle nested list-group)
     **/
    $('.list-group-item-action').on('click', function () {
        clicks++;
        var node = $(this).blur();

        if (clicks == 1) {
            if (node.parent().hasClass('list-group-nested')) {
                click_list(node);
                $('#sessionBtn').click();
                clicks = 0;
                return;
            }
            timer_list = setTimeout(function () {
                click_list(node);
                $('#sessionBtn').click();
                clicks = 0;
            }, DELAY);

        } else {
            clearTimeout(timer_list);
            dblclick_list(node);
            $('#sessionBtn').click();
            clicks = 0;
        }
    })
        .on('dbclick', function (e) {
            e.preventDefault();
        });

    if (session != null) {

        if (session.Selected != null && session.Selected != '') {
            click_list($('#' + session.Selected.replace(".", "\\.")));
        }

        if (session.Toggled != null) {
            const data = session.Toggled.split(";");

            for (i = 0; i < data.length; i++) {
                if (data[i] != null && data[i] != '')
                    dblclick_list($('#' + data[i]));
            }
        }
    }

    /**
     * init #content-table
     * <summary> click handler on the table > tr : (click -> toggle tr background) | (dblclick -> build and show modal)
     **/
    $('#content-table > table > tbody > tr').on('click', function () {
        clicks++;
        var node = $(this); 

        if (clicks == 1) {
            timer_table = setTimeout(function () {
                click_table(node);
                clicks = 0;
            }, DELAY);
        }
        else {
            clearTimeout(timer_table);
            dblclick_table(node);
            clicks = 0;
        }
    })
        .on('dbclick', function (e) {
            e.preventDefault();
        });

    /**
     * <summary> click handler on the bid button : event (validate -> ajax post to server) -> display the results. 
     **/
    $('input[value="Bid"]').click(function () {
        var node = $('.' + tr_active);
        if (node.length != 1) return false;
        let bid = { 'id': node.attr('id'), 'val': $('input[type="number"]').val() };

        if (validates(bid)) {
            postAsyncBid(bid, function (result) {
                if (result.status == 'success') {
                    node.toggleClass(tr_active).toggleClass(tr_success);
                    $('.min', node).html(null)
                        .append('<span class="fa fa-thumbs-up" style="text-align: center; display: block; margin: auto;"></span');
                    setTimeout(function () {
                        $('.min', node).html(maxBid(result.data));
                        node.toggleClass(tr_success);
                    }, WAIT);
                }
                else {
                    errorHandler(result.statusCode, node, $('.min', node), result.data != undefined ? result.data : null);
                }
                
            });
        }
    });

    /**
     * <summary> click handler on the buy button : event ajax post to server -> display the results. 
     **/
    $('input[value="Buy"]').click(function () {
        var node = $('.' + tr_active);
        if (node.length != 1) return false;

        if ($('.max', node).html() != '') {
            postAsyncBuyout({ 'id': node.attr('id') }, function (result) {
                if (result.status == 'success') {
                    node.toggleClass(tr_active).toggleClass(tr_success);
                    $('.max', node).html(null)
                        .append('<span class="fa fa-thumbs-up" style="text-align: center; display: block; margin: auto;"></span');
                    setTimeout(function () {
                        node.remove();
                    }, WAIT);
                }
                else {
                    errorHandler(result.statusCode, node, $('.max', node));
                }
            });
        }
    });

    $('input[type="button"], button').click(function () {
        $(this).blur();
    });

    // init #range_slider
    var instance; 
    var disabled = true;
    var values = [0, 50, 100, 200, 400, 800, 1600, 3200, 6400, 12800, 25600, 51200, '&#8734;'];
    var from = values.indexOf(50);
    var to = values.indexOf(400);
    var range = $('#range');

    /**
     * <summary> instantiates the range-slider, with the values declared above. 
     **/
    $('#range-slider').ionRangeSlider({
        type: 'double', 
        skin: 'big', 
        grid: true, 
        from: from, 
        to: to, 
        values: values, 
        disable: disabled, 
        prettify_enabled: true, 
        prettify_separator: ',', 
        postfix: ' SEK', 
        force_edges: true, 
        onFinish: function (data) {
            range.val(values[data.from] + "-" + values[data.to]);

        }, 
        onUpdate: function (data) {
            if (disabled)
                range.val(null);
            else
                range.val(values[data.from] + "-" + values[data.to]);
        }
    });

    /**
     * <summary> click handler on the range slider toggle button : event toggles the range-slider and its radio inputs. 
     **/
    $('.slider').click(function () {
        instance = $('#range-slider').data('ionRangeSlider'); 
        disabled = !disabled;
        instance.update({
            disable: disabled
        });
        $('#range-selector input[type="radio"]').attr('disabled', disabled)
            .siblings('label').toggleClass('opacity');
    });

    /**
     * <summary> removes the optional parameters from the url before submit. 
     **/
    $('#auctions-form').submit(function () {
        $('#search, #category, #range').each(function () { if (!$(this).val()) $(this).remove(); });
        if (range != undefined && !disabled) {
            range.val($('#range-selector input[type="radio"]:checked').siblings('label').html().replace(/\s/g, '').toLowerCase()
                .concat('-').concat(range.val()));
        } 
        return true;
    });

});

// #content-list click event
function click_list(node) {
    $('.list-group-item-action').not(node).removeClass('active');
    node.toggleClass('active');
    if (node.hasClass('active'))
        state.selected = $('#category').val(node.attr('id')).val();
    else
        state.selected = $('#category').val(null).val();
}

// #content-list dblclick event
function dblclick_list(node) {
    if (node.hasClass('collapser')) {
        node.siblings('div > .list-group').slideToggle();
        $('.svg-inline--fa', node)
            .toggleClass('fa-chevron-right')
            .toggleClass('fa-chevron-down');
        if (!state.toggled.includes(node.attr('id')))
            state.toggled.push(node.attr('id'))
        else
            state.toggled.splice(state.toggled.indexOf(node.attr('id')), 1); 
    }
}

// #content-table click event
function click_table(node) {
    $('#content-table > table > tbody > tr').not(node).removeClass(tr_active);
    node.toggleClass(tr_active);
}

// #content-table dblclick event
function dblclick_table(node) {
    var modal = $('#auctions-modal');
    var indicators = $('.carousel-indicators', modal);
    var inner = $('.carousel-inner', modal);
    var obj = model.find(auc => auc.Id == node.attr('id'));

    indicators.children('li').remove();
    inner.children('.carousel-item').remove();

    if (obj.Images.length == 0) {
        indicators.append('<li data-target="#carousel" data-slide-to="0" class="active"></li>');
        inner.append('<div class="carousel-item active">' + generateImage(true) + '</div>');
    }
    else {
        let i = 0;
        Array.from(obj.Images).forEach((img) => {
            indicators.append('<li data-target="#carousel" data-slide-to="' + i++ + '"></li>');
            inner.append('<div class="carousel-item">' + generateImage(false, img) + '</div>');
        });
        indicators.children('li:first-of-type').addClass('active');
        inner.children('.carousel-item:first-of-type').addClass('active');
    }

    $('#name', modal).html(obj.Name);
    $('#description', modal).html(obj.Description);
    modal.show();
}

function generateImage(def, image) {
    if (def) {
        return '<img class="d-block w-100" src="../images/no-image.jpg" width="250" height="200"/>';
    }
    else {
        return '<img class="d-block w-100" src="data:' + image.ContentType + ';base64,' + image.Bytes + '" width="250" height="200"/>'
    }
}

// error handler for #content-table's buy/buyout buttons
function errorHandler(statusCode, parent, child, data) {

    if (statusCode == 400) {
        parent.toggleClass(tr_active).toggleClass(tr_danger);
        child.html(null)
            .append('<span class="fa fa-thumbs-down" style="text-align: center; display: block; margin: auto;"></span');
        setTimeout(function () {
            child.html(maxBid(data));
            parent.toggleClass(tr_danger);
        }, WAIT);
    }

    if (statusCode == 404) {
        parent.toggleClass(tr_active).toggleClass(tr_danger);
        child.html(null)
            .append('<span class="fa fa-thumbs-down" style="text-align: center; display: block; margin: auto;"></span');
        setTimeout(function () {
            parent.remove();
        }, WAIT);
    }

    if (statusCode == 408) {
        $('#modalBtn').click();
        var modal = $('.modal-content').toggleClass(border_danger);
        setTimeout(function () {
            modal.toggleClass(border_danger);
        }, WAIT);
    }

    if (statusCode == 412) {
        alert('your session has expired');
        document.location.reload();
    }
}

// #content-table btn click event validation
function validates(bid) {
    var init, min, max = null; 
    const del = ' '; 
    var val = true; 
    var init_warn, min_warn, max_warn = false; 
    const warn_class = 'border-warning'; 

    try {
        var theader = $('#content-table > table > thead > tr');
        var node = $('#' + bid.id);

        if (bid.val != null) {
            bid = parseFloat(bid.val);
        }
        else
            return false;

        if (node.children('.init').html().split(del, 1)[0] != null) {
            init = parseFloat(node.children('.init').html().split(del, 1)[0]);
        }

        if (node.children('.min').html().split(del, 1)[0] != null) {
            min = parseFloat(node.children('.min').html().split(del, 1)[0]);
        }

        if (node.children('.max').html().split(del, 1)[0] != null) {
            max = parseFloat(node.children('.max').html().split(del, 1)[0]);
        }

        if (init != null) {
            if (bid < init) {
                init_warn = true;
                val = false;
            }
        }

        if (min != null) {
            if (bid <= min) {
                min_warn = true;
                val = false;
            }
        }

        if (max != null) {
            if (max <= bid) {
                max_warn = true;
                val = false;
            }

        }

        if (init_warn || min_warn || max_warn) {
            if (init_warn) theader.children('.init').toggleClass(warn_class);
            if (min_warn) theader.children('.min').toggleClass(warn_class);
            if (max_warn) theader.children('.max').toggleClass(warn_class);

            setTimeout(function () {
                if (init_warn) theader.children('.init').toggleClass(warn_class);
                if (min_warn) theader.children('.min').toggleClass(warn_class);
                if (max_warn) theader.children('.max').toggleClass(warn_class);
            }, WAIT);
        }
    }

    catch (Exception) { return false; }

    return val;
}

function maxBid(data) {
    return Math.max.apply(Math, data.Bids.map(function (o) { return o.Amount; })) + ' ' + data.Currency;
}