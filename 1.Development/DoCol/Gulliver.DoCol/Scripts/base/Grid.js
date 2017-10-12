(function () {
	Grid = function (name, minRow, rowPrefix, rowBlank, messageType, messageContent, needValidate) {
		this.name = name;
		this.minRow = minRow;
		this.messageType = messageType;
		this.messageContent = messageContent;

		if (rowPrefix == null || rowPrefix == undefined) {
			rowPrefix = "rowIndex";
		}
		this.rowPrefix = rowPrefix;

		if (rowBlank == null || rowBlank == undefined) {
			rowBlank = "rowBlank";
		}
		this.rowBlank = rowBlank;

		if (needValidate == null || needValidate == undefined) {
			needValidate = true;
		}
		this.needValidate = needValidate;

		var tableName = name;
		var rowPrefix = rowPrefix;

		// Line click event
		$(document).on("click", this.name + " .tbl_item", function () {
			// get index of row
			var rowIndex = parseInt($(this).parent().find("div:first").html().trim())

			if (rowIndex > 0)
				selectRow(tableName, rowPrefix, rowIndex);
		});

		// event check all record
		$(document).on("click", this.name + " .Heading input[type=checkbox]", function (evt) {
			selectAll(tableName, rowPrefix);
		});

		//How to navigate through table cells with tab key using JQuery?
		var tr = $(this.name).find('.tbl_row');
		tr.on('focusin', function (event) {
			var status = $(tableName + " .Heading input[type=checkbox]").is(':checked');
			if (status == true)
				return;

			tr.removeClass('row-highlight');
			var tds = $(this).addClass('row-highlight').find('.tbl_item');
		});
	};

	var selectRow = function (tableName, rowPrefix, rowIndex) {
		var checkBoxAll = $(tableName + " .Heading input[type=checkbox]");
		var status = checkBoxAll.is(':checked');
		if (status == true) {
			checkBoxAll.prop("checked", false);
		}

		// remove class highlight of all row
		$(tableName + ' .tbl_row').removeClass('row-highlight');

		// add class high light to row index
		$("#" + rowPrefix + rowIndex).addClass('row-highlight');
	}

	var selectAll = function (tableName, rowPrefix) {
		var status = $(tableName + " .Heading input[type=checkbox]").is(':checked');
		if (status == true) {
			// add class highlight if checkbox is checked
			$(tableName + " div[id*=" + rowPrefix + "]").addClass('row-highlight');
		}
		else {
			// add class highlight if checkbox is unchecked
			$(tableName + " div[id*=" + rowPrefix + "]").removeClass('row-highlight');
		}
	}

	Grid.prototype.InsertRow = function (index) {
		var itemClone = $("#" + this.rowBlank).clone().removeClass('hidden');

		//get row index
		var itemSelectedIndex = this.GetSelectedIndex();
		var itemSelected;
		var nextIndex;
		var hasTableContent = false;

		if (index != null) {
			itemSelectedIndex = index;
		}

		// check row index is exist
		// if don't exist
		if (itemSelectedIndex < 0) {
			// insert row to last row of table
			var maxRow = $("div[id*=" + this.rowPrefix + "]").length;
			nextIndex = maxRow + 1;
		}
			// row selected is exist
		else
			nextIndex = itemSelectedIndex + 1;

		itemSelected = $("#" + this.rowPrefix + (nextIndex - 1));

		if (itemSelected.length > 0) {
			// insert row blank to next row of row selected
			itemClone.insertAfter(itemSelected)
			.attr({
				'id': this.rowPrefix + nextIndex
			});
		} else {
			if ($(this.name).find(".tbl_content").length > 0) {
				itemClone.insertBefore($(this.name).find("#" + this.rowBlank))
				.attr({
					'id': this.rowPrefix + nextIndex
				});
			} else {
				itemClone.insertAfter($(this.name).find(".Heading"))
				.attr({
					'id': this.rowPrefix + nextIndex
				});
			}
		}

		this.updatedLine(nextIndex);

		if (this.needValidate)
			$.validator.unobtrusive.parseDynamicContent('#' + this.rowPrefix + nextIndex);
	};

	Grid.prototype.AddRow = function (index) {
		var status = $(this.name + " .Heading input[type=checkbox]").is(':checked');
		 
		// check status checked of checkbox all
		// if checkbox all is checked
		if (status) {
			return false;
		}

		this.InsertRow(index);

		// Select new row.
		var itemSelectedIndex = this.GetSelectedIndex();
		selectRow(this.name, this.rowPrefix, itemSelectedIndex + 1);

		return true;
	};

	Grid.prototype.GetSelectedIndex = function () {
		var selectedIndex = -1;
		if ($(this.name + " .row-highlight").length !== 0) {
			selectedIndex = parseInt($(this.name + " .row-highlight").find("div:first").html().trim()) || -1;
		}

		return selectedIndex;
	};

	Grid.prototype.RemoveRow = function (selectedIndex) {
		var totalRow;
		// get row selected
		var itemSelected = $("#" + this.rowPrefix + selectedIndex);

		if (selectedIndex < 0) {
			// Display messages
			ShowMsgBox(this.messageType, this.messageContent);
			return false;
		}

		itemSelected.remove();

		//Updated row index
		this.updatedLine(selectedIndex);

		totalRow = this.TotalRow();
		if (totalRow < this.minRow) {
			this.InsertRow(totalRow);
		}

		//hightlight next row
		selectedIndex = selectedIndex > totalRow ? selectedIndex - 1 : selectedIndex;
		selectRow(this.name, this.rowPrefix, selectedIndex);

		return true;
	}

	Grid.prototype.ClearRow = function () {
		$(".tbl_row").remove("div[id*="+ this.rowPrefix +"]");
		for (var i = 0; i < this.minRow; i++) {
			this.InsertRow();
		}
	}

	Grid.prototype.DeleteRow = function () {
		var status = $(this.name + " .Heading input[type=checkbox]").is(':checked');

		// check status checked of checkbox all
		// if checkbox all is checked
		if (status) {
			this.ClearRow();
			selectAll(this.name, this.rowPrefix);

			// Set the first row is selected.
			selectRow(this.name, this.rowPrefix, 1);
			return true;
		}
		else {
			return this.RemoveRow(this.GetSelectedIndex());
		}
	}

	Grid.prototype.TotalRow = function () {
		var rowMax = $(this.name + " .tbl_row").length - 2;
		return rowMax;
	}

	Grid.prototype.IsCheckAll = function () {
		return $(this.name + " .Heading input[type=checkbox]").is(':checked')
	}

	// update status line
	Grid.prototype.updatedLine = function (rIndex) {
		var tableName = this.name;
		var rowPrefix = this.rowPrefix;
		var rowBlank = this.rowBlank;
		$(tableName + ' .tbl_row').each(function (index, element) {
			if (index >= rIndex) {
				//get id of row
				var id = $(element).attr('id');

				// check row not blank
				if (id != rowBlank) {
					// reset id to row
					$(element).attr('id', rowPrefix + index);

					//get text of first element of row
					$(element).children().first().text(index);

					// set name, id to children element in row
					$(element).find('input[type=text], input[type=hidden], input[type=tel], select').each(function (i, item) {
						// reset name to control
						name = $(item).attr('name');
						var value = index - 1;
						if(name.match(/\[\d+\]/)){
							name = name.replace(/\[\d+\]/, "[" + value + "]");
						}else{
							name = name.replace(/\w+\./, "[" + value + "].");
						}
						$(item).attr('name', name);

						// reset id to control
						if($(item).attr('id') != null){
							id = $(item).attr('id');
							var arrayString = id.split('-');
							id = (index - 1) + "-" + arrayString[1];
							$(item).attr('id', id);
						}
					});

					// set name, id to radio element in row
					$(element).find('input[type=radio]').each(function (i, item) {

						// set name
						name = $(item).attr('name');
						var arrayString = name.split('.');
						name = arrayString[0] + "[" + (index - 1) + "]";
						$(item).attr('name', name);

						// set id
						if($(item).attr('id') != null){
							id = $(item).attr('id');
							var arrayString1 = id.split('_');
							id = arrayString1[0] + "_" + (index - 1);
							$(item).attr('id', id);
						}
						
						if ($(item).attr('checked') === "checked")
							$(item).prop("checked", true);

					});

					// set name to radio element in row
					$(element).find("label[class='css-label-radio']").each(function (i, item) {

						// set for
						forAttr = $(item).attr('for');
						var arrayString = forAttr.split('_');
						forAttr = arrayString[0] + "_" + (index - 1);
						$(item).attr('for', forAttr);

					});

					$(element).find("[aria-describedby]").each(function (i, item) {
						// remove attr
						$(item).removeAttr('aria-describedby');
					});

					if (this.needValidate)
						$.validator.unobtrusive.parseDynamicContent(element);
				}
			}
		});
	};


	//Up & Down line
	Grid.prototype.MoveRowUp = function () {
		// get max row, min row of table
		var rowMax = $(this.name + " .tbl_row").length - 2;
		var rowMin = $(this.name + " .tbl_row").first().index();
		var selectedIndex = this.GetSelectedIndex();

		// get row selected
		var itemSelected = $("#" + this.rowPrefix + selectedIndex);

		// check row index
		if (selectedIndex < 0) {
			return;
		}

		if (selectedIndex > 1 && selectedIndex > rowMin) {
			itemSelected.insertBefore(itemSelected.prev());

			//Updated row index
			this.updatedLine(selectedIndex - 1);
		}
	};

	Grid.prototype.MoveRowDown = function () {
		// get max row, min row of table
		var rowMax = $(this.name + " .tbl_row").length - 2;
		var rowMin = $(this.name + " .tbl_row").first().index();
		var selectedIndex = this.GetSelectedIndex();

		// get row selected
		var itemSelected = $("#" + this.rowPrefix + selectedIndex);

		// check row index
		if (selectedIndex < 0) {
			return;
		}

		if (selectedIndex < rowMax) {
			// process row index < max row
			itemSelected.insertAfter(itemSelected.next());

			//Updated row index
			this.updatedLine(selectedIndex);
		}
	};

	Grid.prototype.InsertLastRow = function (index) {
		var itemClone = $("#" + this.rowBlank).clone().removeClass('hidden');

		//get row index
		var itemSelectedIndex = this.GetSelectedIndex();
		var itemSelected;
		var nextIndex;
		var hasTableContent = false;

		if (index != null) {
			itemSelectedIndex = index;
		}

		// check row index is exist
		// if don't exist
		if (itemSelectedIndex < 0) {
			// insert row to last row of table
			var maxRow = $("div[id*=" + this.rowPrefix + "]").length;
			nextIndex = maxRow + 1;
		}
			// row selected is exist
		else
			nextIndex = itemSelectedIndex + 1;

		itemSelected = $("#" + this.rowPrefix + (nextIndex - 1));

		if (itemSelected.length > 0) {
			// insert row blank to next row of row selected
			itemClone.insertAfter(itemSelected)
			.attr({
				'id': this.rowPrefix + nextIndex
			});
		} else {
			if ($(this.name).find(".tbl_content").length > 0) {
				itemClone.insertBefore($(this.name).find("#" + this.rowBlank))
				.attr({
					'id': this.rowPrefix + nextIndex
				});
			} else {
				itemClone.insertAfter($(this.name).find(".Heading"))
				.attr({
					'id': this.rowPrefix + nextIndex
				});
			}
		}

		this.UpdatedIndexInLine(nextIndex);

		//if (this.needValidate)
		//	$.validator.unobtrusive.parseDynamicContent('#' + this.rowPrefix + nextIndex);
	};

	Grid.prototype.UpdatedIndexInLine = function (rIndex) {
		var tableName = this.name;
		var rowPrefix = this.rowPrefix;
		var rowBlank = this.rowBlank;
		$(tableName + ' .tbl_row').each(function (index, element) {
			if (index >= rIndex) {
				//get id of row
				var id = $(element).attr('id');

				// check row not blank
				if (id != rowBlank) {
					// reset id to row
					$(element).attr('id', rowPrefix + index);

					//get text of first element of row
					$(element).children().first().text(index);

					// set name, id to children element in row
					$(element).find('input[type=text], input[type=hidden], input[type=tel], select').each(function (i, item) {
						// reset name to control
						name = $(item).attr('name');
						var arrayString = name.split('.');

						var prefixName = name.match(/(\w+)?\[(-\d+|\d+)\].(\w+)/)[1];
						prefixName = prefixName ? prefixName : "";

						name = prefixName + "[" + (index - 1) + "]." + arrayString[1];
						$(item).attr('name', name);

						// reset id to control
						if ($(item).attr('id') != null) {
							id = $(item).attr('id');
							var arrayString = id.split('-');
							id = (index - 1) + "-" + arrayString[1];
							$(item).attr('id', id);
						}
					});

					// set name, id to radio element in row
					$(element).find('input[type=radio]').each(function (i, item) {

						// set name
						name = $(item).attr('name');
						var arrayString = name.split('.');
						name = arrayString[0] + "[" + (index - 1) + "]";
						$(item).attr('name', name);

						// set id
						if ($(item).attr('id') != null) {
							id = $(item).attr('id');
							var arrayString1 = id.split('_');
							id = arrayString1[0] + "_" + (index - 1);
							$(item).attr('id', id);
						}

						if ($(item).attr('checked') === "checked")
							$(item).prop("checked", true);

					});

					// set name to radio element in row
					$(element).find("label[class='css-label-radio']").each(function (i, item) {

						// set for
						forAttr = $(item).attr('for');
						var arrayString = forAttr.split('_');
						forAttr = arrayString[0] + "_" + (index - 1);
						$(item).attr('for', forAttr);

					});

					$(element).find("[aria-describedby]").each(function (i, item) {
						// remove attr
						$(item).removeAttr('aria-describedby');
					});

					//if (this.needValidate)
					//	$.validator.unobtrusive.parseDynamicContent(element);
				}
			}
		});
	};

	Grid.prototype.H4020InsertRow = function (index) {
		var itemClone = $("#" + this.rowBlank).clone().removeClass('hidden');

		//get row index
		var itemSelectedIndex = this.GetSelectedIndex();
		var itemSelected;
		var nextIndex;
		var hasTableContent = false;

		if (index != null) {
			itemSelectedIndex = index;
		}

		// check row index is exist
		// if don't exist
		if (itemSelectedIndex < 0) {
			// insert row to last row of table
			var maxRow = $("div[id*=" + this.rowPrefix + "]").length;
			nextIndex = maxRow + 1;
		}
			// row selected is exist
		else
			nextIndex = itemSelectedIndex + 1;

		itemSelected = $("#" + this.rowPrefix + (nextIndex - 1));

		if (itemSelected.length > 0) {
			// insert row blank to next row of row selected
			itemClone.insertAfter(itemSelected)
			.attr({
				'id': this.rowPrefix + nextIndex
			});
		} else {
			if ($(this.name).find(".tbl_content").length > 0) {
				itemClone.insertBefore($(this.name).find("#" + this.rowBlank))
				.attr({
					'id': this.rowPrefix + nextIndex
				});
			} else {
				itemClone.insertAfter($(this.name).find(".Heading"))
				.attr({
					'id': this.rowPrefix + nextIndex
				});
			}
		}

		this.H4020UpdateLine(nextIndex);

		//if (this.needValidate)
			//$.validator.unobtrusive.parseDynamicContent('#' + this.rowPrefix + nextIndex);
	};
	
	Grid.prototype.H4020RemoveRow = function (selectedIndex) {
		// get row selected
		var itemSelected = $("#" + this.rowPrefix + selectedIndex);

		itemSelected.remove();

		//Updated row index
		this.H4020UpdateLine(itemSelected);
		return true;
	}

	Grid.prototype.H4020UpdateLine = function (rIndex) {
		var tableName = this.name;
		var rowPrefix = this.rowPrefix;
		var rowBlank = this.rowBlank;
		$('div[id*=rowIndex]').each(function (index, element) {
			// if (index >= (rIndex-1)) {
				//get id of row
				var id = $(element).attr('id');

				// check row not blank
				if (id != rowBlank) {
					// reset id to row
					$(element).attr('id', rowPrefix + (index+1));

					// set name, id to children element in row
					$(element).find('input[type=text], input[type=hidden], input[type=tel], select').each(function (i, item) {
						// reset name to control
						name = $(item).attr('name');
						//var value = index - 1;
						//if (name.match(/\[\d+\]/)) {
						//if (name.match(/([\d+])/)) {
						//	name = name.replace(/(\d+)/,  value );
						//} else {
						//	name = name.replace(/\w+\./, "[" + value + "].");
						//}
						//var value = index;
						//name = name.replace('ListOrderModel[-1]', "[" + value + "]");
						//$(item).attr('name', name);
						var arrayString = name.split('.');

						name = "[" + index + "]." + arrayString[1];
						$(item).attr('name', name);

						// reset id to control
						if ($(item).attr('id') != null) {
							id = $(item).attr('id');
							var arrayString = id.split('-');
							id = (index ) + "-" + arrayString[1];
							$(item).attr('id', id);
						}
					});

					$(element).find("[aria-describedby]").each(function (i, item) {
						// remove attr
						$(item).removeAttr('aria-describedby');
					});

					//if (this.needValidate)
						//$.validator.unobtrusive.parseDynamicContent(element);
				}
			// }
		});
	};

	Grid.prototype.RemoveRowNotInsert = function (selectedIndex) {
		var totalRow;
		// get row selected
		var itemSelected = $("#" + this.rowPrefix + selectedIndex);

		itemSelected.remove();

		//Updated row index
		this.updatedLine(selectedIndex);

		return true;
	}

}());