var AjaxSuccess = true;

$(document).ready(function () {
	// Show mesage validate
	ShowMessengerValidate();

	readyAndAjaxComplete();

	//$(".btn_common").keypress(function (e) {
	//	var kCode = e.keyCode || e.charCode; //for cross browser
	//	if (kCode == 13) {
	//		$(this).click();
	//		return false;
	//	}
	//});

	//// prevent enter on submit
	//$("form").on("keyup keypress", function (e) {
	//	if ($("button[type='submit'], input[type='submit'], textarea, select").is(":focus")) {
	//		return true;
	//	}
	//	var code = e.keyCode || e.which;
	//	if (code == 13) {
	//		e.preventDefault();
	//		return false;
	//	}
	//});

	// Cover Currency To Number when form submit
	$("form:first").submit(function (event) {
		if (FlgCanSubmit) {
			$('form input.autoCompleteMoney, form input.autoCompleteMoneyBlank').each(function () {
				if ($(this).val()) {
					$(this).val(CurrencyToNumber($(this).val()));
				}
			});
		}
	});

	// Set default flag submit
	FlgCanSubmit = true;
	$(':submit').bind('click', function () {
		FlgCanSubmit = true;
	});

	// If form invalid set flag submit is false
	$('form').bind('invalid-form.validate', function (form, validator) {
		if (validator.errorList.length) {
			FlgCanSubmit = false;
		}
	});

	// prevent event when tag <a> has class disabled
	$('a[disabled="disabled"]').click(function (e) {
		e.preventDefault();
	});

	// remove href when tag <a> has class disabled
	$('a[disabled="disabled"]').attr('href', "javascript:void(0)");
});

function readyAndAjaxComplete() {
	// Set Tab Id for Multiple Tab
	SetTabId();

	// Auto Auto Complete Money for input textbox
	AutoCompleteMoney();

	AutoCompleteMoneyNegative();

	AutoCompleteDate();

	AutoCompleteYearMonth();

	$("#lockDiv").css("display", "none");
	$("#loading").hide();
	$(".loading_popup").hide();

	if (!AjaxSuccess) return;
	AjaxSuccess = false;

	// Auto title text truncate 
	$(document).on('mouseenter', ".tbl_fixed .tbl_truncate:not(.relative), .tbl_fixed .tbl_truncate > div", function () {
		var $this = $(this);
		if (this.offsetWidth < this.scrollWidth) {
			// Show by title 
			$this.attr("title", $this.text());
		} else {
			$this.removeAttr("title");
		}
	});
};

function AutoCompleteDate() {
	$('.autoCompleteDate').each(function () {
		if ($(this).val() != '') {
			$(this).val($.datepicker.formatDate('yy/mm/dd', new Date($(this).val())));
		}
	});

	if ($('.autoCompleteDate').length > 0) {
		$('.autoCompleteDate').blur(function (e) {
			ConvertDateTime(e.target.id, e.target.value);
		});

		$(".autoCompleteDate").datepicker({
			firstDay: 1,
			dateFormat: 'yy/mm/dd',
			showOn: "both",
			buttonImage: "/Content/themes/base/images/jquery_trigger_27.png",
			buttonImageOnly: true,
			buttonText: "",
			onClose: function () {
				$(this).valid();
				$(this).focus();
			},
			beforeShow: function (el, dp) {
				$('#ui-datepicker-div')[$(el).is('[data-calendar="false"]') ? 'addClass' : 'removeClass']('hide-calendar');
			}
		});
	}
}

function AutoCompleteYearMonth() {
	$('.autoCompleteYearMonth').each(function () {
		if ($(this).val() != '') {
			$(this).val($.datepicker.formatDate('yy/mm', new Date($(this).val())));
		}
	});

	if ($('.autoCompleteYearMonth').length > 0) {
		$('.autoCompleteYearMonth').blur(function (e) {
			ConvertDateTime(e.target.id, e.target.value, 1);
		});

		$(".autoCompleteYearMonth").datepicker({
			showOn: "both",
			buttonImage: "/Content/themes/base/images/jquery_trigger_27.png",
			buttonImageOnly: true,
			changeMonth: true,
			changeYear: true,
			buttonText: "",
			showButtonPanel: true,
			dateFormat: 'yy/mm',
			onClose: function (dateText, inst) {
				function isDonePressed() {
					return ($('#ui-datepicker-div').html().indexOf('ui-datepicker-close ui-state-default ui-priority-primary ui-corner-all ui-state-hover') > -1);
				}
				if (isDonePressed()) {
					var iMonth = $("#ui-datepicker-div .ui-datepicker-month :selected").val();
					var iYear = $("#ui-datepicker-div .ui-datepicker-year :selected").val();
					$(this).datepicker('setDate', new Date(iYear, iMonth, 1));
				}
			},
			beforeShow: function (el, dp) {
				$('#ui-datepicker-div')[$(el).is('[data-calendar="false"]') ? 'addClass' : 'removeClass']('hide-calendar');
				if ((selDate = $(this).val()).length > 0) {
					iYear = selDate.substring(0, selDate.length - 3);
					iMonth = parseInt(selDate.substring(selDate.length - 2, selDate.length)) - 1;
					$(this).datepicker('option', 'defaultDate', new Date(iYear, iMonth, 1));
					$(this).datepicker('setDate', new Date(iYear, iMonth, 1));
				}
			}
		});
	}
}

function ConvertDateTime(inputID, value, checkYearAndMonth) {
	value = value.replace(/^\s+|\s+$/g, '');
	if (value == "") {
		//SetColor(inputID, 'white');
	}
	else if (checkYearAndMonth == 1 && value.length == 7) {
		var yyyy = value.substring(0, 4);
		var mm = value.substring(5, 7);
		var dd = 01;
		if (!CheckDate(yyyy, mm, dd)) {
			SetColor(inputID, 'error');
			$('#' + inputID).val($.trim($('#' + inputID).val()));
			$('#' + inputID).val('');
			setTimeout(function () {
				$('#' + inputID).focus();
			}, 100);
		}
		else {
			SetColor(inputID, 'white');
			$('#' + inputID).val(yyyy + "/" + mm);
		}
	}
	else if (checkYearAndMonth == 1 && value.length != 7) {
		SetColor(inputID, 'error');
		$('#' + inputID).val($.trim($('#' + inputID).val()));
		$('#' + inputID).val('');
		setTimeout(function () {
			$('#' + inputID).focus();
		}, 100);
	}
	else if (value.length == 4) {
		var mm = value.substring(0, 2);
		var dd = value.substring(2, 4);
		var date = new Date();
		var yyyy = date.getFullYear();
		if (!CheckDate(yyyy, mm, dd)) {
			SetColor(inputID, 'error');
			$('#' + inputID).val('');
			setTimeout(function () {
				$('#' + inputID).focus();
			}, 100);
		}
		else {
			SetColor(inputID, 'white');
			$('#' + inputID).val(yyyy + "/" + mm + "/" + dd);
		}
	}
	else if (value.length == 6) {
		var yyyy = "20" + value.substring(0, 2);
		var mm = value.substring(2, 4);
		var dd = value.substring(4, 6);
		if (!CheckDate(yyyy, mm, dd)) {
			SetColor(inputID, 'error');
			$('#' + inputID).val($.trim($('#' + inputID).val()));
			$('#' + inputID).val('');
			setTimeout(function () {
				$('#' + inputID).focus();
			}, 100);
		}
		else {
			SetColor(inputID, 'white');
			$('#' + inputID).val(yyyy + "/" + mm + "/" + dd);
		}
	}
	else if (value.length == 8) {
		if (value.indexOf("/") >= 0) {
			var yyyy = value.substring(0, 4);
			var mm = value.substring(5, 6);
			var dd = value.substring(7, 8);
			if (!CheckDate(yyyy, mm, dd)) {
				SetColor(inputID, 'error');
				$('#' + inputID).val($.trim($('#' + inputID).val()));
				$('#' + inputID).val('');
				setTimeout(function () {
					$('#' + inputID).focus();
				}, 100);
			}
			else {
				SetColor(inputID, 'white');
			}
		}
		else {
			var yyyy = value.substring(0, 4);
			var mm = value.substring(4, 6);
			var dd = value.substring(6, 8);
			if (!CheckDate(yyyy, mm, dd)) {
				SetColor(inputID, 'error');
				$('#' + inputID).val($.trim($('#' + inputID).val()));
				$('#' + inputID).val('');
				setTimeout(function () {
					$('#' + inputID).focus();
				}, 100);
			}
			else {
				SetColor(inputID, 'white');
				$('#' + inputID).val(yyyy + "/" + mm + "/" + dd);
			}
		}
	}
	else if (value.length == 10
			&& value.indexOf("/") >= 0) {
		var yyyy = value.substring(0, 4);
		var mm = value.substring(5, 7);
		var dd = value.substring(8, 10);
		if (!CheckDate(yyyy, mm, dd)) {
			SetColor(inputID, 'error');
			$('#' + inputID).val($.trim($('#' + inputID).val()));
			$('#' + inputID).val('');
			setTimeout(function () {
				$('#' + inputID).focus();
			}, 100);
		}
		else {
			SetColor(inputID, 'white');
			$('#' + inputID).val(yyyy + "/" + mm + "/" + dd);
		}
	}
	else if (value.length > 0 && value.length <= 10) {
		SetColor(inputID, 'error');
		$('#' + inputID).val($.trim($('#' + inputID).val()));
		$('#' + inputID).val('');
		setTimeout(function () {
			$('#' + inputID).focus();
		}, 100);
	}
	else {
		SetColor(inputID, 'error');
		$('#' + inputID).val($.trim($('#' + inputID).val()));
		$('#' + inputID).val('');
		setTimeout(function () {
			$('#' + inputID).focus();
		}, 100);
	}
};

function SetColor(inputID, color) {
	// QuanNH7 - fix case not display backgroud color when data invalid
	var attr = $('#' + inputID).attr('data-val-excomparedateform');
	var invalid = $('#' + inputID).attr('aria-invalid');
	if (typeof attr == 'undefined') {
		if (color === 'error') {
			$('#' + inputID).addClass('input-validation-error');
		}
		else
			$('#' + inputID).removeClass('input-validation-error');
	}
	else {
		if (invalid == false) {
			if (color === 'error') {
				$('#' + inputID).addClass('input-validation-error');
			}
			else
				$('#' + inputID).removeClass('input-validation-error');
		}
	}
}

$(document).ajaxComplete(function (event, request, settings) {
	setTimeout(readyAndAjaxComplete, 10);
});

$(document).ajaxStart(function () {
	var flgCheckLoadingPopup = $('body').hasClass('modal-open');
	if (!flgCheckLoadingPopup) {
		$("#loading").show();
	}
	$(".loading_popup").show();
	$("#lockDiv").css("display", "block");

	if (!AjaxSuccess) return;
	AjaxSuccess = true;
});

window.onunload = function (e) {
	$("#lockDiv").css("display", "block");
};

function CheckDate(year, month, day) {
	var isleap = (year % 4 == 0 && (year % 100 != 0 || year % 400 == 0));
	if (isNaN(year) || year < 1753) {
		return false;
	}
	if (month < 1 || month > 12 || isNaN(month)) {
		return false;
	}
	else if (day < 1 || day > 31 || isNaN(day)) {
		return false;
	}
	else if ((month == 4 || month == 6 || month == 9 || month == 11) && day == 31) {
		return false;
	}
	else if (month == 2 && (day > 29 || (day == 29 && !isleap))) {
		return false;
	}
	else {
		return true;
	}
}

function CheckFocusRadio() {
	$('input[type="radio"]').focus(function () {

		$("label[for='" + this.id + "']").addClass('labelfocus');

	}).blur(function () {

		$("label").removeClass("labelfocus");

	});

	$('input[type="checkbox"]').focus(function () {

		$("label[for='" + this.id + "']").addClass('labelfocus');

	}).blur(function () {

		$("label").removeClass("labelfocus");

	});
}

function TruncateText(text, i) {
	if (text.length > i) {
		text = text.substring(0, i) + "...";
	}
	return text;
}

function CutString(nameClass, i) {
	$('.' + nameClass).each(function () {
		var txt = $(this).text();
		var txt = TruncateText(txt, i);
		$(this).html(txt);
	});
}

function CutStringByLength(nameClass, i) {
	$(nameClass).each(function () {
		var txt = $(this).text();
		var txt = TruncateText(txt, i);
		$(this).html(txt);
	});
}

function createCookie(name, value, second) {
	if (second) {
		var date = new Date();
		date.setTime(date.getTime() + (second * 1000));
		var expires = "; expires=" + date.toGMTString();
	}
	else var expires = "";
	document.cookie = name + "=" + value + expires + "; path=/";
}

function getCookie(name) {
	var re = new RegExp(name + "=([^;]+)");
	var value = re.exec(document.cookie);
	return (value != null) ? unescape(value[1]) : null;
}

// Sample : RedirectPost("http://localtion", { args0: "content0", args1: "content1"});
function RedirectPost(location, args) {
	var formElement = '';

	// Get TabId from SessionStore
	var tabId = sessionStorage.getItem('UniqueTabSession');
	args = args ? args : {};
	$.each(args, function (key, value) {
		formElement += '<input type="hidden" name="' + key + '" value="' + value + '">';
	});

	// Add TabId as a hidden field
	formElement += '<input type="hidden" name="hfldUniqueTabSession" value="' + tabId + '">';
	$('<form action="' + location + '" method="POST">' + formElement + '</form>').appendTo('body').submit();
}

function CallAjaxPost(location, jsonData, funcSuccess, funcError) {
	var tabId = sessionStorage.getItem('UniqueTabSession');

	if (IsEmpty(jsonData)) {
		jsonData = {};
	}
	jsonData.hfldUniqueTabSession = (IsEmpty(tabId)) ? '' : tabId;

	$.ajax({
		url: location,
		type: "POST",
		contentType: "application/jsons; charset=utf-8",
		data: JSON.stringify(jsonData),
		success: function (data) {
			if (IsEmpty(data)==false && data.CommonError) {
				window.location.href = data.url;
				return;
			}

			if (funcSuccess) funcSuccess(data);
		},
		error: function (data) {
			if (IsEmpty(data) == false && (data.statusText == "error" || data.statusText == "timeout")) {
				window.location.href = "/Common/Error/SysException";
				return;
			}

			if (funcError) funcError(data);
		}
	});
}

function ShowMessengerValidate(rootModal) {

	// Check item empty
	rootModal = rootModal ? rootModal + ' ' : '';

	// Get element show error
	var strErrValidate = $(rootModal + '#HiddenErrorValidate').html();

	if (strErrValidate) {

		// Make list li error
		strMess = '<ul><li>' + strErrValidate.replace(/\|\|/gi, "</li><li>") + '</li></ul>';

		// Display error
		DisplayMsgBox("E", strMess, null, CloseAction);
	}
}

function CloseAction() {
	$(".input-validation-error:first").focus();
}

function IsEmpty(val) {
	return (val === undefined || val == null || val.length <= 0) ? true : false;
}

function AutoCompleteMoney() {
	$(document).on('focusin', 'input.autoCompleteMoney, input.autoCompleteMoneyBlank', function () {
		$(this).val($(this).val() ? CurrencyToNumber($(this).val()) : '');
		$(this).inputmask("integer", { negationSymbol: { front: " " } });
		$(this).unbind('cut.inputmask');
		$(this).attr('maxlength', 9);
		$(this).css("ime-mode", "disabled");
		$(this).attr("type", "tel");
	});

	$(document).on('focusout', 'input.autoCompleteMoney, input.autoCompleteMoneyBlank', function () {
		$(this).inputmask('remove');
		$(this).removeAttr('maxlength');
		$(this).val($(this).val() ? NumberToCurrency($(this).val()) : '');
	});

	$(document).on('keydown', 'input.autoCompleteMoney, input.autoCompleteMoneyBlank', function (e) {
		// Allow: backspace, delete, tab, escape, enter
		if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110]) !== -1 ||
			// Allow: Ctrl+A, Ctrl+C, Ctrl+V, Ctrl+X, Ctrl+Z
			(e.ctrlKey === true && $.inArray(e.keyCode, [65, 67, 86, 88, 90]) !== -1) ||
			// Allow: home, end, left, right, down, up
			(e.keyCode >= 35 && e.keyCode <= 40)) {
			// let it happen, don't do anything
			return;
		}

		// Ensure that it is a number and stop the keypress
		if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
			e.preventDefault();
		}
	});

	$(document).on('keyup', 'input.autoCompleteMoney, input.autoCompleteMoneyBlank', function (e) {
		$(this).on('keyup', function (e) {
			var item = $(this);
			var replace = item.val().replace(/\D/g, '');
			if (item.val() != replace) {
				item.val(replace);
			}
		});
	});

	ConvertAllNumberMoney();
}

function AutoCompleteMoneyNegative() {
	$(document).on('focusin', 'input.autoCompleteMoney_', function () {
		var a = CurrencyToNumber($(this).val());
		$(this).val($(this).val() ? CurrencyToNumberNegative($(this).val()) : '');
		$(this).inputmask("integer");
		$(this).unbind('cut.inputmask');
		$(this).attr('maxlength', 9);
		$(this).css("ime-mode", "disabled");
		$(this).attr("type", "tel");
	});

	$(document).on('focusout', 'input.autoCompleteMoney_', function () {
		$(this).inputmask('remove');
		$(this).removeAttr('maxlength');
		$(this).val($(this).val() ? NumberToCurrency($(this).val()) : '');
	});

	$(document).on('keydown', 'input.autoCompleteMoney_', function (e) {
		// Allow: backspace, delete, tab, escape, enter
		if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110]) !== -1 ||
			// Allow: Ctrl+A, Ctrl+C, Ctrl+V, Ctrl+X, Ctrl+Z
			(e.ctrlKey === true && $.inArray(e.keyCode, [65, 67, 86, 88, 90]) !== -1) ||
			// Allow: home, end, left, right, down, up
			(e.keyCode >= 35 && e.keyCode <= 40)) {
			// let it happen, don't do anything
			return;
		}

		// Ensure that it is a number and stop the keypress
		if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105) && e.keyCode == 45) {
			e.preventDefault();
		}
	});

	ConvertAllNumberMoneyNegative();

	//$(document).on('keyup', 'input.autoCompleteMoney_, input.autoCompleteMoneyBlank', function (e) {
	//	$(this).on('keyup', function (e) {
	//		var item = $(this);
	//		var replace = item.val().replace(/\D/g, '');
	//		if (item.val() != replace) {
	//			item.val(replace);
	//		}
	//	});
	//});

	//// convert number to curency for input money
	//if ($('.autoCompleteMoney_').length > 0) {
	//	$('body .autoCompleteMoney_').each(function () {
	//		$(this).val(NumberToCurrency($(this).val()));
	//	});
	//}
}

function CurrencyToNumberNegative(currency) {
	if (currency != null) {
		var numberResult = currency.replace(/[^0-9\.\-]+/g, "");
		return Number(numberResult.replace(/[^0-9\,\-]+/g, ""));
	}
	else {
		return null;
	}
};

function ConvertAllNumberMoneyNegative() {

	// convert number to curency for input money
	if ($('.autoCompleteMoney_').length > 0) {
		$('body .autoCompleteMoney_').each(function () {
			$(this).val(NumberToCurrency($(this).val()));
		});
	}

	// convert number to curency for input money
	if ($('.autoCompleteMoneyBlank').length > 0) {
		$('body .autoCompleteMoneyBlank').each(function () {
			$(this).val(NumberToCurrency($(this).val()));
		});
	}
}

function ConvertAllNumberMoney() {
	// convert number to curency for label money
	if ($('.autoCompleteMoney-label').length > 0) {
		$('body .autoCompleteMoney-label').each(function () {
			$(this).text(NumberToCurrency($(this).text()));
		});
	}

	// convert number to curency for input money
	if ($('.autoCompleteMoney').length > 0) {
		$('body .autoCompleteMoney').each(function () {
			$(this).val(NumberToCurrency($(this).val()));
		});
	}

	// convert number to curency for input money
	if ($('.autoCompleteMoneyBlank').length > 0) {
		$('body .autoCompleteMoneyBlank').each(function () {
			$(this).val(NumberToCurrency($(this).val()));
		});
	}
}

function moneyInput() {
	$(document).on('focusin', 'input.autoCompleteMoney, input.autoCompleteMoneyBlank', function () {
		$(this).val($(this).val() ? CurrencyToNumber($(this).val()) : '');
		$(this).css("ime-mode", "disabled");
		$(this).attr("type", "tel");
	});

	$(document).on('focusout', 'input.autoCompleteMoney, input.autoCompleteMoneyBlank', function () {
		$(this).val($(this).val() ? NumberToCurrency($(this).val()) : '');
	});

	$(document).on('keydown', 'input.autoCompleteMoney, input.autoCompleteMoneyBlank', function (e) {
		// Allow: backspace, delete, tab, escape, enter and .
		if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 ||
			// Allow: Ctrl+A, Ctrl+C, Ctrl+V, Ctrl+X, Ctrl+Z
			(e.ctrlKey === true && $.inArray(e.keyCode, [65, 67, 86, 88, 90]) !== -1) ||
			// Allow: home, end, left, right, down, up
			(e.keyCode >= 35 && e.keyCode <= 40)) {
			// let it happen, don't do anything
			return;
		}
		// Ensure that it is a number and stop the keypress
		if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
			e.preventDefault();
		}
	});

	$(document).on('paste', 'input.autoCompleteMoney, input.autoCompleteMoneyBlank', function () {
		var item = $(this);
		window.setTimeout(function () {
			item.val(item.val().replace(/\D/g, ''));
		}, 0.00000001);
	});
}

function CurrencyToNumber(currency) {
	if (currency != null) {
		var numberResult = currency.replace(/[^0-9\.]+/g, "");
		return Number(numberResult.replace(/[^0-9\,]+/g, ""));
	}
	else {
		return null;
	}
};

function NumberToCurrency(number) {
	if (number != null) {
		return (number + "").replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,");
	}
	else {
		return null;
	}
};

function Popup(link, windowname) {
	if (IsEmpty(windowname)) windowname = "CarUpgrade";
	if (!window.focus) return true;
	var href;
	if (typeof (link) == 'string')
		href = link;
	else
		href = link.href;
	window.open(href, windowname, 'width=600,height=480,scrollbars=yes');
	return false;
}

// Arguments :
//  verb : 'GET'|'POST'
//  target : an optional opening target (a name, or "_blank"), defaults to "_self"
PopupWithMethodPost = function (verb, url, data, target) {
	var meta = document.createElement('meta');
	meta.name = "referrer";
	meta.content = "always";
	document.getElementsByTagName('head')[0].appendChild(meta);
	var form = document.createElement("form");
	if (IsEmpty(verb)) verb = "POST";
	if (IsEmpty(target)) target = "_blank";
	form.action = url;
	form.method = verb;
	form.target = target;
	if (data) {
		for (var key in data) {
			var input = document.createElement("textarea");
			input.name = key;
			input.value = typeof data[key] === "object" ? JSON.stringify(data[key]) : data[key];
			form.appendChild(input);
		}
	}
	form.style.display = 'none';
	document.body.appendChild(form);
	document.getElementsByTagName('head')[0].appendChild(meta);
	form.submit();
};

function TitleTruncate(element) {
	element = element ? element + ', .tbl_truncate' : '.tbl_truncate';

	// Auto title text truncate
	$(document).on('mouseenter', element, function() {
		var $this = $(this);
		if (this.offsetWidth < this.scrollWidth) {
			// Show by title
			$this.attr("title", $this.text());
		} else {
			$this.removeAttr("title");
		}
	});
}

// disableAllButton
function disableAll() {
	$('input[type="button"], button').attr('disabled', 'disabled');
	$('a').attr('disabled', 'disabled');
	$('a[disabled="disabled"]').attr('href', "javascript:void(0)");
}