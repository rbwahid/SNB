
// basic function for ajax call get data //

function ajaxCall(url, paramData, callback, method, obj) {
    method = method == null ? "POST" : method;
    $.ajax({
        type: method,
        url: url,
        data: JSON.stringify(paramData),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            //callback(data);
            if (callback == 'renderRemoveItem') {
                renderRemoveItem(response);
            }
            else if (callback == 'renderResetPassword') {
                renderResetPassword(response);
            }  
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(textStatus + "! please try again", '<i class="fa fa-exclamation-circle" aria-hidden="true"> Alert</i>');
        }
    });
}

// for Modal...
function ajaxCallModal(url, paramData, callback, method, obj) {
    method = method == null ? "POST" : method;
    $.ajax({
        type: method,
        url: url,
        data: JSON.stringify(paramData),
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (response) {
            //callback(data);
            if (callback == 'renderUserDetailsEntryLoad') {
                renderUserDetailsEntryLoad(response);
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(textStatus + "! please try again", '<i class="fa fa-exclamation-circle" aria-hidden="true"> Alert</i>');
        }
    });
}

// render method //

function renderRemoveItem(data) {
    swalInit({
        title: 'Deleted!',
        text: 'Your data has been deleted.',
        type: 'success'
    }).then(function (result) {
        //if (result.value) {
            location.reload();
        //}
    });
}

function renderResetPassword(data) {
    swalInit({
        title: 'Password Changed!',
        text: 'Your password has been changed.',
        type: 'success'
    }).then(function (result) {
        //if (result.value) {
        location.reload();
        //}
    });
}
