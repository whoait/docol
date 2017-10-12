USE [dbDOC]
GO

/****** Object:  StoredProcedure [dbo].[stp_DCW003GetDocControlExist]    Script Date: 2015/12/25 11:50:59 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROC [dbo].[stp_DCW003GetDocControlExist]
(	
	@ListCsv		DCW003Csv	READONLY
	,@ModeSearch	int
	, @PageIndex int = 1
	, @PageSize int = 10
)
AS

---------------------------------------------------------------------------
-- Version			: 001
-- Designer			: NghiaDT1
-- Programmer		: NghiaDT1
-- Created Date		: 2015/12/03
-- Comment			: 
---------------------------------------------------------------------------
BEGIN
	SET NOCOUNT ON

	-------------------------------------------------------------------------
	-- STP Process
	-------------------------------------------------------------------------
	SELECT * FROM(
	SELECT 
		[TT_DOC_CONTROL].DOC_CONTROL_NO					AS DocControlNo							-- 書類管理番号
		,[TT_DOC_CONTROL].SHIIRE_SHUPPINN_TOROKU_NO		AS ShiireShuppinnTorokuNo				-- 仕入DN出品番号
		,[TT_DOC_CONTROL].URIAGE_SHUPPINN_TOROKU_NO		AS UriageShuppinnTorokuNo				-- 売上DN出品番号
		,[TT_DOC_CONTROL].CHASSIS_NO					AS ChassisNo							-- 車台番号
		,ISNULL([SHOP].TEMPO_CD,'') +ISNULL([SHOP].TEMPO_NAME,'')								AS ShopName								-- 出品店情報
		,ISNULL([RAKUSATSU_SHOP].TEMPO_CD,'') + ISNULL([RAKUSATSU_SHOP].TEMPO_NAME,'')			AS RakusatsuShopName					-- 落札店情報
		,[TT_DOC_CONTROL].SHIIRE_AA_KAIJO				AS ShiireAaKaijo						-- 仕入AA会場
		,[TT_DOC_CONTROL].URIAGE_AA_KAIJO				AS UriageAaKaijo						-- 売上AA会場
		,[TT_DOC_CONTROL].NENSHIKI						AS Nenshiki								-- 年式
		,[TT_DOC_CONTROL].KEI_CAR_FLG					AS KeiCarFlg							-- 車輌区分
		,[TT_DOC_CONTROL].AA_KAISAI_DATE				AS AaKaisaiDate							-- AA開催日
		,[TT_DOC_CONTROL].MAKER_NAME					AS MakerName							-- メーカー
		,[TT_DOC_CONTROL].CAR_NAME						AS CarName								-- 車名
		,[TT_DOC_CONTROL].GRADE_NAME					AS GradeName							-- グレード
		,[TT_DOC_CONTROL].AA_SHUPPIN_NO					AS AaShuppinNo							-- AA番号
		,[TT_DOC_CONTROL].DN_SEIYAKU_DATE				AS DnSeiyakuDate						-- DN成約日
		,[TT_DOC_CONTROL].KATASHIKI						AS Katashiki							-- 型式
		,[TT_DOC_CONTROL].MASSHO_FLG					AS MasshoFlg							-- 書類区分
		,[TT_DOC_CONTROL].JISHAMEI_FLG					AS JishameiFlg							-- 自社名区分
		,[TT_DOC_CONTROL].DOC_STATUS					AS DocStatus							-- 書類ステータス
		,[TT_DOC_CONTROL].TOROKU_NO						AS TorokuNo								-- 登録ナンバー
		,[TT_DOC_CONTROL].SHORUI_LIMIT_DATE				AS ShoruiLimitDate						-- 書類有効期限
		,[TT_DOC_CONTROL].FILE_NO						AS FileNo								-- ファイル番号
		,[TT_DOC_CONTROL].RACK_NO						AS RacNo
		,[TT_DOC_CONTROL].SHIIRE_NO						AS ShiireNo								-- 仕入番号
		,[TT_DOC_CONTROL].SHAKEN_LIMIT_DATE				AS ShakenLimitDate						-- 車検満了日
		,[TT_DOC_CONTROL].DOC_NYUKO_DATE				AS DocNyukoDate							-- 書類入庫日
		,[TT_DOC_CONTROL].JISHAMEI_IRAI_SHUKKO_DATE		AS JishameiIraiShukkoDate				-- 自社名依頼日
		,[TT_DOC_CONTROL].MASSHO_IRAI_SHUKKO_DATE		AS MasshoIraiShukkoDate					-- 抹消依頼日
		,[TT_DOC_CONTROL].DOC_SHUKKO_DATE				AS DocShukkoDate						-- 書類出庫日
		,[TT_DOC_CONTROL].JISHAMEI_KANRYO_NYUKO_DATE	AS JishameiKanryoNyukoDate				-- 自社名完了日
		,[TT_DOC_CONTROL].MASSHO_KANRYO_NYUKO_DATE		AS MasshoKanryoNyukoDate				-- 抹消完了日
		,[TT_DOC_CONTROL].MEMO							AS Memo									-- メモ
		,[TT_DOC_CONTROL].MEIHEN_SHAKEN_TOROKU_DATE		AS MeihenShakenTorokuDate
		, ROW_NUMBER() OVER(ORDER BY TT_DOC_CONTROL.DOC_CONTROL_NO) AS RowNum
		, [List].ID					AS ID
		,COUNT (*) OVER() AS [RowCount]
	-------------------------------------------------------------------------
	-- Source table
	-------------------------------------------------------------------------
	FROM	
		TT_DOC_CONTROL WITH(NOLOCK)
	LEFT JOIN TM_SHOP SHOP
	ON [TT_DOC_CONTROL].SHOP_CD = [SHOP].TEMPO_CD
	AND [SHOP].DELETE_FLG = 0
	LEFT JOIN TM_SHOP RAKUSATSU_SHOP
	ON [TT_DOC_CONTROL].RAKUSATSU_SHOP_CD = [RAKUSATSU_SHOP].TEMPO_CD
	AND [RAKUSATSU_SHOP].DELETE_FLG = 0
	INNER JOIN @ListCsv List
	ON [TT_DOC_CONTROL].CHASSIS_NO = [List].ChassisNo
	WHERE (@ModeSearch = 1 
			AND	([List].ReportType = 1 AND [TT_DOC_CONTROL].DOC_STATUS = '103')
				OR (([List].ReportType = 2 OR [List].ReportType = 4) AND [TT_DOC_CONTROL].DOC_STATUS = '104')
			AND [TT_DOC_CONTROL].RACK_NO = LEFT([List].RacFileNo,1)
			AND [TT_DOC_CONTROL].FILE_NO = RIGHT([List].RacFileNo,4))
		OR (@ModeSearch = 2 AND [TT_DOC_CONTROL].DOC_STATUS <> '101' AND [TT_DOC_CONTROL].DOC_STATUS <> '105' )
		OR (@ModeSearch = 3 AND [TT_DOC_CONTROL].DOC_STATUS = '102')
	) AS SearchInfoTempTable

		WHERE RowNum > @PageSize * (@PageIndex - 1) AND RowNum < @PageSize * @PageIndex + 1
		ORDER BY SearchInfoTempTable.ID
END



GO

