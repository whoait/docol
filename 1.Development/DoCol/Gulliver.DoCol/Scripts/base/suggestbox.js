var urlService = '../../Library/Suggestbox/Suggestbox.ashx';

function SuggestBoxShopCd(data) {
	// Default value of Previous value
	var preDisp = "";
	var preValue = "";
	if (data.length != 0) {
		// Split string input.
		var arr = data.split(',');

		// Get parent object
		var parentElement = "#" + arr[0];
		var hidParentElement = "#" + arr[1];
		var funcName = "CmnShopCd";
		preDisp = $(parentElement).val();
		preValue = $(hidParentElement).val();
		return $(parentElement).autocomplete({
			source: function (request, response) {
				$.ajax({
					url: urlService,
					data: { model: funcName, key: request.term},
					success: function (dataResult) {
						try {
							data = JSON.parse(dataResult);
							response($.map(data, function (item) {
								return {
									label: item.FieldDisplay,
									display: item.FieldCode,
									value: item.FieldName
								};
							}));
						} catch (e) {
							//console.log(e.message);
						}
					},
					error: function () {
						//alert(textStatus);
					}
				});
			},
			focus: function (event, ui) {
				event.preventDefault();
				$(this).val(ui.item.display);
			},
			select: function (event, ui) {
				// If has item select
				if (ui.item) {
					// Set this value = curent display
					this.value = ui.item.display;

					// Set hidden value = curent value
					$(hidParentElement).val(ui.item.value);

					// Save data to previous variable
					preDisp = ui.item.display;
					preValue = ui.item.value;

					// Return event
					event.preventDefault();
				}
			},
			change: function (e, ui) {
				// If not select sugesst value and value != empty
				if (ui.item == null && $(this).val() != '') {
					// Restore value
					this.value = preDisp;
					$(hidParentElement).val(preValue);
				} else // If not select sugesst value and value is empty
					if (ui.item == null && $(this).val() == '') {
						// Empty value
						$(this).val('').attr('value', '');
						$(hidParentElement).val('').attr('value', '');

						// Set value of previous variable is blank
						preDisp = "";
						preValue = "";
					}
			}
		}).focusout(function () {
			// If focusout this value != previous value
			if ($(this).val() != '' && $(this).val() != preDisp) {
				// Restore value
				this.value = preDisp;
				$(hidParentElement).val(preValue);
			} else if ($(this).val() == '') {
				// Empty value
				$(this).val('').attr('value', '');
				$(hidParentElement).val('').attr('value', '');

				// Set value of previous variable is blank
				preDisp = "";
				preValue = "";
			}
		});
	}
}

function SuggestBoxShopName(data) {
	// Default value of Previous value
	var preDisp = "";
	var preValue = "";

	if (data.length != 0) {
		// Split string input.
		var arr = data.split(',');

		// Get parent object
		var parentElement = "#" + arr[0];
		var hidParentElement = "#" + arr[1];
		var funcName = "CmnShopName";
		preDisp = $(parentElement).val();
		preValue = $(hidParentElement).val();
		return $(parentElement).autocomplete({
			source: function (request, response) {
				$.ajax({
					url: urlService,
					data: { model: funcName, key: request.term },
					success: function (dataResult) {
						try {
							data = JSON.parse(dataResult);
							response($.map(data, function (item) {
								return {
									label: item.FieldDisplay,
									display: item.FieldName,
									value: item.FieldCode
								};
							}));
						} catch (e) {
							//console.log(e.message);
						}
					},
					error: function () {
						//alert(textStatus);
					}
				});
			},
			focus: function (event, ui) {
				event.preventDefault();
				$(this).val(ui.item.display);
			},
			select: function (event, ui) {
				// If has item select
				if (ui.item) {
					// Set this value = curent display
					this.value = ui.item.display;

					// Set hidden value = curent value
					$(hidParentElement).val(ui.item.value);

					// Save data to previous variable
					preDisp = ui.item.display;
					preValue = ui.item.value;

					// Return event
					event.preventDefault();
				}
			},
			change: function (e, ui) {
				// If not select sugesst value and value != empty
				if (ui.item == null && $(this).val() != '') {
					// Restore value
					this.value = preDisp;
					$(hidParentElement).val(preValue);
				} else // If not select sugesst value and value is empty
					if (ui.item == null && $(this).val() == '') {
						// Empty value
						$(this).val('').attr('value', '');
						$(hidParentElement).val('').attr('value', '');

						// Set value of previous variable is blank
						preDisp = "";
						preValue = "";
					}
			}
		}).focusout(function () {
			// If focusout this value != previous value
			if ($(this).val() != '' && $(this).val() != preDisp) {
				// Restore value
				this.value = preDisp;
				$(hidParentElement).val(preValue);
			} else if ($(this).val() == '') {
				// Empty value
				$(this).val('').attr('value', '');
				$(hidParentElement).val('').attr('value', '');

				// Set value of previous variable is blank
				preDisp = "";
				preValue = "";
			}
		});
	}
}