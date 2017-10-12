//---------------------------------------------------------------------------
// version      : 1.0
// Programmer : Dhanya.Ratheesh
// Created Date :2015/11/24
//---------------------------------------------------------------------------
//jQuery.validator.defaults.ignore = "input[id=txtShopId]:disabled";
//$(function () {

//    // When shop radio is selected.
//    $("input:radio[id=radShop]").on("change", function () {
//        if ($(this).is(':checked')) {
//            $("#txtShopId").removeAttr('disabled');
//            $("#txtShopPassword").removeAttr('disabled');
//            $("#txtShopId").removeAttr('class');
//            $("#txtShopPassword").removeAttr('class');
//        }
//    });

//    // When HQ radio is selected.
//    $("input:radio[id=radHQ]").on("change", function () {
//        if ($(this).is(':checked')) {
//            ResetFieldByRadioMode();
//        }
//    });

//    // Check HQ radio is selected.
//    if ($("input:radio[id=radHQ]").is(':checked')) {
//        ResetFieldByRadioMode();
//    }

//    // Show messagebox for login unsucessful.
//    if ($('#mustCheckGetLoginFailed').val().length > 0) {
//        ShowMsgBox("E", $('#mustCheckGetLoginFailed').val());
//    }
//});


//function ResetFieldByRadioMode() {
//    $("#txtShopId").val("");
//    $("#txtShopPassword").val("");
//    $("#txtShopId").removeClass("input-validation-error");
//    $("#txtShopPassword").removeClass("input-validation-error");
//    $("#txtShopId").attr('disabled', 'disabled');
//    $("#txtShopId").attr('class', 'disabled');
//    $("#txtShopPassword").attr('disabled', 'disabled');
//    $("#txtShopPassword").attr('class', 'disabled');
//}
$(document).ready(function () {
    $('form-submit').submit();
});
