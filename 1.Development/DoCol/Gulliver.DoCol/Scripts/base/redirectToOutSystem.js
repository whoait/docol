function GotoAA(CAR_SUB_HANYO_KEY14, CAR_SUB_HANYO_KEY1) {
	if (IsEmpty(CAR_SUB_HANYO_KEY14) == false && IsEmpty(CAR_SUB_HANYO_KEY1) == false) {
		var url = 'http://media.in.glv.co.jp/aa/pdf/org/' + CAR_SUB_HANYO_KEY14 + '/' + CAR_SUB_HANYO_KEY1 + '/' + CAR_SUB_HANYO_KEY1 + '.PDF';
		Popup(url);
	}
	event.preventDefault();
};
function GotoDolphin(SHUPPIN_TOROKU_NO) {
	if (IsEmpty(SHUPPIN_TOROKU_NO) == false) {
		var url = 'http://dolphinet-i.in.glv.co.jp/direct_login2.aspx?DN_NO=' + SHUPPIN_TOROKU_NO + '&MODE=1&SHOP=G99999&PASS=honbu2';
		Popup(url);
	}
	event.preventDefault();
};
function GotoStatusFutai(DenpyoNo, ChohyoShuBetsu, Hansu) {
	if (IsEmpty(DenpyoNo) == false && IsEmpty(ChohyoShuBetsu) == false && IsEmpty(Hansu) == false) {
		var url = 'http://sos-site1.in.glv.co.jp/EstSub/JuchuSakusei.aspx?denpyoNo=' + DenpyoNo + '&chohyoShubetsu=' + ChohyoShuBetsu + '&hansu=' + Hansu + '&searchType=2';
		Popup(url);
	}
	event.preventDefault();
};
function GotoClaim(tantoshaCd, tantoshaName, denpyoNo) {
	if (IsEmpty(denpyoNo) == false && IsEmpty(tantoshaCd) == false && IsEmpty(tantoshaName) == false) {
		var inputParameter = new Object();
		inputParameter.tantoshaCd = tantoshaCd;
		inputParameter.tantoshaName = tantoshaName;
		inputParameter.dempyoNo = denpyoNo;
		inputParameter.versionNo = "1.0";
		inputParameter.ninshoMode = "101";
		inputParameter.seniMotoKbn = "203";
		inputParameter.seniSakiKbn = "202";
		inputParameter.seniFukaFlg = "0x01";
		var entryLogin = "tantoshaCd=" + tantoshaCd + "&tantoshaName=" + tantoshaName + "&dempyoNo=" + denpyoNo + "&versionNo=1.0&ninshoMode=101&seniMotoKbn=203&seniSakiKbn=202&seniFukaFlg=0x01";
		var url = 'http://dn-satisfaction.in.glv.co.jp/DH00/DH000020/DH000020.aspx';
		PopupWithMethodPost('POST', url, inputParameter);
	}
	event.preventDefault();
};
function GotoRingi(kanriNo, denpyoKbn) {
	if (IsEmpty(denpyoKbn) == false && IsEmpty(kanriNo) == false) {
		var url = 'http://sos-site3.in.glv.co.jp/AppForm/PopRingiConf.aspx?denpyoKbn=' + denpyoKbn + '&kanriNo=' + kanriNo;
		Popup(url);
	}
	event.preventDefault();
};