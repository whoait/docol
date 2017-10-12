USE [dbDOC]
GO

/****** Object:  StoredProcedure [dbo].[stp_DCW003UpdateDocControl]    Script Date: 2015/12/25 11:55:09 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROC [dbo].[stp_DCW003UpdateDocControl]
(	
	@DocControlNo				varchar(13)
	,@UriageShuppinnTorokuNo	varchar(8)
	,@MasshoFlg					char(1)
	,@JishameiFlg				char(1)
	,@DocStatus					char(3)
	,@DocNyukoDate				datetime
	,@DocShukkoDate				datetime
	,@JishameiIraiShukkoDate	datetime
	,@JishameiKanryoNyukoDate	datetime
	,@MasshoIraiShukkoDate		datetime
	,@MasshoKanryoNyukoDate		datetime
	,@Memo						varchar(500)
	,@User						varchar(10)
	,@ErrorMsg					varchar(5)  OUT
)
AS

---------------------------------------------------------------------------
-- Version			: 001
-- Designer			: NghiaDT1
-- Programmer		: NghiaDT1
-- Created Date		: 2015/12/05
-- Comment			: Store update TT_DOC_CONTROL of DCW003 
---------------------------------------------------------------------------
BEGIN
	SET NOCOUNT ON

	DECLARE @p_saiban_value	varchar(16)
	DECLARE @p_result_id	int
	DECLARE @p_result_msg	varchar(2000)
	-------------------------------------------------------------------------
	-- Insert History
	-------------------------------------------------------------------------
	EXEC USP_MAKE_HISTORY @DocControlNo,NULL,@User,@p_saiban_value OUT,@p_result_id OUT ,@p_result_msg OUT

	-------------------------------------------------------------------------
	-- STP Process
	-------------------------------------------------------------------------
	UPDATE TT_DOC_CONTROL SET
		[TT_DOC_CONTROL].DOC_STATUS						= @DocStatus
		,[TT_DOC_CONTROL].URIAGE_SHUPPINN_TOROKU_NO		= @UriageShuppinnTorokuNo 
		,[TT_DOC_CONTROL].JISHAMEI_FLG					= @JishameiFlg 
		,[TT_DOC_CONTROL].MASSHO_FLG					= @MasshoFlg
		,[TT_DOC_CONTROL].DOC_NYUKO_DATE				= @DocNyukoDate
		,[TT_DOC_CONTROL].DOC_SHUKKO_DATE				= @DocShukkoDate
		,[TT_DOC_CONTROL].JISHAMEI_IRAI_SHUKKO_DATE		= @JishameiIraiShukkoDate
		,[TT_DOC_CONTROL].JISHAMEI_KANRYO_NYUKO_DATE	= @JishameiKanryoNyukoDate
		,[TT_DOC_CONTROL].MASSHO_IRAI_SHUKKO_DATE		= @MasshoIraiShukkoDate
		,[TT_DOC_CONTROL].MASSHO_KANRYO_NYUKO_DATE		= @MasshoKanryoNyukoDate
		,[TT_DOC_CONTROL].MEMO							= @Memo
		,[TT_DOC_CONTROL].UPDATE_DATE					= GETDATE()
		,[TT_DOC_CONTROL].UPDATE_USER_CD				= @User
	WHERE
		[TT_DOC_CONTROL].DOC_CONTROL_NO	= @DocControlNo
	AND [TT_DOC_CONTROL].DELETE_FLG = 0


	IF	@@ROWCOUNT = 0
	BEGIN
		SET @ErrorMsg = 'W0010'
	END	
END

GO

