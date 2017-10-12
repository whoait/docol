$(document).ready(function () {
	InitPaging();
	InitSorting();
});

var SORT_DIRECTION_ASC = "0";
var SORT_DIRECTION_DESC = "1";

function InitSorting() {
	var sortItem = $("#SortItem").val();
	var sortDirection = $("#SortDirection").val();

	if (sortItem == null && sortDirection == null) {
		$("input[id*=_SortItem]").each(function (index) {
			var sortArea = $(this).attr("name").split('_')[0];
			var sortItem = $(this).val();
			var sortDirection = $("#" + sortArea + "_SortDirection").val();

			changeSortDirection(sortDirection, sortItem, sortArea);
		});
	}

	$(document).off("click", ".title_line");
	$(document).on("click", ".title_line", function (evt) {
		var sortfield = $(evt.target).data("sortfield");
		var sortarea = $(evt.target).data("sortarea");
		var prefix = sortarea == null ? "" : sortarea + "_";

		if ($("#" + prefix + "SortItem").val() == sortfield) {
			if ($("#" + prefix + "SortDirection").val() == SORT_DIRECTION_ASC) {
				$("#" + prefix + "SortDirection").val(SORT_DIRECTION_DESC);
			}
			else {
				$("#" + prefix + "SortDirection").val(SORT_DIRECTION_ASC);
			}
		}
		else {
			$("#" + prefix + "SortItem").val(sortfield);
			$("#" + prefix + "SortDirection").val(SORT_DIRECTION_ASC);
		}
		$("#PageIndex").val(1);
		PostData(prefix);

		changeSortDirection($("#" + prefix + "SortDirection").val(), sortfield, sortarea);

		evt.preventDefault();
	});

	if (sortItem == "") {
		return;
	}

	if (sortItem != null && sortDirection != null) {
		// Change sort icon
		if (sortDirection == SORT_DIRECTION_ASC) {
			$('[data-sortfield=' + sortItem + ']').addClass("button-arrow-asc");
		}
		else {
			$('[data-sortfield=' + sortItem + ']').addClass("button-arrow-desc");
		}
	}
}

function changeSortDirection(sortDirection, sortItem, sortArea) {
	var SORT_DIRECTION_ASC = "0";
	var SORT_DIRECTION_DESC = "1";

	// Change sort icon
	if (sortArea !== null && sortArea !== '') {

		$("[data-sortarea=" + sortArea + "]").removeClass("button-arrow-desc");
		$("[data-sortarea=" + sortArea + "]").removeClass("button-arrow-asc");

		if (sortDirection === SORT_DIRECTION_ASC) {
			$('[data-sortarea="' + sortArea + '"][data-sortfield=' + sortItem + ']').addClass("button-arrow-asc");
		}
		else {
			$('[data-sortarea="' + sortArea + '"][data-sortfield=' + sortItem + ']').addClass("button-arrow-desc");
		}

		return;
	}

	$("[data-sortfield]").removeClass("button-arrow-desc");
	$("[data-sortfield]").removeClass("button-arrow-asc");
	if (sortDirection == SORT_DIRECTION_ASC) {
		$('[data-sortfield=' + sortItem + ']').addClass("button-arrow-asc");
	}
	else {
		$('[data-sortfield=' + sortItem + ']').addClass("button-arrow-desc");
	}
}

function InitPaging() {
	$(".paging").click(function (evt) {
		var pageIndex = $(evt.target).data("pageindex");
		if (pageIndex == "")
			return;
		$("#PageIndex").val(pageIndex);
		PostData();
		evt.preventDefault();
	});

	$('.id1>div>div:nth-child(2)').removeAttr("style").css("padding", "10px 0px");
	$('.id2>div>div:nth-child(2)').removeAttr("style").css("padding", "5px 0px");
}

function PostData(prefix) {

	prefix = prefix == null ? "" : prefix;

	var action = $("#" + prefix + "Action").val();
	var displayArea = $("#" + prefix + "DisplayArea").val();
	var dataObject = JSON.stringify({
		//'SortItem': $("#" + prefix + "SortItem").val(),
		//'SortDirection': $("#" + prefix + "SortDirection").val(),
		'PageSize': $("#" + prefix + "PageSize").val(),
		'PageIndex': $("#" + prefix + "PageIndex").val(),
		'PageBegin': $("#" + prefix + "PageBegin").val(),
		'PageEnd': $("#" + prefix + "PageEnd").val(),
		'hfldUniqueTabSession': sessionStorage.getItem('UniqueTabSession')
	});

	$.ajax({
		url: action,
		type: 'POST',
		contentType: 'application/json',
		data: dataObject,
		success: function (response) {
			$("#" + displayArea).html("");
			$("#" + displayArea).html(response);

			//var sortItem = $("#" + prefix + "SortItem").val();
			//var sortDirection = $("#" + prefix + "SortDirection").val();
			//changeSortDirection(sortDirection, sortItem, prefix.replace('_', ''));
		},
		error: function (e) {
			console.log("Error " + e);
		}
	});

}