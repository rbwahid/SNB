
$('.daterange-singledate').daterangepicker({
    singleDatePicker: true,
    autoUpdateInput: false,
    showDropdowns: true,
    minYear: 2000,
    maxYear: parseInt(moment().format('YYYY'), 10),
    locale: {
        format: 'DD-MMM-YYYY'
    }
});

$('.daterange-singledate').on('apply.daterangepicker', function (ev, picker) {
    $(this).val(picker.startDate.format('DD-MMM-YYYY'));
});

$('.daterange-singledate').on('cancel.daterangepicker', function (ev, picker) {
    $(this).val('');
});

$(document).on("click", ".daterange-singledate-icon", function () {
    $(this).parent().find('.daterange-singledate').focus();
});