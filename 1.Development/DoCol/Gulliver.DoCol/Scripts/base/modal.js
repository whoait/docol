(function () {
	Modal = function (IDModal, urlModal, inputCfgModal) {
		// default setting modal
		this.cfgModal = {
			rootModal: null,
			width: null,
			autoOpen: false,
			dataModal: null,
			Close: null,
			autoClose: false,
		};

		if (inputCfgModal) {
			// set config Modal
			this.cfgModal.rootModal = inputCfgModal.rootModal ? inputCfgModal.rootModal : this.cfgModal.rootModal;
			this.cfgModal.width = inputCfgModal.width ? inputCfgModal.width : this.cfgModal.width;
			this.cfgModal.autoOpen = inputCfgModal.autoOpen ? inputCfgModal.autoOpen : this.cfgModal.autoOpen;
			this.cfgModal.dataModal = inputCfgModal.dataModal ? inputCfgModal.dataModal : this.cfgModal.dataModal;
			this.cfgModal.Close = inputCfgModal.Close ? inputCfgModal.Close : this.cfgModal.Close;
			this.cfgModal.autoClose = inputCfgModal.autoClose != undefined ? inputCfgModal.autoClose : inputCfgModal.autoClose;
		}
		this.IDModal = IDModal;
		initModal(IDModal, urlModal, this.cfgModal);
	}

	// Modal common : callModal + clickSwichModal
	function modalContent(IDModal) {
		return '<div class="modal fade" id="' + IDModal + '" data-backdrop="static" role="dialog" aria-labelledby="' + IDModal + '" aria-hidden="true">'
				+ '<div class="modal-dialog modal-lg">'
				+ ' <div class="modal-content">'
				//+ '       <div class="modal-header">'
				//+ '       </div>'
				+ '     <div class="modal-body">'
				+ '     </div>'
				+ '  </div>'
				+ ' </div>'
				+ '</div>';
	}

	function initModal(IDModal, urlModal, cfgModal) {
		// set rootModal = body if rootModal empty
		if (cfgModal.rootModal == undefined || cfgModal.rootModal == null) {
			cfgModal.rootModal = 'body';
		}

		// check element modal exists
		if ($('#' + IDModal).length == 0) {
			$(cfgModal.rootModal).append(modalContent(IDModal));
		}

		// set event modalshow for Modal
		$(document).on('show.bs.modal', '#' + IDModal, function () {
			// Check Modal is displayed.
			if ($('#' + IDModal).data('bs.modal') != undefined && $('#' + IDModal).data('bs.modal').isShown != true) {
				if (cfgModal.width != null) {
					$('#' + IDModal + ' .modal-lg').width(cfgModal.width);
				}

				// Set height parent modal if exists
				var parentHeight = $('#' + IDModal).parents(".modal-content").height();
				$('#' + IDModal).parents(".modal-content").css('height', parentHeight + 100);

				// Show loading
				$("#" + IDModal + " .modal-body").html('Loading...');

				getModalContent(IDModal, urlModal, cfgModal);
			}
		});

		// auto open modal
		if (cfgModal.autoOpen) {
			$('#' + IDModal).modal('show');
		}

		// callback close popup
		if (cfgModal.Close) {
			$('#' + IDModal).off('hide.bs.modal');
			$('#' + IDModal).on('hide.bs.modal', function () {
				cfgModal.Close();
			});
		}
	}

	function getModalContent(IDModal, urlModal, cfgModal) {
		// Get content Modal
		CallAjaxPost(encodeURI(urlModal), cfgModal.dataModal, function (data) {
			if (data != undefined && data != null && data.error) {
				window.location.href = data.url;
				return;
			}

			$("#" + IDModal + " .modal-body").html(data);

			// Call function remove logo modal form autolayout.js
			if (typeof removeItemModal !== 'undefined') removeItemModal($('#' + IDModal));

			// Call ShowMessengerValidate for modal from Common.js
			if (typeof ShowMessengerValidate !== 'undefined') ShowMessengerValidate('#' + IDModal);
		});
	}

	Modal.prototype.ChangeData = function (data) {
		this.cfgModal.dataModal = data;
	};

	Modal.prototype.ChangeDataAndOpen = function (data) {
		this.ChangeData(data);
		$('#' + this.IDModal).modal('show');
	};

	Modal.prototype.Open = function () {
		$('#' + this.IDModal).modal('show');
	};
}());

// Modal common : callModal + clickSwichModal
function modalContent(IDModal) {
	return '<div class="modal fade" id="' + IDModal + '" tabindex="-1" data-backdrop="static" role="dialog" aria-labelledby="' + IDModal + '" aria-hidden="true">'
            + '<div class="modal-dialog modal-lg">'
            + ' <div class="modal-content">'
            //+ '       <div class="modal-header">'
            //+ '       </div>'
            + '     <div class="modal-body">'
            + '     </div>'
            + '  </div>'
            + ' </div>'
            + '</div>';
}

function callModal(IDModal, urlModal, inputCfgModal) {
	// Default setting modal
	var cfgModal = {
		rootModal: null,
		width: null,
		autoOpen: false,
		dataModal: null,
		Close: null
	};

	if (inputCfgModal) {
		// Set config Modal
		cfgModal.rootModal = inputCfgModal.rootModal ? inputCfgModal.rootModal : cfgModal.rootModal;
		cfgModal.width = inputCfgModal.width ? inputCfgModal.width : cfgModal.width;
		cfgModal.autoOpen = inputCfgModal.autoOpen ? inputCfgModal.autoOpen : cfgModal.autoOpen;
		cfgModal.dataModal = inputCfgModal.dataModal ? inputCfgModal.dataModal : cfgModal.dataModal;
		cfgModal.Close = inputCfgModal.Close ? inputCfgModal.Close : cfgModal.Close;
	}

	// Set rootModal = body if rootModal empty
	if (cfgModal.rootModal == undefined || cfgModal.rootModal == null) {
		cfgModal.rootModal = 'body';
	}

	// Check element modal exists
	if ($('#' + IDModal).length == 0) {
		$(cfgModal.rootModal).append(modalContent(IDModal));
	}

	// Set event modalshow for Modal
	$('#' + IDModal).off('show.bs.modal');
	$('#' + IDModal).on('show.bs.modal', function () {
		// Check Modal is displayed.
		if ($('#' + IDModal).data('bs.modal') != undefined && $('#' + IDModal).data('bs.modal').isShown != true) {
			if (cfgModal.width != null) {
				$('#' + IDModal + ' .modal-lg').width(cfgModal.width);
			}

			// Set height parent modal if exists
			parentHeight = $('#' + IDModal).parents(".modal-content").height();
			$('#' + IDModal).parents(".modal-content").css('height', parentHeight + 100);

			// Show loading
			$("#" + IDModal + " .modal-body").html('Loading... <button tabindex="1" type="button" data-dismiss="modal" class="close_popup fr" aria-hidden="true">X</button>');

			// Get content Modal
			// Console.log(cfgModal.dataModal);

			CallAjaxPost(encodeURI(urlModal), cfgModal.dataModal, function (data) {
				$("#" + IDModal + " .modal-body").html(data);

				// Call function remove logo modal form autolayout.js
				if (typeof removeItemModal !== 'undefined') removeItemModal($('#' + IDModal));

				// Call ShowMessengerValidate for modal from Common.js
				if (typeof ShowMessengerValidate !== 'undefined') ShowMessengerValidate('#' + IDModal);
			});
		}
	});

	// Auto open modal if no message is show
	if (cfgModal.autoOpen && !$(elementMsg).length) {
		$('#' + IDModal).modal('show');
		$(".modal-open").removeAttr("style");
	}

	// Callback close popup
	if (cfgModal.Close) {
		$('#' + IDModal).off('hide.bs.modal');
		$('#' + IDModal).on('hide.bs.modal', function () {
			cfgModal.Close();
		});
	}
	$(".modal-open").removeAttr("style");
}

function clickSwichModal(element, urlModal, dataModal) {
	$(element).click(function () {
		parentModal = $(this).parents(".modal-body");
		swichModal(parentModal, urlModal, dataModal);
	});
}

function swichModal(parentModal, urlModal, dataModal) {
	CallAjaxPost(encodeURI(urlModal), dataModal, function (data) {
		parentModal.html(data);
	});
}

// Show messenger box common : callMess
function messContent(msg) {
	// Set content header
	headerContent = !msg.header ? '' : '<div class="modal-header">'
                                        + '     <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>'
                                        + '     <h4 class="modal-title" id="confirmModalLabel">' + msg.header + '</h4>'
                                        + '</div>';
	// Set content body
	bodyContent = !msg.body ? '' : '<div class="modal-body">' + msg.body + '</div>';

	// Set content btn Action
	btnAction = !msg.btnAction ? '' : msg.btnAction;
	btnAction = !msg.lbAction ? btnAction : '<button id="btnCallbackConfirm" class="btn btn-default">' + msg.lbAction + '</button>';

	// Set content btn Action
	btnNOK = !msg.btnNOK ? '' : msg.btnNOK;
	btnNOK = !msg.lbNOK ? btnNOK : '<button id="btnCallbackNOK" class="btn btn-default">' + msg.lbNOK + '</button>';

	// Set label btn Close
	lbClose = !msg.lbClose ? '' : msg.lbClose;

	return ''
            + headerContent
            + bodyContent
            + '<div class="modal-footer">'
			+ btnAction
			+ btnNOK
            + '     <button type="button" id="modalClose" class="btn btn-default" data-dismiss="modal">' + lbClose + '</button>'
            + '</div>';
}

function divConfirmModal() {
	return '<div class="modal fade" id="confirm-modal" tabindex="-1" data-backdrop="static" role="dialog" aria-labelledby="confirmModalLabel" aria-hidden="true">'
            + '   <div class="modal-dialog">'
            + '        <div class="modal-content">'
            + '        </div>'
            + '    </div>'
            + '</div>';
}

var elementMsg = '#confirm-modal';

// Manually hidden modal : $(elementMsg).modal('hide');
function callMess(msg, callbackAction, callbackNOK, rootModal, callbackClose) {
	// Default messenger input
	var inputMsg = {
		header: '',
		body: '',
		btnAction: '',
		lbAction: '',
		flCloseLastest: false,
		lbClose: 'Close',
		btnNOK: '',
		lbNOK: '',
		closeRunCB: false,	//  Close to run callbackClose
		autoClose: true,
		closeBy: 0			// 0 : btnClose, 1 : btnAction, 2 : both
	};

	// Set messenger input
	inputMsg.header = msg.header ? msg.header : inputMsg.header;
	inputMsg.body = msg.body ? msg.body : inputMsg.body;
	inputMsg.btnAction = msg.btnAction ? msg.btnAction : inputMsg.btnAction;
	inputMsg.lbAction = msg.lbAction ? msg.lbAction : inputMsg.lbAction;
	inputMsg.flCloseLastest = msg.flCloseLastest != undefined ? msg.flCloseLastest : inputMsg.flCloseLastest;
	inputMsg.lbClose = msg.lbClose ? msg.lbClose : inputMsg.lbClose;

	inputMsg.btnNOK = msg.btnNOK ? msg.btnNOK : inputMsg.btnNOK;
	inputMsg.lbNOK = msg.lbNOK ? msg.lbNOK : inputMsg.lbNOK;

	inputMsg.closeRunCB = msg.closeRunCB != undefined ? msg.closeRunCB : inputMsg.closeRunCB;
	inputMsg.autoClose = msg.autoClose != undefined ? msg.autoClose : inputMsg.autoClose;
	inputMsg.closeBy = msg.closeBy ? msg.closeBy : inputMsg.closeBy;

	// Set rootModal = body if rootModal empty
	if (!rootModal) {
		rootModal = 'body';
	}

	// Check element modal exists
	if ($(elementMsg).length == 0) {
		$(rootModal).append(divConfirmModal());
	}

	// Set content messenger
	var contentMessenger = messContent(inputMsg);
	$(elementMsg + ' .modal-content').html(contentMessenger);

	$(elementMsg).off('shown.bs.modal');
	$(elementMsg).on('shown.bs.modal', function () {
		$("#modalClose").focus();
	});

	// Show modal messenger
	$(elementMsg).modal('show');

	// Set event callback if click to lbAction
	if (inputMsg.lbAction) {
		$('#btnCallbackConfirm').bind('click', function () {
			// run callback if click to btn confirm
			if (callbackAction) callbackAction();

			// Close messbox
			if (inputMsg.autoClose || inputMsg.closeBy == 1 || inputMsg.closeBy == 2)
				$(elementMsg).modal('hide');
		});
	}

	// Set event callback if click to lbAction
	if (inputMsg.lbNOK) {
		$('#btnCallbackNOK').bind('click', function () {
			// run callback if click to btn confirm
			if (callbackNOK) callbackNOK();

			// Close messbox
			if (inputMsg.autoClose || inputMsg.closeBy == 1 || inputMsg.closeBy == 2)
				$(elementMsg).modal('hide');
		});
	}

	// If closeRunCB = true set flag is false
	if (inputMsg.closeRunCB)
		flRunClose = false;

	if (callbackClose) {
		$('#modalClose').bind('click', function () {
			// Run callback if click to btn confirm
			callbackClose();

			// If closeRunCB = true set flag is true
			if (inputMsg.closeRunCB)
				flRunClose = true;

			// Close messbox
			if (inputMsg.autoClose || inputMsg.closeBy == 0 || inputMsg.closeBy == 2)
				$(elementMsg).modal('hide');
		});
	}

	// Add event remove div confirm if close
	$(elementMsg).bind('hidden.bs.modal', function () {
		// Run callback close if closeRunCB is true
		if (inputMsg.closeRunCB && !flRunClose) {
			if (callbackClose) callbackClose();

			flRunClose = false;
		}

		// Run callbackClose after hidden modal
		if (inputMsg.flCloseLastest) {
			if (callbackClose) callbackClose();
		}

		$(elementMsg).remove();

		if ($('.modal.fade.in').length > 0) {
			$('body').addClass('modal-open');
		}
	});

	$(".modal-open").removeAttr("style");
}

function DisplayMsgBox(clMsg, strMsg, callbackAction, callbackNOK, msg, callbackClose) {
	var message;
	if (clMsg.length > 1) {
		message = JSON.parse(clMsg.replace(/&quot;/g, '"'));
	} else {
		message = {
			messageType: clMsg,
			buttonOK: "OK"
		};
	}

	// Default messenger input
	var inputMsg = {
		header: '',
		body: '',
		btnAction: '',
		lbAction: '',
		flCloseLastest: false,
		lbClose: 'Close',
		btnNOK: '',
		lbNOK: '',
		closeRunCB: false,	//  Close to run callbackClose
		autoClose: true,
		closeBy: 0		// 0 : btnClose, 1 : btnAction, 2 : both
	};

	// Convert || to new line
	strMsg = strMsg.replace(/\|\|/gi, "<br />");

	// Set input autoClose
	if (msg) {
		inputMsg.btnAction = msg.btnAction ? msg.btnAction : inputMsg.btnAction;
		inputMsg.lbAction = msg.lbAction ? msg.lbAction : inputMsg.lbAction;
		inputMsg.flCloseLastest = msg.flCloseLastest != undefined ? msg.flCloseLastest : inputMsg.flCloseLastest;
		inputMsg.lbClose = msg.lbClose ? msg.lbClose : inputMsg.lbClose;
		inputMsg.closeRunCB = msg.closeRunCB != undefined ? msg.closeRunCB : inputMsg.closeRunCB;
		inputMsg.autoClose = msg.autoClose != undefined ? msg.autoClose : inputMsg.autoClose;
		inputMsg.closeBy = msg.closeBy ? msg.closeBy : inputMsg.closeBy;
	}

	if (message.buttonOK != null && message.buttonNOK == null) {
		inputMsg.lbClose = message.buttonOK;
	}

	if (message.buttonNOK != null) {
		inputMsg.lbAction = message.buttonOK;
		inputMsg.lbClose = message.buttonNOK;
		switch (message.messageType) {
			case "I":
				message.messageType = "IC";
				break;
			case "W":
				message.messageType = "WC";
				break;
		}
	}

	if (message.buttonCancel != null) {
		inputMsg.lbAction = message.buttonOK;
		inputMsg.lbNOK = message.buttonNOK;
		inputMsg.lbClose = message.buttonCancel;
		switch (message.messageType) {
			case "I":
				message.messageType = "IC";
				break;
			case "W":
				message.messageType = "WC";
				break;
		}
	}

	switch (message.messageType) {
		case "I":
			inputMsg.header = '情報';
			inputMsg.body = '<div class="Dialog_Information">' + strMsg + '</div>';
			break;

		case "IC":
			inputMsg.header = '確認';
			inputMsg.body = '<div class="Dialog_Information">' + strMsg + '</div>';
			break;

		case "WC":
			inputMsg.header = 'ワーニング';
			inputMsg.body = '<div class="Dialog_Warning">' + strMsg + '</div>';
			break;

		case "Q":
			inputMsg.header = '質問';
			inputMsg.body = '<div class="Dialog_Question">' + strMsg + '</div>';
			break;

		case "W":
			inputMsg.header = 'ワーニング';
			inputMsg.body = '<div class="Dialog_Warning">' + strMsg + '</div>';
			break;

		case "E":
			inputMsg.header = 'エラー';
			inputMsg.body = '<div class="Dialog_Error">' + strMsg + '</div>';
			break;
	}

	// Call mess
	callMess(inputMsg, callbackAction, callbackClose, null, callbackNOK);

	$('.close').css('display', 'none');
}

function ShowMsgBox(clsMsg, strMsg, callbackAction, callbackNOK, msg, callbackClose) {
	if (strMsg.trim() != '') {
		DisplayMsgBox(clsMsg, strMsg, callbackAction, callbackClose, msg, callbackNOK);
	}
}

// demo
//$(document).ready(function () {
//    callMess({
//        header: 'Demo Header',
//        body: 'Demo Body',
//        lbAction: 'Delete',
//        lbClose: 'Cancle'
//    }, function () {
//        console.log('run function callback');
//    });

//    callMess({
//        header: 'Demo Header',
//        body: 'Demo Body',
//        btnAction: '<a href="#" id="btnCallbackConfirm" class="btn btn-danger danger">Delete</a>',
//        lbClose: 'Demo Close'
//    }, function () {
//        console.log('run function callback');
//    });

//});