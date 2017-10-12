$(document).ready(function () {


	Initial();

	SetCheckBox();

	SetFormatDate();

    ///cell break
	$(".cell_break").each(function () {
	    var html = $(this).html().split(" ");
	    html = html[0] + "<br>" + html.slice(1).join(" ");
	    $(this).html(html);
	});
    //sole table
	$('.solecolor .checkCL').each(function (index) {
	    if (index % 2 == 1) {
	        $(".checkCL_" + index).addClass("bg_solebar");
	    }
	});
	$(document).on('click', '.headerCheckbox', function () {
	    if (document.getElementById('IsNewCar').checked) {
	        $('.checkboxItem').prop('checked', true);
	    }
	    else {
	        $('.checkboxItem').prop('checked', false);
	    }
	});

	$(document).on('click', '.checkboxItem', function () {
	    
	        for (var i = 0; i < $('#countCheckbox').val() ; i++) {
	            if (!document.getElementById('chk-' + i).checked) {
	                $('.headerCheckbox').prop('checked', false);
	                break;
	            }
	            else {
	                $('.headerCheckbox').prop('checked', true);
	            }
	        }
	    
	});

	$(document).on('change', 'input[name="RadioType"]', function () {
		var response = $('label[for="' + this.id + '"]').html();
		switch (response) {
			case "下4桁検索":
				$("#RadioType").val('1');
				break;
			case "完全一致":
				$("#RadioType").val('2');
				break;
			case "前方一致":
				$("#RadioType").val('3');
				break;
			case "後方一致":
				$("#RadioType").val('4');
				break;
		}
	});

	$('#btnSearch').click(function () {

		var form = $('#submitSearchForm');

		if (form.valid()) {
			SetValue();
			var formJson = prepareDataPost('#submitSearchForm');
			CallAjaxPost(SEARCH, formJson, function (detailModel) {
				$("#tblTabs").empty().html(detailModel);
				$(".checkboxFuzokuhin").each(function () {
					var cell = $(this).closest('.Cell');
					if ($(cell).find('input[name*=IsChecked]').val() == 1) {
						$(cell).find('input[type=checkbox]').prop('checked', true);
					}
				});
				$('select[name*=DocStatus] option[value=101]').remove();
				$("#ModeImport").val('');
			    ///cell break
				$(".cell_break").each(function () {
				    var html = $(this).html().split(" ");
				    html = html[0] + "<br>" + html.slice(1).join(" ");
				    $(this).html(html);
				});
			    //sole table
				$('.solecolor .checkCL').each(function (index) {
				    if (index % 2 == 1) {
				        $(".checkCL_" + index).addClass("bg_solebar");
				    }
				});
				SetButton();
				SetFormatDate();
				ShowMsgBox(MessageTypeI0001, MessageContentI0001);
			}, null);
		}
	});

	$(document).on("click", ".btnUpdate", function () {
		var row = $(this).closest('.tbl_row');
		var docControlNo = row.find('input[name*=DocControlNo]').val();
		var uriageShuppinnTorokuNo = row.find('input[name*=UriageShuppinnTorokuNo]').val();
		var masshoFlg = row.find('input[name*=MasshoFlg]').val();
		var jishameiFlg = row.find('input[name*=JishameiFlg]').val();
		var docStatus = row.find('input[name*=DocStatus]').val();
		var docNyukoDate = row.find('input[name*=DocNyukoDate]').val();
		var docShukkoDate = row.find('input[name*=DocShukkoDate]').val();
		var jishameiIraiShukkoDate = row.find('input[name*=JishameiIraiShukkoDate]').val();
		var jishameiKanryoNyukoDate = row.find('input[name*=JishameiKanryoNyukoDate]').val();
		var masshoIraiShukkoDate = row.find('input[name*=MasshoIraiShukkoDate]').val();
		var masshoKanryoNyukoDate = row.find('input[name*=MasshoKanryoNyukoDate]').val();
		var memo = row.find('textarea[name*=Memo]').val();
		if (docStatus == '102' && docNyukoDate == "") {
		    var date = new Date();
		    docNyukoDate = date.getFullYear() + '/' + date.getMonth() + '/' + date.getDate();
		    row.find('input[name*=DocNyukoDate]').val(docNyukoDate);
		}
		if (docStatus == '105' && docShukkoDate == "") {
		    var date = new Date();
		    docShukkoDate = date.getFullYear() + '/' + date.getMonth() + '/' + date.getDate();
		    row.find('input[name*=DocShukkoDate]').val(docShukkoDate);
		}

		var data = {
			DocControlNo: docControlNo,
			UriageShuppinnTorokuNo: uriageShuppinnTorokuNo,
			MasshoFlg: masshoFlg,
			JishameiFlg: jishameiFlg,
			DocStatus: docStatus,
			DocNyukoDate: docNyukoDate,
			DocShukkoDate: docShukkoDate,
			JishameiIraiShukkoDate: jishameiIraiShukkoDate,
			JishameiKanryoNyukoDate: jishameiKanryoNyukoDate,
			MasshoIraiShukkoDate: masshoIraiShukkoDate,
			MasshoKanryoNyukoDate: masshoKanryoNyukoDate,
			Memo: memo
		};
		CallAjaxPost(UPDATE, data, onSuccess, onFailed);
	});

	$(document).on("click", "#btnUpdateAll", function () {
		if ($("#ModeImport").val() == 1) {
			$('.tblDetail').each(function () {
				var row = $(this).closest('.tbl_row');
				row.find('input[name*=DocStatus]').val('102');
				row.find('select[name*=DocStatus]').val('102');
				$("#ModeImport").val('');
			});
		}
		$('.headerCheckbox').prop('checked', false);
		var listData = GetListSubmit();
		CallAjaxPost(UPDATE_ALL, { resultModel: listData }, onSuccess, onFailed);
		SetButton();
	});

	$(document).on("click", "#btnRegister", function () {
		var formJson = prepareDataPost('#formTab2');
		CallAjaxPost(REGISTER, formJson, onSuccess, onFailed);
	});

	$(document).on("click", "#btnPrintPage", function () {
		var ReportId = 'RD0020';
		var listData = GetListSubmit();
		CallAjaxPost(DCW003PrintUrl, { resultModel: listData, ReportId: ReportId }, OpenTab, onFailed);
	});

	function OpenTab(data) {
		var listData = GetListSubmit();
		if (data.Success != false) {
			OpenWindowWithPost(DCW003PrintUrl, listData)
		}
		else {
			ShowMsgBox(data.MessageClass, data.Message, null, null, null, null);
		}
	}
	function OpenTabCsv(data) {
		var listData = GetListSubmit();
		if (data.Success != false) {
			OpenWindowWithPostCsv(DCW003ExportCSVUrl, listData)
		}
		else {
			ShowMsgBox(data.MessageClass, data.Message, null, null, null, null);
		}
	}

	function OpenWindowWithPostCsv(url, params) {
		var form = document.createElement("form");
		form.setAttribute("method", "post");
		form.setAttribute("action", url);
		form.setAttribute("onsubmit", window.open('about:blank', "_form"));
		form.setAttribute("target", "_form");
		for (var k = 0; k < params.length; k++) {
			for (var i in params[k]) {
				if (params[k].hasOwnProperty(i)) {
					var input = document.createElement('input');
					input.type = 'hidden';
					input.name = '[' + k + '].' + i;
					input.value = params[k][i];
					form.appendChild(input);
				}
			}
		}
		window.open('about:blank', "_form").close();
		document.body.appendChild(form);
		form.submit();

	}
	function OpenWindowWithPost(url, params) {
		var form = document.createElement("form");

		form.setAttribute("method", "post");
		form.setAttribute("action", url);
		form.setAttribute("onsubmit", window.open('about:blank', "_form"));
		form.setAttribute("target", "_form");
		for (var k = 0; k < params.length; k++) {
			for (var i in params[k]) {
				if (params[k].hasOwnProperty(i)) {
					var input = document.createElement('input');
					input.type = 'hidden';
					input.name = '[' + k + '].' + i;
					input.value = params[k][i];
					form.appendChild(input);
				}
			}
		}

		document.body.appendChild(form);
		form.submit();

	}

	$(document).on("click", "#btnExportCSV", function () {
		var listData = GetListSubmit();
		CallAjaxPost(DCW003ExportCSVUrl, { resultModel: listData }, OpenTabCsv, onFailed);
	});

	$(document).on("click", "#btnSendAutoSearch", function () {
		var listData = GetListSubmit();
		CallAjaxPost(SEND_AUTO_SEARCH, { resultModel: listData }, onSuccess, onFailed);
	});

	$(document).on('click', '.checkboxFuzokuhin', function () {
        
		var Cell = $(this).closest('.Cell');
		var checked = $(Cell).find('input[type=checkbox]').prop('checked');
		if (checked == true) {
			$(Cell).find('input[name*=IsChecked]').val(1);
		}
		else {
			$(Cell).find('input[name*=IsChecked]').val(0);
		}
		
	});

	$(document).on('change', '.dropYear', function () {
		var Cell = $(this).closest('.Cell');
		var value = $(Cell).find('select[name*=Note]').val();
		if (value != '') {
			$(Cell).find('input[name*=IsChecked]').val(1);
			$(Cell).find('input[name*=Note]').val(value);
		}
		else {
			$(Cell).find('input[name*=IsChecked]').val(0);
			$(Cell).find('input[name*=Note]').val('');
		}

	});

	$(document).on('blur', '.txtNote', function () {
		var Cell = $(this).closest('.Cell');
		var value = $(Cell).find('.txtNote').val();
		if (value != '') {
			$(Cell).find('input[name*=Note]').val(value);
			$(Cell).find('input[name*=IsChecked]').val(1);
		}
		else {
			$(Cell).find('input[name*=IsChecked]').val(0);
		}
		
	});

	$(document).on('change', 'select[name*=MasshoFlg]', function () {
		//var value = $('select[name*=MasshoFlg]').val();
		//var row = $(this).closest('.tbl_row');
	    //row.find('input[name*=MasshoFlg]').val(value);
	    var row = $(this).closest('.tbl_row');
	    var value = row.find('select[name*=MasshoFlg]').val();
	    row.find('input[name*=MasshoFlg]').val(value);
	});

	$(document).on('change', 'select[name*=DocStatus]', function () {
	    var row = $(this).closest('.tbl_row');
	    var value = row.find('select[name*=DocStatus]').val();
		row.find('input[name*=DocStatus]').val(value);
	});

	$(document).on('change', 'select[name*=JishameiFlg]', function () {
	    var row = $(this).closest('.tbl_row');
	    var value = row.find('select[name*=JishameiFlg]').val();
		row.find('input[name*=JishameiFlg]').val(value);
	});

	ShowMsgBox(MessageTypeI0010, MessageContentI0010);
	
});

function Initial() {
	//$("#RadioType").val('1');
	SetButton();
}

//Set button
function SetButton() {

	var flgCheck = false;
	$(".tblDetail").each(function () {
		if ($(this).find("input[type=checkbox]")) {
			flgCheck = true;
			return;
		}
	});

	if (flgCheck) {
	    $('.cancel_11, .cancel_10,.cancel_01,.cancel_1').addClass("bg_cancel");
		$('#btnPrintPage').removeAttr('disabled');
		$('#btnExportCSV').removeAttr('disabled');
		$('#btnSendAutoSearch').removeAttr('disabled');
		$('#btnUpdateAll').removeAttr('disabled');

		var flg = $("#ModeImport").val()
		if (flg == 1) {
			$('#btnPrintPage').attr('disabled', true);
			$('#btnExportCSV').attr('disabled', true);
			$('#btnSendAutoSearch').attr('disabled', true);
			$('#btnSearch').attr('disabled', true);
			$('.btnBack').attr('disabled', true);
			$('#btnUpdateAll').removeAttr('disabled');
			$('.checkboxItem').prop('checked', true);
			$('.droplist').attr('disabled', true);
			$('.txtItem').attr('disabled', true);
			$('.date').attr('disabled', true);
			$('.btnUpdate').attr('disabled', true);
		}
		else if (flg == 2) {
			$(".suggestItem").css('background-color', 'darkorange');
			$('input[name*=ReportType]').each(function () {
				var row = $(this).closest('.tbl_row');
				var reportType = $(row).find('input[name*=ReportType]').val();
				if (reportType == "1") {
					$(row).find('.jishameiColor').css('background-color', 'darkorange');
					$(row).find('input[name*=JishameiKanryoNyukoDate]').css('background-color', 'darkorange');
				}
				else if (reportType == "2" || reportType =="4") {
					$(row).find('.masshoColor').css('background-color', 'darkorange');
					$(row).find('input[name*=MasshoKanryoNyukoDate]').css('background-color', 'darkorange');
				}
			});
		}
		else if (flg == 3) {
			$('.checkboxItem').prop('checked', true);
		}
		else if (flg == 4) {
			$('input[name*=ReportType]').each(function () {
				var row = $(this).closest('.tbl_row');
				var reportType = $(row).find('input[name*=ReportType]').val();
				if (reportType == "1") {
					$(row).find('input[name*=JishameiKanryoNyukoDate]').css('background-color', 'darkorange');
					$(row).find('select[name*=DocStatus]').css('background-color', 'darkorange');
					$(row).find('select[name*=DocStatus]').val(103);
				}
				else if (reportType == "2") {
					$(row).find('input[name*=MasshoKanryoNyukoDate]').css('background-color', 'darkorange');
					$(row).find('select[name*=DocStatus]').css('background-color', 'darkorange');
					$(row).find('select[name*=DocStatus]').val(104);
				}
			});	
		}
		else {
			$('.checkboxItem').prop('checked', false);
			$('.droplist').removeAttr('disabled');
			$('.txtItem').removeAttr('disabled');
			$('.date').removeAttr('disabled');
			$('.btnUpdate').removeAttr('disabled');
			$('#btnSearch').removeAttr('disabled');
			$('#btnRegister').removeAttr('disabled');
			$('.btnBack').removeAttr('disabled');
		}
	}
	else {
		$('#btnPrintPage').attr('disabled', true);
		$('#btnExportCSV').attr('disabled', true);
		$('#btnSendAutoSearch').attr('disabled', true);
		$('#btnUpdateAll').attr('disabled', true);
		$('#btnRegister').attr('disabled', true);	
		$('#btnSearch').removeAttr('disabled');
	}
}

function GetListSubmit() {
	var listTable = new Array();
	$('.tblDetail input[type=checkbox]:checked').each(function () {
		var row = $(this).closest('.tbl_row');
		var docControlNo = row.find('input[name*=DocControlNo]').val();
		var uriageShuppinnTorokuNo = row.find('input[name*=UriageShuppinnTorokuNo]').val();
		var masshoFlg = row.find('input[name*=MasshoFlg]').val();
		var jishameiFlg = row.find('input[name*=JishameiFlg]').val();
		var docStatus = row.find('input[name*=DocStatus]').val();
		var docNyukoDate = row.find('input[name*=DocNyukoDate]').val();
		var docShukkoDate = row.find('input[name*=DocShukkoDate]').val();
		var jishameiIraiShukkoDate = row.find('input[name*=JishameiIraiShukkoDate]').val();
		var jishameiKanryoNyukoDate = row.find('input[name*=JishameiKanryoNyukoDate]').val();
		var masshoIraiShukkoDate = row.find('input[name*=MasshoIraiShukkoDate]').val();
		var masshoKanryoNyukoDate = row.find('input[name*=MasshoKanryoNyukoDate]').val();
		var memo = row.find('textarea[name*=Memo]').val();
		var fileNo = row.find('input[name*=FileNo]').val();
		var racNo = row.find('input[name*=RacNo]').val();
		var chassisNo = row.find('input[name*=ChassisNo]').val();

		if (docStatus == '102' && docNyukoDate == "") {
		    var date = new Date();
		    docNyukoDate = date.getFullYear() + '/' + date.getMonth() + '/' + date.getDate();
		    row.find('input[name*=DocNyukoDate]').val(docNyukoDate);
		}
		if (docStatus == '105' && docShukkoDate == "") {
		    var date = new Date();
		    docShukkoDate = date.getFullYear() + '/' + date.getMonth() + '/' + date.getDate();
		    row.find('input[name*=DocShukkoDate]').val(docShukkoDate);
		}

		//Case import file
		if (docStatus == '101') {
			docStatus = '102';
		}
		var data = {
			DocControlNo: docControlNo,
			UriageShuppinnTorokuNo: uriageShuppinnTorokuNo,
			MasshoFlg: masshoFlg,
			JishameiFlg: jishameiFlg,
			DocStatus: docStatus,
			DocNyukoDate: docNyukoDate,
			DocShukkoDate: docShukkoDate,
			JishameiIraiShukkoDate: jishameiIraiShukkoDate,
			JishameiKanryoNyukoDate: jishameiKanryoNyukoDate,
			MasshoIraiShukkoDate: masshoIraiShukkoDate,
			MasshoKanryoNyukoDate: masshoKanryoNyukoDate,
			Memo: memo,
			FileNo: fileNo,
			RacNo: racNo,
			ChassisNo: chassisNo
		};

		listTable.push(data);
	});

	return listTable;
}

function SetValue() {
	if ($('#shohin_DN').is(':checked')) {
		$('#ShohinType').val('101');
	} else {
		$('#ShohinType').val('');
	}

	if ($('#status_102').is(':checked')) {
		$('#DocStatus102').val(1);
	} else {
		$('#DocStatus102').val(0);
	}

	if ($('#status_103').is(':checked')) {
		$('#DocStatus103').val(1);
	} else {
		$('#DocStatus103').val(0);
	}

	if ($('#status_104').is(':checked')) {
		$('#DocStatus104').val(1);
	} else {
		$('#DocStatus104').val(0);
	}

	if ($('#status_105').is(':checked')) {
		$('#DocStatus105').val(1);
	} else {
		$('#DocStatus105').val(0);
	}


	////////////////add by HoaVV begin
	if ($('#chkMode2').is(':checked')) {
		$('#DocumentNomalCar').val(1);
	} else {
		$('#DocumentNomalCar').val(0);
	}
	if ($('#chkMode3').is(':checked')) {
		$('#DocumentNotNomalCar').val(1);
	} else {
		$('#DocumentNotNomalCar').val(0);
	}
	if ($('#chkMode4').is(':checked')) {
		$('#DocumentStocktaking').val(1);
	} else {
		$('#DocumentStocktaking').val(0);
	}
	///////////////////////add by HoaVV end


	if ($('#keiCar_0').is(':checked')) {
		$('#KeiCarFlg0').val('1');
	} else {
		$('#KeiCarFlg0').val('0');
	}

	if ($('#keiCar_1').is(':checked')) {
		$('#KeiCarFlg1').val('1');
	} else {
		$('#KeiCarFlg1').val('0');
	}

	if ($('#jishameiFlg').is(':checked')) {
		$('#JishameiFlg').val('1');
	} else {
		$('#JishameiFlg').val('0');
	}

	if ($('#masshoFlg').is(':checked')) {
		$('#MasshoFlg').val('1');
	} else {
		$('#MasshoFlg').val('0');
	}
}

function SetCheckBox() {

	if ($("#DocStatus102").val() == 1) {
		$('#status_102').prop('checked', true)
	}
	if ($("#DocStatus102").val() == 1) {
	    $('#status_102').prop('checked', true)
	}
	if ($("#DocStatus103").val() == 1) {
	    $('#status_103').prop('checked', true)
	}
	if ($("#DocStatus104").val() == 1) {
	    $('#status_104').prop('checked', true)
	}
	if ($("#DocStatus105").val() == 1) {
	    $('#status_105').prop('checked', true)
	}
	if ($("#ShohinType").val() == '101') {
		$('#shohin_DN').prop('checked', true);
	}
	if ($("#RadioType").val() == 1) {
	    $('#rdFourUp').prop('checked', true);
	}
	if ($("#RadioType").val() == 2) {
	    $('#rdFull').prop('checked', true);
	}
	if ($("#RadioType").val() == 3) {
	    $('#rdFront').prop('checked', true);
	}
	if ($("#RadioType").val() == 4) {
	    $('#rdBehind').prop('checked', true);
	}
	//else if ($("#ShohinType").val() == '102') {
	//	$('#shohin_AA').prop('checked', true);
	//}

	if ($("#KeiCarFlg0").val() == '1') {
		$('#keiCar_0').prop('checked', true);
	}
	if ($("#KeiCarFlg1").val() == '1') {
		$('#keiCar_1').prop('checked', true);
	}

	$(".checkboxFuzokuhin").each(function () {
		var cell = $(this).closest('.Cell');
		if ($(cell).find('input[name*=IsChecked]').val() == 1) {
			$(cell).find('input[type=checkbox]').prop('checked', true);
		}
	});

	$(".txtNote").each(function () {
		var cell = $(this).closest('.Cell');
		if ($(cell).find('input[name*=IsChecked]').val() == 1) {
			var value = $(cell).find('input[name*=Note]').val();
			$(cell).find('.txtNote').val(value);
		}
	});
}

function prepareDataPost(form) {
	var list = $(form).serializeArray();

	// Convert to list object
	var listSubmit = Object();
	$.each(list, function (index, key) {
		listSubmit[key["name"]] = key["value"];
	});

	return listSubmit;
}

function onSuccess(data) {
	if (data.Success) {
		ShowMsgBox(data.MessageClass, data.Message, null, null, null, null);

	} else {
		ShowMsgBox(data.MessageClass, data.Message, null, null, null, null);
	}
}

function onFailed() {
	console.log("failed");
}

function SetFormatDate() {
	$(".autoCompleteDate-lbl").each(function () {
		if ($(this).text().trim() != '') {
			$(this).text($.datepicker.formatDate('yy/mm/dd', new Date($(this).text())));
		}
	});
}

function DCW003PrintPage(shipOrderNo, reportId, cancelFlg) {

	// Set value for form

	$('#lstParam').val();

	$('#ReportId').val(reportId);

	$('#DCW003ViewPrintPage').submit();

	return false;
}
