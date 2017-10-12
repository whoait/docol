//---------------------------------------------------------------------------
// System		: DOCOL
// Class Name	: DCW002 java script
// Overview		: 
// Designer		: BinhVT5
// Programmer	: BinhVT5
// Created Date	: 2015/11/26
//---------------------------------------------------------------------------
$(document).ready(function () {

    // Select file 
    $('#btnSelectFile').click(function () {
        $('#fileUpload').click();
    });

    //document.getElementById("fileUpload").onchange = function () {
    //    document.getElementById("form").submit();
    //};

    // Click button 詳細条件で書類を検索する
    $('#btnSearchDetail').click(function () {
        RedirectPost(URL1);
    });

    // Click button 本日発送予定の書類を検索する(普通車)
    $('#btnSearchLightVehicles').click(function () {
        RedirectPost(URL2);
    });

    // Click button 本日発送予定の書類を検索する(軽)
    $('#btnSearchLory').click(function () {
        RedirectPost(URL3);
    });

    // Click button 本日発送予定の書類を検索する(AA)
    $('#btnSearchAA').click(function () {
        RedirectPost(URL4);
    });

    // Click button 棚卸し
    $('#btnInventory').click(function () {
        RedirectPost(URL5);
    });

    $('#btnExportCsv').click(function () {
        RedirectPost(URL8);
    });
    //$('#btnImport').click(function () {
    //    var file = $("#fileUploadContent").val();
    //    var check = 0;
    //    if ($('#checkBox').prop("checked")) check = 1;
    //    var mode = '';
    //    if ($("#rad1").prop("checked")) mode = '6'
    //    else if ($("#rad2").prop("checked")) mode = '7'
    //    else if ($('#rad3').prop("checked")) mode = '8'
    //    else if ($('#rad4').prop("checked")) mode = '9'
    //    else if ($('#rad5').prop("checked")) mode = '10'
    //    var xhr = new XMLHttpRequest();
    //    var fd = new FormData($('#form'));
    //    var fileinput = document.getElementById('fileUpload');

    //    xhr.open("POST", UPLOAD_FILE, true);
    //    xhr.send(fd);
    //    CallAjaxPost(UPLOAD_FILE, {mode:mode, check:check}, onsuccess,null);
    //});

    //window.alert($("#DCW002MessContent").val(),$("#DCW002MessContent").val());
    //window.prompt($("#DCW002MessType").val(), $("#DCW002MessContent").val());

        ShowMsgBox($('#DCW002MessType').val(), $('#DCW002MessContent').val());
        $('#DCW002MessType').val('');
        $('#DCW002MessContent').val('')
});

function fileSelected(input) {
    if ($('#fileUpload').val() == '') {
        ("#lblPatchFile").text('選択されていません');
    }
    else {
        var fileNameIndex = $("#fileUpload").val().lastIndexOf("\\") + 1;
        var filename = $("#fileUpload").val().substr(fileNameIndex);
        $("#lblPatchFile").text(filename);
    }
}

