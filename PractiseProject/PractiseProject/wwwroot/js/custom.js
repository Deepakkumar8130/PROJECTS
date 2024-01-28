$(document).ready(function () {

    var sameAddressCheckbox = $('#sameAddressCheckbox');
    var currentAddressTextarea = $('#txtCurrentAddress');
    var permanentAddressTextarea = $('#txtPermanentAddress');

    sameAddressCheckbox.click(function () {

        if (sameAddressCheckbox.prop('checked')) {

            permanentAddressTextarea.val(currentAddressTextarea.val());
        } else {

            permanentAddressTextarea.val('');
        }
    });

})