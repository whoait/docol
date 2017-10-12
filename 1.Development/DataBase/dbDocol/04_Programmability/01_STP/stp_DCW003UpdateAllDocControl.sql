USE [dbDOC]
GO

/****** Object:  StoredProcedure [dbo].[stp_DCW003UpdateAllDocControl]    Script Date: 2015/12/25 11:54:55 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROC [dbo].[stp_DCW003UpdateAllDocControl]
(	
	@ListUpdate				DCW003ListUpdate	READONLY
	,@User						varchar(10)
)
AS

---------------------------------------------------------------------------
-- Version			: 001
-- Designer			: NghiaDT1
-- Programmer		: NghiaDT1
-- Created Date		: 2015/12/05
-- Comment			: Store update all TT_DOC_CONTROL of DCW003 
---------------------------------------------------------------------------
BEGIN
	SET NOCOUNT ON
	DECLARE @p_saiban_value	varchar(16)
	DECLARE @p_result_id	int
	DECLARE @p_result_msg	varchar(2000)

	DECLARE @ct_DOC_CONTROL_NO				varchar(13)
	DECLARE @ct_DOC_STATUS					char(3)
	DECLARE @ct_JISHAMEI_FLG				char(1)
	DECLARE @ct_MASSHO_FLG					char(1)
	DECLARE @ct_URIAGE_SHUPPINN_TOROKU_NO	varchar(8)
	DECLARE @ct_DOC_NYUKO_DATE				datetime
	DECLARE @ct_DOC_SHUKKO_DATE				datetime
	DECLARE @ct_JISHAMEI_IRAI_SHUKKO_DATE	datetime
	DECLARE @ct_JISHAMEI_IRAI_NYUKO_DATE	datetime
	DECLARE @ct_MASSHO_IRAI_SHUKKO_DATE		datetime
	DECLARE @ct_MASSHO_IRAI_NYUKO_DATE		datetime
	DECLARE @ct_MEMO						varchar(500)

	DECLARE doc_cursor CURSOR 
	FOR SELECT [DocControlNo]				
				,[MasshoFlg]				
				,[JishameiFlg]				
				,[DocStatus]					
				,[DocNyukoDate]				
				,[DocShukkoDate]
				,[UriageShuppinTorokuNo]				
				,[JishameiIraiShukkoDate]	
				,[JishameiIraiNyukoDate]	
				,[MasshoIraiShukkoDate]		
				,[MasshoIraiNyukoDate]		
				,[Memo]						 FROM @ListUpdate

	OPEN doc_cursor

	FETCH NEXT FROM doc_cursor INTO @ct_DOC_CONTROL_NO, @ct_MASSHO_FLG, @ct_JISHAMEI_FLG,@ct_DOC_STATUS, @ct_DOC_NYUKO_DATE, @ct_DOC_SHUKKO_DATE, @ct_URIAGE_SHUPPINN_TOROKU_NO
									, @ct_JISHAMEI_IRAI_SHUKKO_DATE, @ct_JISHAMEI_IRAI_NYUKO_DATE, @ct_MASSHO_IRAI_SHUKKO_DATE, @ct_MASSHO_IRAI_NYUKO_DATE, @ct_MEMO

	WHILE @@FETCH_STATUS = 0
	BEGIN
		-------------------------------------------------------------------------
		-- Insert History
		-------------------------------------------------------------------------
		EXEC USP_MAKE_HISTORY @ct_DOC_CONTROL_NO,NULL,@User,@p_saiban_value OUT,@p_result_id OUT ,@p_result_msg OUT

		-------------------------------------------------------------------------
		-- Update
		-------------------------------------------------------------------------
		UPDATE TT_DOC_CONTROL SET
				[TT_DOC_CONTROL].DOC_STATUS						= @ct_DOC_STATUS
				,[TT_DOC_CONTROL].URIAGE_SHUPPINN_TOROKU_NO		= @ct_URIAGE_SHUPPINN_TOROKU_NO
				,[TT_DOC_CONTROL].JISHAMEI_FLG					= @ct_JISHAMEI_FLG
				,[TT_DOC_CONTROL].MASSHO_FLG					= @ct_MASSHO_FLG
				,[TT_DOC_CONTROL].DOC_NYUKO_DATE				= @ct_DOC_NYUKO_DATE
				,[TT_DOC_CONTROL].DOC_SHUKKO_DATE				= @ct_DOC_SHUKKO_DATE
				,[TT_DOC_CONTROL].JISHAMEI_IRAI_SHUKKO_DATE		= @ct_JISHAMEI_IRAI_SHUKKO_DATE
				,[TT_DOC_CONTROL].JISHAMEI_KANRYO_NYUKO_DATE	= @ct_JISHAMEI_IRAI_NYUKO_DATE
				,[TT_DOC_CONTROL].MASSHO_IRAI_SHUKKO_DATE		= @ct_MASSHO_IRAI_SHUKKO_DATE
				,[TT_DOC_CONTROL].MASSHO_KANRYO_NYUKO_DATE		= @ct_MASSHO_IRAI_NYUKO_DATE
				,[TT_DOC_CONTROL].MEMO							= @ct_MEMO
				,[TT_DOC_CONTROL].UPDATE_DATE					= GETDATE()
				,[TT_DOC_CONTROL].UPDATE_USER_CD				= @User
		WHERE
				[TT_DOC_CONTROL].DOC_CONTROL_NO = @ct_DOC_CONTROL_NO
				AND [TT_DOC_CONTROL].DELETE_FLG = 0	

		FETCH NEXT FROM doc_cursor INTO @ct_DOC_CONTROL_NO, @ct_MASSHO_FLG, @ct_JISHAMEI_FLG,@ct_DOC_STATUS, @ct_DOC_NYUKO_DATE, @ct_DOC_SHUKKO_DATE, @ct_URIAGE_SHUPPINN_TOROKU_NO
									, @ct_JISHAMEI_IRAI_SHUKKO_DATE, @ct_JISHAMEI_IRAI_NYUKO_DATE, @ct_MASSHO_IRAI_SHUKKO_DATE, @ct_MASSHO_IRAI_NYUKO_DATE, @ct_MEMO
	END
	
	CLOSE doc_cursor
    DEALLOCATE doc_cursor          

END

GO

