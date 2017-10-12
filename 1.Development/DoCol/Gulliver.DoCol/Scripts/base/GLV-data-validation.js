// --------------------------------------------------------------------------------------------------------------------
// Version		: 001
// Designer		: TienNT2
// Programmer	: TienNT2
// Date			: 2013/10/01
// Comment		: Create new
// --------------------------------------------------------------------------------------------------------------------

//---------------------------------------------------------------------------------------------------------------------
// Varibles
//---------------------------------------------------------------------------------------------------------------------
var validationScreenErrorMsg = '';
var validationScreenErrorMsg_Backup = ''; // use for checkDoubleClick
var validationErrorSelector = '';

//---------------------------------------------------------------------------------------------------------------------
// Lock key: number
//---------------------------------------------------------------------------------------------------------------------
function OnlyNumberKey() {
    if ($('.Numberic, input[data-val-exnumberic]').length > 0) {
        $('.Numberic, input[data-val-exnumberic]').css("text-align", "right");
        $('.Numberic, input[data-val-exnumberic]').keydown(function (e) {
            var code = (e.keyCode ? e.keyCode : e.which);
            var typed = String.fromCharCode(code);
            var functional = false;

            // allow Ctrl+A
            if ((e.ctrlKey && code == 97 /* firefox */) || (e.ctrlKey && code == 65) /* opera */ || (e.metaKey && code == 65)) functional = true;
            // allow Ctrl+X (cut)
            if ((e.ctrlKey && code == 120 /* firefox */) || (e.ctrlKey && code == 88) /* opera */ || (e.metaKey && code == 88)) functional = true;
            // allow Ctrl+C (copy)
            if ((e.ctrlKey && code == 99 /* firefox */) || (e.ctrlKey && code == 67) /* opera */ || (e.metaKey && code == 67)) functional = true;

            // allow Ctrl+Z (undo)
            if ((e.ctrlKey && code == 122 /* firefox */) || (e.ctrlKey && code == 90) /* opera */ || (e.metaKey && code == 90)) return true;
            // allow or deny Ctrl+V (paste), Shift+Ins
            if ((e.ctrlKey && code == 118 /* firefox */) || (e.ctrlKey && code == 86) /* opera */ || (e.metaKey && code == 86)
			|| (e.shiftKey && code == 45)) return true;

            // allow key numbers, 0 to 9
            if (((code >= 48 && code <= 57 && !e.shiftKey) || (code >= 96 && code <= 105 && !e.shiftKey) || (code == 229 && !e.shiftKey))) functional = true;

            // check Backspace, Tab, Enter, Delete, and left/right arrows
            if (code == 8) functional = true;
            if (code == 9) functional = true;
            if (code == 13) functional = true;
            if (code == 46) functional = true;
            if (code == 37) functional = true;
            if (code == 39) functional = true;

            if (!functional) {
                e.preventDefault();
                e.stopPropagation();
            }
        });

        $('.Numberic, input[data-val-exnumberic]').on('paste', function () {
            var target = jQuery(this);
            window.setTimeout(function () {
                var v = target.val();
                target.val(v.replace(/\D/g, ''));
            }, 1);
        });
    }
}

function InputModeLocker() {
	if ($('.autoCompleteDate, input[data-val-exhalfsize], .Numberic, .Decimal, input[data-val-exnumberic], .autoCompleteMoney, .autoCompleteMoneyBlank').length > 0) {
		$('.autoCompleteDate, input[data-val-exhalfsize], .Numberic, .Decimal, input[data-val-exnumberic], .autoCompleteMoney, .autoCompleteMoneyBlank').css("ime-mode", "disabled");
		
		var supportIMEMode = ('ime-mode' in document.body.style);
		if (!supportIMEMode) {

			// Alphanumeric
			$('input[data-val-exhalfsize]').inputmask("*{1,}", { placeholder: "", negationSymbol: { front: " " } });

			// Numeric
			$('.Numberic, .Decimal, input[data-vale-xnumberic]').inputmask("9{1,}", { placeholder: "", negationSymbol: { front: " " } });
		}

		$(".zip").inputmask("999-9999", { showMaskOnHover: false });
		$(".tel").inputmask("9{2,4}-9{2,4}-9999", { showMaskOnHover: false });
		$(".telJNet").inputmask("99999-99", { showMaskOnHover: false });
		$(".fax").inputmask("999-999-9999", { showMaskOnHover: false });
		$(".faxJNet").inputmask("99999-99", { showMaskOnHover: false });

		$('.autoCompleteDate, input[data-val-exhalfsize], .Numberic, input[data-val-exnumberic], .autoCompleteMoney, .autoCompleteMoneyBlank')
			//.on('blur', function () {

			//	if (supportIMEMode) return;

			//	var target = $(this);
			//	window.setTimeout(function () {
			//		var v = target.val();
			//		target.val(filterMBC(v));

			//	}, 1);
			//})
			.on('paste', function () {
				var target = jQuery(this);
				window.setTimeout(function () {
					var v = target.val();
					target.val(filterMBC(v));
				}, 1);
			});
	}
}

function filterMBC(src) {
	var str = '';
	src = escape(src);
	for (i = 0; i < src.length; i++) {
		var chr = src.charAt(i);
		if (chr == '%') {
			var nchr = src.charAt(++i);
			if (nchr == 'u') {
				i += 4;
			} else {
				str += chr
				str += nchr
				str += src.charAt(++i);
			}
			continue;
		}
		str += chr;
	}
	return unescape(str);
}

$(document).ready(function () {
	OnlyNumberKey();
	InputModeLocker();

	// create div tag for validation form
	if ($('#errorlist').length == 0) {
		$('body').append('<div id="errorlist" style="display: none"></div>');
	}

	// auto bind validation on all page
	$('form').bind('invalid-form.validate', function (form, validator) {
		//$(document).on('invalid-form.validate', 'form', function (form, validator) {
		var $list = $('<ul />');
		if (validator.errorList.length) {
			$.each(validator.errorList, function (i, entity) {
				if (entity.method !== "exrequiredgroup" || (entity.method === "exrequiredgroup" && $list.html().indexOf(entity.message) < 0)) {
					$("<li />").html(entity.message).appendTo($list);
				}
			});
			//msgboxError('Error', $('<div />').append($list));
			DisplayMsgBox("E", $list.html(), null, CloseAction);
			return false;
		}
	});
	
});

function RegistValidate(theForm) {
	$.validator.unobtrusive.parseDynamicContent(theForm);

	$('form').bind('invalid-form.validate', function (form, validator) {
		var $list = $('<ul />');
		if (validator.errorList.length) {
			$.each(validator.errorList, function (i, entity) {
				if (entity.method !== "exrequiredgroup" || (entity.method === "exrequiredgroup" && $list.html().indexOf(entity.message) < 0)) {
					$("<li />").html(entity.message).appendTo($list);
				}
			});
			//msgboxError('Error', $('<div />').append($list));
			DisplayMsgBox("E", $list.html(), null, CloseAction);
			return false;
		}
	});
}

$(document).ajaxComplete(function () {
	OnlyNumberKey();
	InputModeLocker();
});

//---------------------------------------------------------------------------------------------------------------------
// Diplay message box in case form error valid
//---------------------------------------------------------------------------------------------------------------------
function msgboxError(_title, _messageHtml) {
	$('#errorlist').html(_messageHtml)
                .dialog({
                	modal: true,
                	title: _title,
                	zIndex: 10000,
                	autoOpen: true,
                	draggable: false,
                	resizable: false,
                	width: 'auto',
                	dialogClass: "dlg-no-close",
                	buttons: {
                		"戻る": function () {
                			$(this).dialog("close");
                		}
                	}
                });
}

//---------------------------------------------------------------------------------------------------------------------
// Check form valid
//---------------------------------------------------------------------------------------------------------------------
function isFormValid() {
	return $('.input-validation-error, .glverror').length == 0;
}

//---------------------------------------------------------------------------------------------------------------------
// Reset form validator
//---------------------------------------------------------------------------------------------------------------------
function ResetValidations(formId) {
	$("#" + formId).removeData("validator");
	$("#" + formId).removeData("unobtrusiveValidation");
	$.validator.unobtrusive.parse("#" + formId);
	window.isIntervalAlertSummaryError = true;
}

//---------------------------------------------------------------------------------------------------------------------
// Fill server error
//---------------------------------------------------------------------------------------------------------------------
function FillServerError() {
	var dataValidError = getCookie("GLV_SYS_isModelStateValid_error");
	if (dataValidError != null) {
		window.eraseCookie("GLV_SYS_isModelStateValid_error");

		var dataValidErrorAray = dataValidError.split("|");
		var errorItem = "";
		var errorMsg = "";

		for (var i = 0; i < dataValidErrorAray.length; i++) {
			if (errorItem.length > 0) {
				errorItem = errorItem + ",";
			}
			if ($("[name='" + dataValidErrorAray[i].split(":")[0] + "']").length > 0) {
				if (errorItem.length == "") {
					$("[name='" + dataValidErrorAray[i].split(":")[0] + "']").focus();
				}

				errorItem = errorItem + "[name='" + dataValidErrorAray[i].split(":")[0] + "']";

				if (errorMsg.indexOf($("[name='" + dataValidErrorAray[i].split(":")[0] + "']").attr("data-val-" + dataValidErrorAray[i].split(":")[1])) < 0) {
					if (errorMsg.length > 0) {
						errorMsg = errorMsg + '\n';
					}
					errorMsg = errorMsg +
						$("[name='" + dataValidErrorAray[i].split(":")[0] + "']").attr("data-val-" + dataValidErrorAray[i].split(":")[1]);
				}
			}
			else {
				if (errorItem.length == "") {
					$("[name$='" + dataValidErrorAray[i].split(":")[0] + "']").first().focus();
				}
				errorItem = errorItem + "[name$='" + dataValidErrorAray[i].split(":")[0] + "']";

				if (errorMsg.indexOf($("[name$='." + dataValidErrorAray[i].split(":")[0] + "']").first().attr("data-val-" + dataValidErrorAray[i].split(":")[1])) < 0) {
					if (errorMsg.length > 0) {
						errorMsg = errorMsg + '\n';
					}
					errorMsg = errorMsg +
						$("[name$='." + dataValidErrorAray[i].split(":")[0] + "']").first().attr("data-val-" + dataValidErrorAray[i].split(":")[1]);
				}
			}
		}

		alert(errorMsg);
		$(errorItem).addClass("input-validation-error").change(function () {
			$(this).removeClass("input-validation-error");
		});
	}
}
setInterval("FillServerError()", 100);

//---------------------------------------------------------------------------------------------------------------------
// EXRequired
//---------------------------------------------------------------------------------------------------------------------
jQuery.validator.unobtrusive.adapters.addSingleVal("exrequired", "other", "exrequiredcheck");
jQuery.validator.addMethod("exrequiredcheck",
	function (value, element, other) {
		return checkRequire(value, element, true, other);
	}
);

function checkRequire(value, element, allowZero, allowBlank) {
	var isValid = false;

	if (allowBlank !== "allowblank")
		value = $.trim(value);

	//if (((($(element).attr("type") === "tel" || $(element).attr("type") === "text") && value == 0 && allowZero) || value !== "0") && value.length > 0)
	if (value.length > 0)
		isValid = true;

	return isValid;
}

//---------------------------------------------------------------------------------------------------------------------
// EXRequiredIf
//---------------------------------------------------------------------------------------------------------------------
jQuery.validator.addMethod('exrequiredif',
	function (value, element, parameters) {
		var prefix = element.id.split('-');
		var prefixString = prefix[0];

		var dependentControlList = parameters['dependentproperty'].split('_');

		var isRequireByRadioButton = false;
		var isRequireByDenpendent = false;

		// get the target value (as a string, 
		// as that's what actual value will be)
		var targetvalue = parameters['targetvalue'];
		targetvalue =
		  (targetvalue == null ? '' : targetvalue).toString();

		var hasRadioButton = parameters['hasradiobutton'];
		var startIndex = hasRadioButton ? 1 : 0;
		var control;
		var actualvalue;
		var id;
		for (var i = startIndex; i < dependentControlList.length; i++) {
			id = "#" + prefixString + '-' + dependentControlList[i]; // get the actual value of the target control
			// note - this probably needs to cater for more 
			// control types, e.g. radios
			control = $(id);
			var controltype = control.attr('type');
			actualvalue = control.val();
			if (actualvalue != "" && actualvalue != 0 && actualvalue != undefined) {
				isRequireByDenpendent = true;
				break;
			}
		}

		if (hasRadioButton) {
			id = "#" + prefixString + '-' + dependentControlList[0];
			control = $(id);
			actualvalue = control.val();
			if (actualvalue.toLowerCase() == "true") {
				isRequireByRadioButton = true;
			}
		}

		// if the condition is true, reuse the existing 
		// required field validator functionality
		if ((!hasRadioButton && isRequireByDenpendent) || (hasRadioButton && isRequireByDenpendent && isRequireByRadioButton)) {
			return checkRequire(value, element, true);
		}

		return true;
	}
);

jQuery.validator.unobtrusive.adapters.add(
	'exrequiredif',
	['dependentproperty', 'targetvalue', 'hasradiobutton'],
	function (options) {
		options.rules['exrequiredif'] = {
			dependentproperty: options.params['dependentproperty'],
			targetvalue: options.params['targetvalue'],
			hasradiobutton: options.params['hasradiobutton'],
		};
		options.messages['exrequiredif'] = options.message;
	}
);

//---------------------------------------------------------------------------------------------------------------------
// exrequiredgroup
//---------------------------------------------------------------------------------------------------------------------
jQuery.validator.addMethod('exrequiredgroup', function (value, element, params) {
	var properties = params.propertynames.split(',');
	var isValid = false;
	for (var i = 0; i < properties.length; i++) {

		var id = "#" + properties[i];
		var control = $(id);
		var controltype = control.attr('type');
		var actualvalue = control.val();

		if ((controltype !== "radio" && actualvalue != "") || (controltype === "radio" && control.is(':checked'))) {
			isValid = true;
			break;
		}
	}
	return isValid;
}, '');

jQuery.validator.unobtrusive.adapters.add(
	'exrequiredgroup',
    ['propertynames'],
    function (options) {
    	options.rules['exrequiredgroup'] = options.params;
    	options.messages['exrequiredgroup'] = options.message;
    }
);

//---------------------------------------------------------------------------------------------------------------------
// EXRequiredIfConstraint
//---------------------------------------------------------------------------------------------------------------------
jQuery.validator.addMethod('exrequiredifconstraint',
	function (value, element, parameters) {

		var id = 'input[name="' + parameters['dependentproperty'] + '"]:checked';

		// get the target value (as a string, 
		// as that's what actual value will be)
		var targetvalue = parameters['targetvalue'];
		targetvalue =
			(targetvalue == null ? '' : targetvalue).toString();

		// get the actual value of the target control
		// note - this probably needs to cater for more 
		// control types, e.g. radios
		var control = $(id);

		if (control.length < 1) {
			control = $('select[name="' + parameters['dependentproperty'] + '"]');
		}

		var controltype = control.attr('type');
		var actualvalue =
				controltype === 'checkbox' ?
				control.attr('checked').toString() :
				control.val();

		// if the condition is true, reuse the existing 
		// required field validator functionality
		if (targetvalue === actualvalue)
			return jQuery.validator.methods.exrequiredcheck.call(
					this, value, element, parameters);

		return true;
	}
);

jQuery.validator.unobtrusive.adapters.add(
	'exrequiredifconstraint',
	['dependentproperty', 'targetvalue'],
	function (options) {
		options.rules['exrequiredifconstraint'] = {
			dependentproperty: options.params['dependentproperty'],
			targetvalue: options.params['targetvalue']
		};
		options.messages['exrequiredifconstraint'] = options.message;
	}
);

//---------------------------------------------------------------------------------------------------------------------
// EXStringLength
//---------------------------------------------------------------------------------------------------------------------
$.validator.unobtrusive.adapters.add("exstringlength", ["minlength", "maxlength", "allowblank"], function (options) {
	options.rules["exstringlengthcheck"] = 'exstringlengthcheck';
	if (options.message) options.messages["exstringlengthcheck"] = options.message;
});
jQuery.validator.addMethod("exstringlengthcheck",
	function (value, element, maxlength) {
		var max = parseInt($(element).attr('data-val-exstringlength-maxlength'));
		var min = parseInt($(element).attr('data-val-exstringlength-minlength'));
		var allowBlank = parseInt($(element).attr('data-val-exstringlength-allowblank'));
		var length = this.getLength($.trim(value), element);
		return ((length >= min && length <= max) || (allowBlank == 1 && length == 0));
	}
);

//---------------------------------------------------------------------------------------------------------------------
// EXRegex
//---------------------------------------------------------------------------------------------------------------------
jQuery.validator.unobtrusive.adapters.addSingleVal("exregex", "regex", "exregexcheck");
jQuery.validator.addMethod("exregexcheck",
	function (value, element, params) {
		if (IsEmpty(value))
			return true;
		return new RegExp($(element).attr("data-val-exregex-regex")).test(value);
	}
);

//---------------------------------------------------------------------------------------------------------------------
// EXRegexNumeric
//---------------------------------------------------------------------------------------------------------------------
jQuery.validator.unobtrusive.adapters.addSingleVal("exnumberic", "regexnumberic", "exnumbericcheck");
jQuery.validator.addMethod("exnumbericcheck",
	function (value, element, params) {
		if (value === "") {
			return true;
		}
		return new RegExp($(element).attr("data-val-exnumberic-regexnumberic")).test(value);
	}
);

//---------------------------------------------------------------------------------------------------------------------
// EXCompare
//---------------------------------------------------------------------------------------------------------------------
jQuery.validator.unobtrusive.adapters.addSingleVal("excompare", "fromobjectname", "excomparecheck");
jQuery.validator.addMethod("excomparecheck",
	function (value, element, fromobjectname) {
		var fromObject = $("input[name='" + fromobjectname + "']");
		if (value == null || value.length == 0 || $(fromObject).val().length == 0) {
			return true;
		}
		if (isInteger(value) && isInteger($(fromObject).val())) {
			if (parseInt(value.replace(/\//g, '').replace(/-/g, '')) < parseInt($(fromObject).val().replace(/\//g, '').replace(/-/g, ''))) {
				return false;
			}
		} else if (isFloat(value) && isFloat($(fromObject).val())) {
			if (parseFloat(value.replace(/\//g, '').replace(/-/g, '')) < parseFloat($(fromObject).val().replace(/\//g, '').replace(/-/g, ''))) {
				return false;
			}
		}
		return true;
	}
);

//---------------------------------------------------------------------------------------------------------------------
// EXRegexAlphaNumeric
//---------------------------------------------------------------------------------------------------------------------
jQuery.validator.unobtrusive.adapters.addSingleVal("exalphanumberic", "regexalphanumberic", "exalphacheck");
jQuery.validator.addMethod("exalphacheck",
	function (value, element, params) {
		return new RegExp($(element).attr("data-val-exalphanumberic-regexalphanumberic")).test(value);
	}
);

//---------------------------------------------------------------------------------------------------------------------
// EXRange
//---------------------------------------------------------------------------------------------------------------------
jQuery.validator.unobtrusive.adapters.addSingleVal("exrange", "max", "exrangecheck");
jQuery.validator.addMethod("exrangecheck",
	function (value, element, other) {
		var max = parseInt($(element).attr('data-val-exrange-max'));
		var min = parseInt($(element).attr('data-val-exrange-min'));
		return (value >= min && value <= max);
	}
);

//---------------------------------------------------------------------------------------------------------------------
// EXEmail
//---------------------------------------------------------------------------------------------------------------------
jQuery.validator.unobtrusive.adapters.addSingleVal("exemail", "regexemail", "exemailcheck");
jQuery.validator.addMethod("exemailcheck",
	function (value, element, regexemail) {
		return new RegExp($(element).attr("data-val-exemail-regexemail")).test(value);
	}
);

//---------------------------------------------------------------------------------------------------------------------
// EXFullSize
//---------------------------------------------------------------------------------------------------------------------
jQuery.validator.unobtrusive.adapters.addSingleVal("exfullsize", "other", "exfullsizecheck");
jQuery.validator.addMethod("exfullsizecheck",
	function (value, element, other) {
		return true;
	}
);

//---------------------------------------------------------------------------------------------------------------------
// EXHalfSize
//---------------------------------------------------------------------------------------------------------------------
jQuery.validator.unobtrusive.adapters.addSingleVal("exhalfsize", "other", "exhalfsizecheck");
jQuery.validator.addMethod("exhalfsizecheck",
	function (value, element, other) {
		return true;
	}
);

//---------------------------------------------------------------------------------------------------------------------
// EXCompareDateForm
//---------------------------------------------------------------------------------------------------------------------
jQuery.validator.unobtrusive.adapters.addSingleVal("excomparedateform", "fromdatename", "excomparedateformcheck");
jQuery.validator.addMethod("excomparedateformcheck",
	function (value, element, fromdatename) {
		//if (value == null || value.length == 0 || isNaN($(fromdatename)) || $(fromdatename).val().length == 0) {
		//	return true;
		//}
		//if (parseInt(value.replace(/\//g, '').replace(/-/g, '')) < parseInt($(fromdatename).val().replace(/\//g, '').replace(/-/g, ''))) {
		//	return false;
		//}
		//return true;

		var fromDate = $("input[name='" + fromdatename + "']");
		if (value == null || value.length == 0 || $(fromDate).val().length == 0) {
			return true;
		}
		if (parseInt(value.replace(/\//g, '').replace(/-/g, '')) < parseInt($(fromDate).val().replace(/\//g, '').replace(/-/g, ''))) {
			return false;
		}
		return true;
	}
);

//---------------------------------------------------------------------------------------------------------------------
// EXDate
//---------------------------------------------------------------------------------------------------------------------
jQuery.validator.unobtrusive.adapters.addSingleVal("exdate", "other", "exdatecheck");
jQuery.validator.addMethod("exdatecheck",
	function (value, element, other) {
		value = value.replace(/^\s+|\s+$/g, '');
		if (value == null || value.length == 0) {
			return true;
		}
		var parts = value.split("/");
		var dd = parseInt(parts[2], 10);
		if (isNaN(dd) && element.classList.contains("autoCompleteYearMonth")) dd = 01;
		var mm = parseInt(parts[1], 10);
		var yyyy = parseInt(parts[0], 10);

		return CheckDate(yyyy, mm, dd);
	}
);

// +++++++++++++++++++++++++++++++++++++++++
//---------------------------------------------------------------------------------------------------------------------
// Manually support
//---------------------------------------------------------------------------------------------------------------------
// +++++++++++++++++++++++++++++++++++++++++

var hasClearOldError = true;
var hasFocusToErrorElement = true;

$.fn.ClearError = function () {
	$(this).unbind(".glv_clear_error");
	$(this).removeClass("glverror");
}

$.fn.MarkError = function (isSuccess) {
	if (!isSuccess) {
		if (hasFocusToErrorElement) $(this).focus();
		$(this).addClass("glverror");
		$(this).bind("keyup.glv_clear_error change.glv_clear_error", function () {
			$(this).ClearError();
		});
	}
	else {
		if (hasClearOldError) {
			$(this).removeClass("glverror");
			//alert("clear else" + $(this).attr("id"));
		}
	}
}

//---------------------------------------------------------------------------------------------------------------------
// required
//---------------------------------------------------------------------------------------------------------------------
function isNotEmpty(id) {
	var value = $("#" + id).val();
	var result = $.trim(value).length > 0;
	$("#" + id).MarkError(result);
	return result;
}

//---------------------------------------------------------------------------------------------------------------------
// Minlength
//---------------------------------------------------------------------------------------------------------------------
function isMinLengthValid(id, minlenth) {
	var value = $("#" + id).val();
	var result = $.trim(value).length >= minlenth;
	$("#" + id).MarkError(result);
	return result;
}

//---------------------------------------------------------------------------------------------------------------------
// Maxlength
//---------------------------------------------------------------------------------------------------------------------
function isMaxLengthValid(id, maxlenth) {
	var value = $("#" + id).val();
	var result = $.trim(value).length <= maxlenth;
	$("#" + id).MarkError(result);
	return result;
}

//---------------------------------------------------------------------------------------------------------------------
// MinMaxlength
//---------------------------------------------------------------------------------------------------------------------
function isMinMaxLengthValid(id, minlenth, maxlenth) {
	var value = $("#" + id).val();
	var result = ($.trim(value).length <= maxlenth && $.trim(value).length >= minlenth);
	$("#" + id).MarkError(result);
	return result;
}

//---------------------------------------------------------------------------------------------------------------------
// Range
//---------------------------------------------------------------------------------------------------------------------
function isRangeValid(id, minValue, maxValue) {
	var value = $("#" + id).val();
	var result = ($.trim(value).length <= maxValue && $.trim(value).length >= minValue);
	$("#" + id).MarkError(result);
	return result;
}

//---------------------------------------------------------------------------------------------------------------------
// Compare date
//---------------------------------------------------------------------------------------------------------------------
function isDateFormDateToValid(fromId, toId) {
	var fromDate = $("#" + fromId).val().replace(/\//g, '').replace(/-/g, '');
	var toDate = $("#" + toId).val().replace(/\//g, '').replace(/-/g, '');
	var result = fromDate.length == 0 || toDate.length == 0 || fromDate <= toDate;
	$("#" + fromId).MarkError(result);
	$("#" + toId).MarkError(result);
	return result;
}

//---------------------------------------------------------------------------------------------------------------------
// check a string valid by a regex
//---------------------------------------------------------------------------------------------------------------------
function isRegexValid(id, regex) {
	alert(id);
	var result = new RegExp(regex).test($("#" + id).val());
	$("#" + id).MarkError(result);
	return result;
}

//---------------------------------------------------------------------------------------------------------------------
// check valid email
//---------------------------------------------------------------------------------------------------------------------
function isEmailValid(id) {
	var regexEmail = /^$|((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))$/;
	return isRegexValid(id, regexEmail);
}

//---------------------------------------------------------------------------------------------------------------------
// check valid Fax
//---------------------------------------------------------------------------------------------------------------------
function isFaxValid(id) {
	var regexPayFaxNo = /(^[0]{1}[0-9-]*$)|(^$)/;
	return isRegexValid(id, regexPayFaxNo);
}

//---------------------------------------------------------------------------------------------------------------------
// check valid Tel No
//---------------------------------------------------------------------------------------------------------------------
function isTelNoValid(id) {
	var regexPayTelNo = /^[0-9-]*$/;
	return isRegexValid(id, regexPayTelNo);
}

//---------------------------------------------------------------------------------------------------------------------
// check valid Numberic
//---------------------------------------------------------------------------------------------------------------------
function isNumbericValid(id) {
	var regexNumberic = /^[0-9]*$/;
	return isRegexValid(id, regexNumberic);
}

//---------------------------------------------------------------------------------------------------------------------
// check valid Alphal Numberic
//---------------------------------------------------------------------------------------------------------------------
function isAlphaNumbericValid(id) {
	var regexAlphaNumberic = /^[a-zA-Z0-9]*$/;
	return isRegexValid(id, regexAlphaNumberic);
}

//---------------------------------------------------------------------------------------------------------------------
// check valid PayZipNo3Figure
//---------------------------------------------------------------------------------------------------------------------
function isPayZipNo3FigureValid(id) {
	var regexPayZipNo3Figure = /^$|\d{3}/;
	return isRegexValid(id, regexPayZipNo3Figure);
}

//---------------------------------------------------------------------------------------------------------------------
// check valid PayZipNo4Figure
//---------------------------------------------------------------------------------------------------------------------
function isPayZipNo4FigureValid(id) {
	var regexPayZipNo4Figure = /^$|\d{4}/;
	return isRegexValid(id, regexPayZipNo4Figure);
}

//---------------------------------------------------------------------------------------------------------------------
// check max character number in a string
//---------------------------------------------------------------------------------------------------------------------
function checkMaxLenghtDigital(id, maxlenght) {
	var replaceDigital = $("#" + id).val().replace(/-/g, "");
	var result = replaceDigital.length <= maxlenght;
	$("#" + id).MarkError(result);
	return result;
}

//---------------------------------------------------------------------------------------------------------------------
// check valid value is Integer
//---------------------------------------------------------------------------------------------------------------------
function isInteger(x) {
	return x % 1 === 0;
	 //return Math.floor(x) === x;
}

//---------------------------------------------------------------------------------------------------------------------
// check valid value is Float
//---------------------------------------------------------------------------------------------------------------------
function isFloat(x) {
	//return !!(x % 1);
	return n === Number(n) && n % 1 !== 0;
}

(function ($) {
	$.validator.unobtrusive.parseDynamicContent = function (selector) {
		//use the normal unobstrusive.parse method
		//$.validator.unobtrusive.parse(selector); changed this line with
		$(selector).find('*[data-val = true]').each(function () {

			$.validator.unobtrusive.parseElement(this, false);
		});

		//get the relevant form
		var form = $(selector).first().closest('form');

		//get the collections of unobstrusive validators, and jquery validators
		//and compare the two
		var unobtrusiveValidation = form.data('unobtrusiveValidation');
		var validator = form.validate();

		if (typeof (unobtrusiveValidation) != 'undefined') {
		$.each(unobtrusiveValidation.options.rules, function (elname, elrules) {
			if (validator.settings.rules[elname] == undefined) {
				var args = {};
				$.extend(args, elrules);
				args.messages = unobtrusiveValidation.options.messages[elname];
				$('[name="' + elname + '"]').rules("add", args);
			} else {
				$.each(elrules, function (rulename, data) {
					if (validator.settings.rules[elname][rulename] == undefined) {
						var args = {};
						args[rulename] = data;
						args.messages = unobtrusiveValidation.options.messages[elname][rulename];
						$('[name="' + elname + '"]').rules("add", args);
					}
				});
			}
		});
	}
	}
})($);