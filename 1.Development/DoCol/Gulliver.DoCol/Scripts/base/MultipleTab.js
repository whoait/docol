$(function () {
	var oldTabId = getParameterByName("tabId");
	var tabId = sessionStorage.getItem('UniqueTabSession');
	//if (tabId == null || tabId !== oldTabId) {
	if (tabId == null) {
		$.get("/DCW/DCW001/GetTabId?oldTabId=" + oldTabId, function (data) {
			tabId = data.tabId;
			console.log(tabId);
			sessionStorage.setItem('UniqueTabSession', tabId);

			var my_form = $('FORM');

			// insert hidden ID value
			var my_val = document.createElement('INPUT');
			my_val.type = 'hidden';
			my_val.name = 'hfldUniqueTabSession';
			my_val.value = tabId;
			my_form.append(my_val);
			//location.hash = "tabId=" + tabId;

			var newURL = updateURLParameter(window.location.href, 'tabId', tabId);
			window.history.pushState("", "", newURL);
		});
	} else {
		var my_form = $('FORM');

		// insert hidden ID value
		var my_val = document.createElement('INPUT');
		my_val.type = 'hidden';
		my_val.name = 'hfldUniqueTabSession';
		my_val.value = tabId;
		my_form.append(my_val);
	}
});

function SetTabId() {
	var tabId = sessionStorage.getItem('UniqueTabSession');
	setTabIdToActionLink(tabId);
};

function GetTabId() {
	return sessionStorage.getItem('UniqueTabSession');
};

function setTabIdToActionLink(tabId) {
	$("a").each(function () {
		this.href = updateURLParameter(this.href, 'tabId', tabId);
	});
	$("#btnCallbackConfirm").attr("href", "#");
	$('a[data-toggle="modal"]').attr("href", "#");
	$('#link-toppage').attr("href", updateURLParameter($('#link-toppage').attr("href"), 'tabId', tabId));
	$('.no-tabid').attr("href", "#");
	$('.ui-corner-all').attr("href", "#");
	$('.no-href').removeAttr("href");
}

function getParameterByName(name) {
	name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
	var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
        results = regex.exec(location.search);
	return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
}

function updateURLParameter(url, param, paramVal) {

	if (paramVal == null || url == null)
		return url;

	var TheAnchor = null;
	var newAdditionalURL = "";
	var tempArray = url.split("?");
	var baseURL = tempArray[0];
	var additionalURL = tempArray[1];
	var temp = "";

	if (additionalURL) {
		var tmpAnchor = additionalURL.split("#");
		var TheParams = tmpAnchor[0];
		TheAnchor = tmpAnchor[1];
		if (TheAnchor)
			additionalURL = TheParams;

		tempArray = additionalURL.split("&");

		for (i = 0; i < tempArray.length; i++) {
			if (tempArray[i].split('=')[0] != param) {
				newAdditionalURL += temp + tempArray[i];
				temp = "&";
			}
		}
	}
	else {
		var tmpAnchor = baseURL.split("#");
		var TheParams = tmpAnchor[0];
		TheAnchor = tmpAnchor[1];

		if (TheParams)
			baseURL = TheParams;
	}

	if (TheAnchor)
		paramVal += "#" + TheAnchor;

	var rows_txt = temp + "" + param + "=" + paramVal;
	return baseURL + "?" + newAdditionalURL + rows_txt;
}