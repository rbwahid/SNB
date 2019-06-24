

//------Delete Alert-------
var swalInit = swal.mixin({
    buttonsStyling: false,
    confirmButtonClass: 'btn btn-primary',
    cancelButtonClass: 'btn btn-light'
});

function message_show(title='Oops...', text='Something went wrong!', type='error') {
    swalInit({
        title: title,
        text: text,
        type: type,
        padding: 40
    });
}

function delete_confirm(url, paramData) {
    swalInit({
        title: 'Are you sure want to delete?',
        type: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Confirm'
    }).then(function (result) {
        if (result.value) {
            ajaxCall(url, paramData, "renderRemoveItem");

        }
    });

}

function reset_password_confirm(url, paramData) {
    swalInit({
        title: 'Are you sure want to reset password?',
        type: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Confirm'
    }).then(function (result) {
        if (result.value) {
            ajaxCall(url, paramData, "renderResetPassword");

        }
    });

}


//---Form Progress-----
$("form").on("submit", function (event) {
    if ($(this).valid()) {
        $.blockUI({
            message: '<i class="icon-spinner4 spinner"></i>',
            timeout: 20000000, //unblock after 20 seconds
            overlayCSS: {
                backgroundColor: '#1b2024',
                opacity: 0.8,
                zIndex: 1200,
                cursor: 'wait'
            },
            css: {
                border: 0,
                color: '#fff',
                padding: 0,
                zIndex: 1201,
                backgroundColor: 'transparent'
            }
        });
    }
});


//-------for Check Value--------
function customTryParseInt(value, defaultValue) {
    defaultValue = (typeof defaultValue !== "undefined" && defaultValue !== null) ? defaultValue : 0;
    var returnValue = defaultValue;
    if (typeof value !== "undefined" && value !== null) {
        if (value.toString().length > 0) {
            if (!isNaN(value)) {
                returnValue = parseInt(value);
            }
        }
    }
    return returnValue;
}

function customTryParseFloat(value, defaultValue) {
    defaultValue = (typeof defaultValue !== "undefined" && defaultValue !== null) ? defaultValue : 0;
    var returnValue = defaultValue;
    if (typeof value !== "undefined" && value !== null) {
        if (value.toString().length > 0) {
            if (!isNaN(value)) {
                returnValue = parseFloat(value);
            }
        }
    }
    return returnValue;
}

